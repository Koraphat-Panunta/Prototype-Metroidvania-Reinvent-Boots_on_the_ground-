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
        if (Character.Sprint.IsExit == true)
        {
            Animation.Play("Post-Run");
        }
        else
        {
            Animation.Play("Idle");
        }
        base.EnterState();
    }
    public override void FrameUpdateState()
    {
        if (Animation.GetCurrentAnimatorStateInfo(0).IsName("Post-Run")) 
        {
            if (Animation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) 
            {
                Animation.Play("Idle");
            }
        }
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
