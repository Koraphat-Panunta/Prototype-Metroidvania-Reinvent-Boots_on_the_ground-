using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDown : State
{
    int AnimateFrame;
    public DropDown(Animator animator, GameObject Char) : base(animator, Char)
    {
    }
    public override void EnterState()
    {
        base.Animation.Play("Fall");
        AnimateFrame = 0;
        Character.Hitted_box.isTrigger = true;
        base.EnterState();
    }

    public override void ExitState()
    {
        
        base.ExitState();
    }

    public override void FrameUpdateState()
    {
        AnimateFrame++;
        if(AnimateFrame >= 140) 
        {
            ExitState();
        }
        base.FrameUpdateState();
    }

    public override void PhysicUpdateState()
    {
        base.PhysicUpdateState();
    }

    protected override void SetStateLevel()
    {
        throw new System.NotImplementedException();
    }
}
