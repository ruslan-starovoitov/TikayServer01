﻿using Server.GameEngine.Experimental;
using Server.Udp.Sending;
using Server.Udp.Storage;

namespace Server.GameEngine
{
    /// <summary>
    /// Отвечает за правильный вызов подпрограмм во время тика.
    /// </summary>
    public class GameEngineTicker
    {
        private readonly MatchStorage matchStorage;
        private readonly MatchLifeCycleManager matchLifeCycleManager;
        private readonly InputEntitiesCreator inputEntitiesCreator;
        private readonly ExitEntitiesCreator exitEntitiesCreator;
        private readonly RudpMessagesSender rudpMessagesSender;

        public GameEngineTicker(MatchStorage matchStorage, MatchLifeCycleManager matchLifeCycleManager,
            InputEntitiesCreator inputEntitiesCreator, ExitEntitiesCreator exitEntitiesCreator,
            ByteArrayRudpStorage byteArrayRudpStorage, UdpSendUtils udpSendUtils)
        {
            this.matchStorage = matchStorage;
            this.matchLifeCycleManager = matchLifeCycleManager;
            this.inputEntitiesCreator = inputEntitiesCreator;
            this.exitEntitiesCreator = exitEntitiesCreator;
            rudpMessagesSender = new RudpMessagesSender(byteArrayRudpStorage, matchStorage, udpSendUtils);
        }

        public void Tick()
        {
            inputEntitiesCreator.Create();
            exitEntitiesCreator.Create();
            
            //Перемещение игровых сущностей
            foreach (Match match in matchStorage.GetAllMatches())
            {
                match.Tick();
            }

            //создание/удаление матчей
            matchLifeCycleManager.UpdateMatchesLifeStatus();
            
            //отправка rudp
            rudpMessagesSender.SendAll();
        }
    }
}