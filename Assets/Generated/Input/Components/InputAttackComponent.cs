//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public AttackComponent attack { get { return (AttackComponent)GetComponent(InputComponentsLookup.Attack); } }
    public bool hasAttack { get { return HasComponent(InputComponentsLookup.Attack); } }

    public void AddAttack(float newDirection) {
        var index = InputComponentsLookup.Attack;
        var component = (AttackComponent)CreateComponent(index, typeof(AttackComponent));
        component.direction = newDirection;
        AddComponent(index, component);
    }

    public void ReplaceAttack(float newDirection) {
        var index = InputComponentsLookup.Attack;
        var component = (AttackComponent)CreateComponent(index, typeof(AttackComponent));
        component.direction = newDirection;
        ReplaceComponent(index, component);
    }

    public void RemoveAttack() {
        RemoveComponent(InputComponentsLookup.Attack);
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
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherAttack;

    public static Entitas.IMatcher<InputEntity> Attack {
        get {
            if (_matcherAttack == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.Attack);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherAttack = matcher;
            }

            return _matcherAttack;
        }
    }
}
