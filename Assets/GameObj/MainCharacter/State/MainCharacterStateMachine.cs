using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterStateMachine : StateMachine
{ 
    public MainCharacterStateMachine(State state) : base(state) 
    {
        Current_state = state;
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
