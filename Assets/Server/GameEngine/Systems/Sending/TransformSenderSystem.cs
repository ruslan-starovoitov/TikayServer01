﻿using System;
using System.Collections.Generic;
using Entitas;
using Plugins.submodules.SharedCode.LagCompensation;
using Plugins.submodules.SharedCode.Logger;
using Plugins.submodules.SharedCode.NetworkLibrary.Udp.ServerToPlayer.PositionMessages;
using Server.Udp.Sending;
using SharedSimulationCode.LagCompensation;
using UnityEngine;

namespace Server.GameEngine.Systems.Sending
{
    /// <summary>
    /// Каждому игроку отправляет все позиции.
    /// </summary>
    public class TransformSenderSystem : IExecuteSystem
    {
        private readonly int matchId;
        private readonly UdpSendUtils udpSendUtils;
        private readonly IGroup<GameEntity> allWithView;
        private readonly IGroup<GameEntity> alivePlayers;
        private readonly IGameStateHistory gameStateHistory;
        private readonly ILog log = LogManager.CreateLogger(typeof(TransformSenderSystem));

        public TransformSenderSystem(int matchId, Contexts contexts, UdpSendUtils udpSendUtils,
            IGameStateHistory gameStateHistory)
        {
            this.matchId = matchId;
            this.udpSendUtils = udpSendUtils;
            this.gameStateHistory = gameStateHistory;
            alivePlayers = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player).NoneOf(GameMatcher.Bot));
            allWithView = contexts.game.GetGroup(GameMatcher.Transform);
        }

        public void Execute()
        {
            Dictionary<ushort, ViewTransformCompressed> allGos = new Dictionary<ushort, ViewTransformCompressed>();

            if (allWithView.count == 0)
            {
                Debug.LogError("Нет объектов с моделями");
                return;
            }
            foreach (var entity in allWithView)
            {
                if (entity.transform.value == null)
                {
                    throw new Exception("transform пуст id = "+entity.id.value);
                }
                var position = entity.transform.value.position;
                float x = position.x;
                float z = position.z;
                float angle = entity.transform.value.rotation.eulerAngles.y;
                ViewTypeId viewTypeId = entity.viewType.id;
                ViewTransformCompressed viewTransform = new ViewTransformCompressed(x, z, angle, viewTypeId);
                allGos.Add(entity.id.value, viewTransform);
            }

            if (allGos.Count == 0)
            {
                Debug.LogError("Пустые координаты");
                return;
            }
            
            //отправить всем игрокам позиции всех объектов
            foreach (var entity in alivePlayers)
            {
                int tickNumber = gameStateHistory.GetLastTickNumber();
                float tickTime = gameStateHistory.GetLastTickTime();
                if (tickNumber!=0 && Math.Abs(tickTime) < 0.01)
                {
                    log.Debug($"Пустое время tickNumber = {tickNumber} tickTime = {tickTime}");
                }
                udpSendUtils.SendPositions(matchId, entity.player.id, allGos, tickNumber, tickTime);
            }
        }
    }
}