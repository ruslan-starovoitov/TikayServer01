﻿using Entitas;
using Server.GameEngine;
using Server.GameEngine.Chronometers;

public sealed class AbilityCooldownSubtractionSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> cooldownGroup;

    public AbilityCooldownSubtractionSystem(Contexts contexts)
    {
        cooldownGroup = contexts.game.GetGroup(GameMatcher.AbilityCooldown);
    }

    public void Execute()
    {
        foreach (var e in cooldownGroup)
        {
            e.ReplaceAbilityCooldown(e.abilityCooldown.value - Chronometer.DeltaTime);
        }
    }
}
