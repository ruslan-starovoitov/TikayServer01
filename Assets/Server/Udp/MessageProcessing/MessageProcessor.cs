﻿using Libraries.NetworkLibrary.Udp;
using Server.GameEngine.Experimental;
using Server.Udp.MessageProcessing.Handlers;
using Server.Udp.Sending;
using Server.Udp.Storage;
using System;
using System.Net;
using NetworkLibrary.NetworkLibrary.Udp;
using Server.GameEngine.MessageSorters;
using Server.GameEngine.Rudp;

namespace Server.Udp.MessageProcessing
{
    /// <summary>
    /// Перенаправляет все сообщения от игроков по обработчикам.
    /// </summary>
    public class MessageProcessor
    {
        private readonly PingMessageHandler pingMessageHandler;
        private readonly InputMessageHandler inputMessageHandler;
        private readonly PlayerExitMessageHandler exitMessageHandler;
        // private readonly RudpConfirmationSender rudpConfirmationSender;
        private readonly RudpConfirmationReceiver rudpConfirmationHandler;

        public MessageProcessor(InputEntitiesCreator inputEntitiesCreator, ExitEntitiesCreator exitEntitiesCreator,
             ByteArrayRudpStorage byteArrayRudpStorage,
             // UdpSendUtils udpSendUtils,
             IpAddressesStorage ipAddressesStorage)
        {
            pingMessageHandler = new PingMessageHandler(ipAddressesStorage);
            // rudpConfirmationSender = new RudpConfirmationSender(udpSendUtils);
            inputMessageHandler = new InputMessageHandler(inputEntitiesCreator);
            exitMessageHandler = new PlayerExitMessageHandler(exitEntitiesCreator);
            rudpConfirmationHandler = new RudpConfirmationReceiver(byteArrayRudpStorage);
        }
        
        public void Handle(MessageWrapper messageWrapper, IPEndPoint sender)
        {
            // if (messageWrapper.NeedResponse)
            // {
            //     rudpConfirmationSender.Handle(messageWrapper, sender);
            // }

            switch (messageWrapper.MessageType)
            {
                case MessageType.PlayerInput:
                    inputMessageHandler.Handle(messageWrapper, sender);
                    break;
                case MessageType.PlayerPing:
                    pingMessageHandler.Handle(messageWrapper, sender);
                    break;
                case MessageType.DeliveryConfirmation:
                    rudpConfirmationHandler.Handle(messageWrapper, sender);
                    break;
                case MessageType.PlayerExit:
                    exitMessageHandler.Handle(messageWrapper, sender);
                    break;
                default:
                    throw new Exception("Неожиданный тип сообщения "+messageWrapper.MessageType);
            }
        }
    }
}