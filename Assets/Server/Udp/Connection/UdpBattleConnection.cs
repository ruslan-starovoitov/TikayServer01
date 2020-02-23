﻿using System.Net;

namespace Server.Udp.Connection
{
    public class UdpBattleConnection:UdpConnection
    {
        private readonly NetworkMediator mediator;
        
        public UdpBattleConnection(NetworkMediator mediator)
        {
            this.mediator = mediator;
            mediator.SetUdpConnection(this);
        }
        
        protected override void HandleBytes(byte[] data, IPEndPoint endPoint)
        {
            base.HandleBytes(data, endPoint);
            mediator.HandleBytes(data, endPoint);
        }
    }
}