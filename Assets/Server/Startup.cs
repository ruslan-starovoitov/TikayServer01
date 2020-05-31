﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Server.GameEngine;
using Server.GameEngine.Experimental;
using Server.Http;
using Server.Udp.Connection;
using Server.Udp.MessageProcessing;
using Server.Udp.Sending;
using Server.Udp.Storage;

//TODO добавить di контейнер, когда сервер станет стабильным

namespace Server
{
    /// <summary>
    /// Запускает все потоки при старте и убивает их при остановке.
    /// Устанавливает зависимости.
    /// </summary>
    public class Startup
    {
        private const int HttpPort = 14065;
        private const int UdpPort = 48956;
        
        private Thread httpListeningThread;
        private ShittyUdpMediator shittyUdpMediator;
        private Thread matchmakerNotifierThread;
        
        private MatchStorage matchStorage;
        private MatchRemover matchRemover;

        public void Run()
        {
            //Чек
            if (httpListeningThread != null)
            {
                throw new Exception("Сервер уже запущен");
            }

            //Старт уведомления матчмейкера о смертях игроков и окончании матчей
            MatchmakerNotifier notifier = new MatchmakerNotifier();
            matchmakerNotifierThread = notifier.StartThread();
            
            //Создание структур данных для матчей
            matchStorage = new MatchStorage();
            InputEntitiesCreator inputEntitiesCreator = new InputEntitiesCreator(matchStorage);
            ExitEntitiesCreator exitEntitiesCreator = new ExitEntitiesCreator(matchStorage);
            
            ByteArrayRudpStorage byteArrayRudpStorage = new ByteArrayRudpStorage();

            shittyUdpMediator = new ShittyUdpMediator();
            
            ShittyDatagramPacker shittyDatagramPacker = new ShittyDatagramPacker(1500, shittyUdpMediator);
            OutgoingMessagesStorage outgoingMessagesStorage = new OutgoingMessagesStorage(shittyDatagramPacker);
            UdpSendUtils udpSendUtils = new UdpSendUtils(matchStorage, byteArrayRudpStorage, outgoingMessagesStorage);
            MessageProcessor messageProcessor = new MessageProcessor(inputEntitiesCreator, exitEntitiesCreator, matchStorage,
                byteArrayRudpStorage, udpSendUtils);
            
            shittyUdpMediator.SetProcessor(messageProcessor);
            
            matchRemover = new MatchRemover(matchStorage, byteArrayRudpStorage, udpSendUtils, notifier);
            MatchFactory matchFactory = new MatchFactory(matchRemover, udpSendUtils, notifier);
            MatchCreator matchCreator = new MatchCreator(matchFactory);
            MatchLifeCycleManager matchLifeCycleManager = 
                new MatchLifeCycleManager(matchStorage, matchCreator, matchRemover);
            
            
            //Старт прослушки матчмейкера
            httpListeningThread = StartMatchmakerListening(HttpPort, matchCreator, matchStorage);

            //Старт прослушки игроков
            shittyUdpMediator
                .SetupConnection(UdpPort)
                .StartReceiveThread();

            RudpMessagesSender rudpMessagesSender = new RudpMessagesSender(byteArrayRudpStorage, matchStorage, udpSendUtils);
            GameEngineTicker gameEngineTicker = new GameEngineTicker(matchStorage, matchLifeCycleManager,
                inputEntitiesCreator, exitEntitiesCreator, rudpMessagesSender, outgoingMessagesStorage);
            
            //Старт тиков
            Chronometer chronometer = ChronometerFactory.Create(gameEngineTicker.Tick);
            chronometer.StartEndlessLoop();
        }
        
        private Thread StartMatchmakerListening(int port, MatchCreator matchCreator, MatchStorage matchStorageArg)
        {
            MatchDataMessageHandler matchDataMessageHandler = 
                new MatchDataMessageHandler(matchCreator, matchStorageArg);
            Thread thread = new Thread(() =>
            {
                new HttpConnection(matchDataMessageHandler).StartListenHttp(port).Wait();
            });
            thread.Start();
            return thread;
        }

        public void FinishAllMatches()
        {
            //TODO возможно lock поможет от одновременного вызова систем
            lock (matchRemover)
            {
                foreach (var match in matchStorage.GetAllMatches())
                {
                    matchRemover.MarkMatchAsFinished(match.MatchId);
                }
                matchRemover.DeleteFinishedMatches();    
            }
            //Жду, чтобы rudp о удалении матчей точно дошли до игроков
            Task.Delay(1500).Wait();
        }
        
        public void StopAllThreads()
        {
            httpListeningThread.Interrupt();
            shittyUdpMediator.Stop();
            matchmakerNotifierThread.Interrupt();
        }
    }
}