﻿using Entitas;
using System.Collections.Generic;
using System.Linq;
using Server.GameEngine;
using Server.GameEngine.Chronometers;
using UnityEngine;

public sealed class TargetPointMovingSystem : IExecuteSystem
{
    private readonly List<GameEntity> buffer;
    private readonly GameContext gameContext;
    private readonly IGroup<GameEntity> movingGroup;
    private const float positionSqrDelta = 0.01f;
    private const float angleDelta = 5f;

    public TargetPointMovingSystem(Contexts contexts)
    {
        buffer = new List<GameEntity>();
        gameContext = contexts.game;
        var matcher = GameMatcher.AllOf(GameMatcher.Position, GameMatcher.Direction, GameMatcher.MaxVelocity, GameMatcher.TargetMovingPoint).NoneOf(GameMatcher.Unmovable);
        movingGroup = gameContext.GetGroup(matcher);
    }

    public void Execute()
    {
        foreach (var e in movingGroup.GetEntities(buffer))
        {
            var delta = e.targetMovingPoint.position - e.GetGlobalPositionVector2(gameContext);
            var deltaSqrMagnitude = delta.sqrMagnitude;
            if (deltaSqrMagnitude <= positionSqrDelta)
            {
                e.RemoveTargetMovingPoint();
                continue;
            }

            var newVelocity = delta / Chronometer.DeltaTime;
            if (!e.isDirectionTargetingShooting)
            {
                var directionAngle = Mathf.Atan2(newVelocity.y, newVelocity.x) * Mathf.Rad2Deg;
                if (directionAngle < 0) directionAngle += 360f;
                if (e.hasDirectionTargeting)
                {
                    e.ReplaceDirectionTargeting(directionAngle);
                }
                else
                {
                    e.AddDirectionTargeting(directionAngle);
                }
            }

            if (e.hasChaser && !e.GetAllChildrenGameEntities(gameContext, c => c.hasCannon).Any())
            {
                var directedVelocity = delta.magnitude * CoordinatesExtensions.GetRotatedUnitVector2(e.GetGlobalAngle(gameContext));
                var angle = Vector2.Angle(newVelocity, directedVelocity);
                if(angle > angleDelta)
                {
                    newVelocity = directedVelocity * (1f - angle / 180f);
                }
            }

            if (e.hasVelocity)
            {
                e.ReplaceVelocity(newVelocity);
            }
            else
            {
                e.AddVelocity(newVelocity);
            }
        }
    }
}
