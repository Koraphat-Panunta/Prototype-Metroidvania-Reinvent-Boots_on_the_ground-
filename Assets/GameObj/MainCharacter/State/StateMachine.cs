using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine 
{
    public State Current_state;
    public StateMachine(State StartState) 
    {
        Current_state = StartState;
    }
    public virtual void UpdateState() 
    {
        Current_state.FrameUpdateState();
    }
    public virtual void FixedStateUpdate() 
    {
        Current_state.PhysicUpdateState();
    }
    public void ChangeState(State Nextstate) 
    {
        Current_state.ExitState();
        Current_state = Nextstate;
        Current_state.EnterState();
    }
}
