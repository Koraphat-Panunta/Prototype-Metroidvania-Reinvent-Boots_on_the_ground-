using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpWallSide : JumpState
{
    public JumpWallSide(Animator animator, GameObject Char) : base(animator, Char)
    {

    }
    public override void EnterState()
    {
        base.Animation.Play("Jump");
        base.Aniamtion_frame = 0;
        base.Jumpphase = JumpPhase.Jumping;
        base.EnterState();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdateState()
    {
        if(Aniamtion_frame == 5) 
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, JUMPFORCE));
        }
        else if(Aniamtion_frame >= 40) 
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
        base.SetStateLevel();
    }
}
