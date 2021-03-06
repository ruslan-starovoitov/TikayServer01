//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public SpecialShooterComponent specialShooter { get { return (SpecialShooterComponent)GetComponent(GameComponentsLookup.SpecialShooter); } }
    public bool hasSpecialShooter { get { return HasComponent(GameComponentsLookup.SpecialShooter); } }

    public void AddSpecialShooter(SpecialShooter newValue) {
        var index = GameComponentsLookup.SpecialShooter;
        var component = (SpecialShooterComponent)CreateComponent(index, typeof(SpecialShooterComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceSpecialShooter(SpecialShooter newValue) {
        var index = GameComponentsLookup.SpecialShooter;
        var component = (SpecialShooterComponent)CreateComponent(index, typeof(SpecialShooterComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveSpecialShooter() {
        RemoveComponent(GameComponentsLookup.SpecialShooter);
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

    static Entitas.IMatcher<GameEntity> _matcherSpecialShooter;

    public static Entitas.IMatcher<GameEntity> SpecialShooter {
        get {
            if (_matcherSpecialShooter == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.SpecialShooter);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherSpecialShooter = matcher;
            }

            return _matcherSpecialShooter;
        }
    }
}
