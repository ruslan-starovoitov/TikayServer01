﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class ParentsExtensions
{
    public static bool IsParentOf(this GameEntity e1, GameEntity e2, GameContext context)
    {
        var firstParent = e2;
        while (firstParent.hasParent)
        {
            if (firstParent.parent.id == e1.id.value) return true;
            firstParent = context.GetEntityWithId(firstParent.parent.id);
        }

        return false;
    }

    public static GameEntity GetGrandParent(this GameEntity entity, GameContext context)
    {
        var firstParent = entity;
        while (firstParent.hasParent)
        {
            firstParent = context.GetEntityWithId(firstParent.parent.id);
        }

        return firstParent;
    }

    public static GameEntity GetGrandOwner(this GameEntity entity, GameContext context)
    {
        var result = entity.GetGrandParent(context);
        while (result.hasOwner)
        {
            result = context.GetEntityWithId(result.owner.id);
        }

        return result;
    }

    public static bool TryGetFirstGameEntity(this GameEntity entity, GameContext context, Predicate<GameEntity> predicate, out GameEntity result)
    {
        result = entity;
        while (result.hasParent)
        {
            if (predicate(result)) return true;
            result = context.GetEntityWithId(result.parent.id);
        }

        return predicate(result);
    }

    public static IEnumerable<GameEntity> GetAllChildrenGameEntities(this GameEntity entity, GameContext context, Predicate<GameEntity> predicate)
    {
        if (predicate(entity)) yield return entity;
        var children = context.GetEntitiesWithParent(entity.id.value);
        foreach (var childGameEntity in children.SelectMany(child => GetAllChildrenGameEntities(child, context, predicate)))
        {
            yield return childGameEntity;
        }
    }
}