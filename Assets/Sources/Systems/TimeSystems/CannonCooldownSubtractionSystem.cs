﻿using System.Collections;
using System.Collections.Generic;
using Entitas;
using Server.GameEngine;
using Server.GameEngine.Chronometers;
using UnityEngine;

public sealed class CannonCooldownSubtractionSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> cooldownGroup;

    public CannonCooldownSubtractionSystem(Contexts contexts)
    {
        cooldownGroup = contexts.game.GetGroup(GameMatcher.CannonCooldown);
    }

    public void Execute()
    {
        foreach (var e in cooldownGroup)
        {
            e.ReplaceCannonCooldown(e.cannonCooldown.value - Chronometer.DeltaTime);
        }
    }
}
