//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public CannonCooldownComponent cannonCooldown { get { return (CannonCooldownComponent)GetComponent(GameComponentsLookup.CannonCooldown); } }
    public bool hasCannonCooldown { get { return HasComponent(GameComponentsLookup.CannonCooldown); } }

    public void AddCannonCooldown(float newValue) {
        var index = GameComponentsLookup.CannonCooldown;
        var component = (CannonCooldownComponent)CreateComponent(index, typeof(CannonCooldownComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCannonCooldown(float newValue) {
        var index = GameComponentsLookup.CannonCooldown;
        var component = (CannonCooldownComponent)CreateComponent(index, typeof(CannonCooldownComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCannonCooldown() {
        RemoveComponent(GameComponentsLookup.CannonCooldown);
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

    static Entitas.IMatcher<GameEntity> _matcherCannonCooldown;

    public static Entitas.IMatcher<GameEntity> CannonCooldown {
        get {
            if (_matcherCannonCooldown == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CannonCooldown);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCannonCooldown = matcher;
            }

            return _matcherCannonCooldown;
        }
    }
}
