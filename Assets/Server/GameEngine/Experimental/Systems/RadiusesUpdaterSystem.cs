﻿using Entitas;
using Server.Udp.Sending;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Server.GameEngine.Systems
{
    public class RadiusesUpdaterSystem : ReactivePlayersVisionSystem
    {
        public RadiusesUpdaterSystem(Contexts contexts, int matchId, UdpSendUtils udpSendUtils) : base(contexts, matchId, udpSendUtils)
        { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CircleCollider);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasCircleCollider && entity.hasCircleScaling;
        }

        protected override void SendData(UdpSendUtils udpSendUtils, int matchId, int playerId, IEnumerable<GameEntity> entities)
        {
            var dict = entities.ToDictionary(e => e.id.value, e => Mathf.FloatToHalf(e.circleCollider.radius));
            udpSendUtils.SendRadiuses(matchId, playerId, dict);
        }
    }
}