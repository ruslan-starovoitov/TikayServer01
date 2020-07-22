//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public LocalDirectionTargetingComponent localDirectionTargeting { get { return (LocalDirectionTargetingComponent)GetComponent(GameComponentsLookup.LocalDirectionTargeting); } }
    public bool hasLocalDirectionTargeting { get { return HasComponent(GameComponentsLookup.LocalDirectionTargeting); } }

    public void AddLocalDirectionTargeting(float newAngle) {
        var index = GameComponentsLookup.LocalDirectionTargeting;
        var component = (LocalDirectionTargetingComponent)CreateComponent(index, typeof(LocalDirectionTargetingComponent));
        component.angle = newAngle;
        AddComponent(index, component);
    }

    public void ReplaceLocalDirectionTargeting(float newAngle) {
        var index = GameComponentsLookup.LocalDirectionTargeting;
        var component = (LocalDirectionTargetingComponent)CreateComponent(index, typeof(LocalDirectionTargetingComponent));
        component.angle = newAngle;
        ReplaceComponent(index, component);
    }

    public void RemoveLocalDirectionTargeting() {
        RemoveComponent(GameComponentsLookup.LocalDirectionTargeting);
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

    static Entitas.IMatcher<GameEntity> _matcherLocalDirectionTargeting;

    public static Entitas.IMatcher<GameEntity> LocalDirectionTargeting {
        get {
            if (_matcherLocalDirectionTargeting == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.LocalDirectionTargeting);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherLocalDirectionTargeting = matcher;
            }

            return _matcherLocalDirectionTargeting;
        }
    }
}
