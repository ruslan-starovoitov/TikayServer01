//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public AbilityCooldownComponent abilityCooldown { get { return (AbilityCooldownComponent)GetComponent(GameComponentsLookup.AbilityCooldown); } }
    public bool hasAbilityCooldown { get { return HasComponent(GameComponentsLookup.AbilityCooldown); } }

    public void AddAbilityCooldown(float newValue) {
        var index = GameComponentsLookup.AbilityCooldown;
        var component = (AbilityCooldownComponent)CreateComponent(index, typeof(AbilityCooldownComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAbilityCooldown(float newValue) {
        var index = GameComponentsLookup.AbilityCooldown;
        var component = (AbilityCooldownComponent)CreateComponent(index, typeof(AbilityCooldownComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAbilityCooldown() {
        RemoveComponent(GameComponentsLookup.AbilityCooldown);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherAbilityCooldown;

    public static Entitas.IMatcher<GameEntity> AbilityCooldown {
        get {
            if (_matcherAbilityCooldown == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AbilityCooldown);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAbilityCooldown = matcher;
            }

            return _matcherAbilityCooldown;
        }
    }
}
