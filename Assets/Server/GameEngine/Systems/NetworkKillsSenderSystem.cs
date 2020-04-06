﻿using System;
using Entitas;
using System.Collections.Generic;
using log4net;
using Server.GameEngine;
using Server.Http;
using Server.Udp.Sending;

//TODO если игровые юниты умерли в один кадр, то им можно дать одинаковое место

/// <summary>
/// Отвечает за отправку сообщения об убийствах.
/// </summary>
public class NetworkKillsSenderSystem : ReactiveSystem<GameEntity>
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(NetworkKillsSenderSystem));
    readonly IGroup<GameEntity> alivePlayersAndBots;
    private readonly IGroup<GameEntity> alivePlayers;
    private readonly Dictionary<int, (int playerId, ViewTypeId type)> killersInfo;
    readonly GameContext gameContext;
    
    public NetworkKillsSenderSystem(Contexts contexts, Dictionary<int, (int playerId, ViewTypeId type)> killersInfos)
        : base(contexts.game)
    {
        killersInfo = killersInfos;
        gameContext = contexts.game;
        alivePlayers = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Player).NoneOf(GameMatcher.Bot));
        alivePlayersAndBots = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Player).NoneOf(GameMatcher.KilledBy));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.KilledBy.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPlayer && entity.hasViewType && entity.hasKilledBy;
    }

    protected override void Execute(List<GameEntity> killedEntities)
    {
        int countOfAlivePlayersAndBots = alivePlayersAndBots.count;
        int countOfKilledEntities = killedEntities.Count;

        if (killedEntities.Count > 1)
        {
            Log.Warn($"killedEntities.Count = {killedEntities.Count}");
        }
        
        foreach (var player in alivePlayers)
        {
            for (var killedEntityIndex = 0; killedEntityIndex < killedEntities.Count; killedEntityIndex++)
            {
                GameEntity killedEntity = killedEntities[killedEntityIndex];
                if (!killersInfo.TryGetValue(killedEntity.killedBy.id, out var killerInfo))
                {
                    killerInfo = (0, 0);
                }

                KillData killData = new KillData
                {
                    TargetPlayerId = player.player.id,
                    KillerId = killerInfo.playerId,
                    KillerType = killerInfo.type,
                    VictimType = killedEntity.viewType.id,
                    VictimId = killedEntity.player.id
                };
                
                UdpSendUtils.SendKill(killData);

                if (!killedEntity.isBot)
                {
                    int playerTmpId = killedEntity.player.id;
                    if (!gameContext.hasMatch)
                    {
                        throw new Exception("gameContext do not have match data");
                    }

                    //TODO это говнище
                    var dich = GameEngineMediator.MatchStorageFacade.TryRemovePlayer(playerTmpId);
                    
                    int matchId = gameContext.match.MatchId;
                    int placeInBattle = GetPlaceInBattle(countOfAlivePlayersAndBots, countOfKilledEntities,
                        killedEntityIndex);
                    PlayerDeathData playerDeathData = new PlayerDeathData
                    {
                        PlayerId = playerTmpId,
                        PlaceInBattle = placeInBattle,
                        MatchId = matchId 
                    };
                    PlayerDeathNotifier.KilledPlayers.Enqueue(playerDeathData);
                    UdpSendUtils.SendBattleFinishMessage(killedEntity.player.id);    
                }
            }
        }
    }

    private int GetPlaceInBattle(int countOfAlivePlayersAndBots, int countOfKilledEntities, int killedEntityIndex)
    {
        return countOfAlivePlayersAndBots + 1 + (countOfKilledEntities - 1 - killedEntityIndex);
    }
}
