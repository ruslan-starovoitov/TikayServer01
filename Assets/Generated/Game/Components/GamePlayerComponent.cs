//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PlayerComponent player { get { return (PlayerComponent)GetComponent(GameComponentsLookup.Player); } }
    public bool hasPlayer { get { return HasComponent(GameComponentsLookup.Player); } }

    public void AddPlayer(string newGoogleId, int newPlayerId) {
        var index = GameComponentsLookup.Player;
        var component = (PlayerComponent)CreateComponent(index, typeof(PlayerComponent));
        component.GoogleId = newGoogleId;
        component.PlayerId = newPlayerId;
        AddComponent(index, component);
    }

    public void ReplacePlayer(string newGoogleId, int newPlayerId) {
        var index = GameComponentsLookup.Player;
        var component = (PlayerComponent)CreateComponent(index, typeof(PlayerComponent));
        component.GoogleId = newGoogleId;
        component.PlayerId = newPlayerId;
        ReplaceComponent(index, component);
    }

    public void RemovePlayer() {
        RemoveComponent(GameComponentsLookup.Player);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiInterfaceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity : IPlayerEntity { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherPlayer;

    public static Entitas.IMatcher<GameEntity> Player {
        get {
            if (_matcherPlayer == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Player);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPlayer = matcher;
            }

            return _matcherPlayer;
        }
    }
}
