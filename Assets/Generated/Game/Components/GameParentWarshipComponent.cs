//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ParentWarshipComponent parentWarship { get { return (ParentWarshipComponent)GetComponent(GameComponentsLookup.ParentWarship); } }
    public bool hasParentWarship { get { return HasComponent(GameComponentsLookup.ParentWarship); } }

    public void AddParentWarship(GameEntity newEntity) {
        var index = GameComponentsLookup.ParentWarship;
        var component = (ParentWarshipComponent)CreateComponent(index, typeof(ParentWarshipComponent));
        component.entity = newEntity;
        AddComponent(index, component);
    }

    public void ReplaceParentWarship(GameEntity newEntity) {
        var index = GameComponentsLookup.ParentWarship;
        var component = (ParentWarshipComponent)CreateComponent(index, typeof(ParentWarshipComponent));
        component.entity = newEntity;
        ReplaceComponent(index, component);
    }

    public void RemoveParentWarship() {
        RemoveComponent(GameComponentsLookup.ParentWarship);
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

    static Entitas.IMatcher<GameEntity> _matcherParentWarship;

    public static Entitas.IMatcher<GameEntity> ParentWarship {
        get {
            if (_matcherParentWarship == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ParentWarship);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherParentWarship = matcher;
            }

            return _matcherParentWarship;
        }
    }
}
