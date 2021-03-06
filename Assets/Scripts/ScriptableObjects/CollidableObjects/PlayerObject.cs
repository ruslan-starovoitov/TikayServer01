﻿using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "BaseObjects/Player", order = 55)]
public class PlayerObject : MovableWithHealthObject
{
    public AbilityInfo ability;

    public override void FillEntity(GameContext context, GameEntity entity)
    {
        base.FillEntity(context, entity);
        ability.AddAbilityToEntity(entity);
        entity.isBonusPickable = true;
        entity.isSingleTargeting = true;
    }
}
