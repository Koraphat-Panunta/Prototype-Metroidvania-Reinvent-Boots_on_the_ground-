using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    
    public IdleState(Animator animator,GameObject Char):base(animator,Char) 
    {
        
    }
    public override void EnterState()
    {
        Animation.Play("Idle");      
        base.EnterState();
    }
    public override void FrameUpdateState()
    {
        base.FrameUpdateState();
    }
    public override void PhysicUpdateState()
    {
        base.PhysicUpdateState();
    }
    public override void ExitState()
    {        
        base.ExitState();
    }
}
