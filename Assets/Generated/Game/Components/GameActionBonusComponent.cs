//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ActionBonusComponent actionBonus { get { return (ActionBonusComponent)GetComponent(GameComponentsLookup.ActionBonus); } }
    public bool hasActionBonus { get { return HasComponent(GameComponentsLookup.ActionBonus); } }

    public void AddActionBonus(System.Func<GameEntity, bool> newCheck, System.Action<GameEntity> newAction) {
        var index = GameComponentsLookup.ActionBonus;
        var component = (ActionBonusComponent)CreateComponent(index, typeof(ActionBonusComponent));
        component.check = newCheck;
        component.action = newAction;
        AddComponent(index, component);
    }

    public void ReplaceActionBonus(System.Func<GameEntity, bool> newCheck, System.Action<GameEntity> newAction) {
        var index = GameComponentsLookup.ActionBonus;
        var component = (ActionBonusComponent)CreateComponent(index, typeof(ActionBonusComponent));
        component.check = newCheck;
        component.action = newAction;
        ReplaceComponent(index, component);
    }

    public void RemoveActionBonus() {
        RemoveComponent(GameComponentsLookup.ActionBonus);
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

    static Entitas.IMatcher<GameEntity> _matcherActionBonus;

    public static Entitas.IMatcher<GameEntity> ActionBonus {
        get {
            if (_matcherActionBonus == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ActionBonus);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherActionBonus = matcher;
            }

            return _matcherActionBonus;
        }
    }
}
