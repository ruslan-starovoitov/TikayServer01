﻿using UnityEngine;

[CreateAssetMenu(fileName = "NewBaseHealthObject", menuName = "BaseObjects/BaseHealthObject", order = 52)]
public class BaseWithHealthObject : BaseObject
{
    [Min(0)]
    public float maxHealthPoints;

    public override void FillEntity(GameContext context, GameEntity entity)
    {
        base.FillEntity(context, entity);
        entity.AddHealthPoints(maxHealthPoints);
        entity.AddMaxHealthPoints(maxHealthPoints);
    }
}
