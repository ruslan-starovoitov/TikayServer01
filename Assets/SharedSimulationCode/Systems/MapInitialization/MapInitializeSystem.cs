﻿// #define ONE_PLAYER

using Entitas;
using NetworkLibrary.NetworkLibrary.Http;
using UnityEngine;

namespace SharedSimulationCode.Systems.MapInitialization
{
    public class MapInitializeSystem:IInitializeSystem
    {
        private readonly Contexts contexts;
        private readonly BattleRoyaleMatchModel matchModel;

        public MapInitializeSystem(Contexts contexts, BattleRoyaleMatchModel matchModel)
        {
            this.contexts = contexts;
            this.matchModel = matchModel;
        }
        
        public void Initialize()
        {
            Vector3 position = new Vector3();
            
#if ONE_PLAYER
            var firstPlayer = matchModel.GameUnits.Players.First();
            Debug.LogError($"TemporaryId = "+firstPlayer.TemporaryId);
            Debug.LogError($"AccountId = "+firstPlayer.AccountId);
            CreatePlayer(position, firstPlayer.TemporaryId, firstPlayer.AccountId);
#else
            foreach (var player in matchModel.GameUnits.Players)
            {
                CreatePlayer(position, player.TemporaryId, player.AccountId);
                position = position + new Vector3(15, 0,15);
            }
            foreach (var botModel in matchModel.GameUnits.Bots)
            {
                var bot = CreatePlayer(position, botModel.TemporaryId, -botModel.TemporaryId);
                bot.isBot = true;
                position = position + new Vector3(15, 0,15);
            }
#endif
        }

        private GameEntity CreatePlayer(Vector3 position, ushort playerId, int accountId)
        {
            GameEntity entity = contexts.game.CreateEntity();
            entity.AddPlayer(playerId);
            entity.AddAccount(accountId);
            entity.AddHealthPoints(1999);
            entity.AddMaxHealthPoints(2000);
            entity.AddTeam((byte)(playerId+1));
            entity.AddViewType(ViewTypeId.StarSparrow);
            entity.AddSpawnPoint(position, Quaternion.identity);
            entity.isSpawnWarship = true;
            return entity;
        }
    }
}