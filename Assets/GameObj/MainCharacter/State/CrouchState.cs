using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : State
{
    public CrouchState(Animator animator, GameObject Char) : base(animator, Char)
    {

    }
    public override void EnterState()
    {
        Animation.Play("Crouche");
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
