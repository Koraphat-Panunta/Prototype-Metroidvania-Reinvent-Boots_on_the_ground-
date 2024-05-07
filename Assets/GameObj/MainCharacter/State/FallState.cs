using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState :State
{
    
    public FallState(Animator animator, GameObject Char) : base(animator, Char)
    {
       
    }
    public override void EnterState()
    {
        Animation.Play("Fall");
        base.EnterState();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdateState()
    {
        base.FrameUpdateState();
    }
    public override void PhysicUpdateState()
    {
        base.PhysicUpdateState();
    }
}
