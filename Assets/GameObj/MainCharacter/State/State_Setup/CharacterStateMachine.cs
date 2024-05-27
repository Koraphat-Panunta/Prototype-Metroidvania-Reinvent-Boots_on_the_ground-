using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine : StateMachine
{
    public CharacterStateMachine(State state) : base(state) 
    {
        ChangeState(state);
    }
    public override void UpdateState()
    {
        base.UpdateState();
    }
    public override void FixedStateUpdate()
    {
        base.FixedStateUpdate();
    }

}
