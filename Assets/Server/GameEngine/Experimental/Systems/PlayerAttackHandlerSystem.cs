﻿using System.Collections.Generic;
using Entitas;
using log4net;

namespace Server.GameEngine.Systems
{
    public class PlayerAttackHandlerSystem : ReactiveSystem<InputEntity>
    {
        private readonly GameContext gameContext;
        private static readonly ILog Log = LogManager.GetLogger(typeof(PlayerAttackHandlerSystem));
        
        public PlayerAttackHandlerSystem(Contexts contexts) : base(contexts.input)
        {
            gameContext = contexts.game;
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.Attack.Added());
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasAttack && entity.hasPlayer;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var inputEntity in entities)
            {
                var playerAttackDirection = inputEntity.attack.direction;

                var playerId = inputEntity.player.id;

                var gamePlayer = gameContext.GetEntityWithPlayer(playerId);

                if (gamePlayer == null)
                {
                    Log.Warn("Пришло сообщение об атаке от игрока, которого (уже) нет в комнате. Данные игнорируются.");
                    return;
                }

                if (float.IsNaN(playerAttackDirection))
                {
                    //if(gamePlayer.hasDirectionTargeting) gamePlayer.RemoveDirectionTargeting();
                    gamePlayer.isDirectionTargetingShooting = false;
                    continue;
                }

                if (playerAttackDirection < 0f)
                {
                    if (playerAttackDirection <= -360f)
                    {
                        playerAttackDirection %= 360;
                    }
                    playerAttackDirection += 360;
                }
                else if(playerAttackDirection >= 360f)
                {
                    playerAttackDirection %= 360;
                }

                if (gamePlayer.hasDirectionTargeting)
                {
                    gamePlayer.ReplaceDirectionTargeting(playerAttackDirection);
                }
                else
                {
                    gamePlayer.AddDirectionTargeting(playerAttackDirection);
                }

                gamePlayer.isDirectionTargetingShooting = true;
            }
        }
    }
}