﻿internal class EffectsSystems : Feature
{
    public EffectsSystems(Contexts contexts) : base("Effects Systems")
    {
        Add(new RemovingNegativeAbilityCooldownSystem(contexts));
        Add(new BonusApplyingSystem(contexts));
        Add(new BonusActingSystem(contexts));
        Add(new AuraDamageSystem(contexts));
        Add(new CircleScalingSystem(contexts));
        Add(new CircleTargetScalingCheckerSystem(contexts));
        Add(new DirectionSaverRemoverSystem(contexts));
    }
}
