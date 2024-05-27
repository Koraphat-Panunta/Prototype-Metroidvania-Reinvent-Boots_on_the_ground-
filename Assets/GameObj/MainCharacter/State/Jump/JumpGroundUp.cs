using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpGroundUp : JumpState
{
    public JumpGroundUp(Animator animator, GameObject Char) : base(animator, Char)
    {

    }
    public override void EnterState()
    {
        base.Animation.Play("Jump");
        base.Jumpphase = JumpPhase.PreJump;
        base.Aniamtion_frame = 0;
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdateState()
    {
        if (base.Aniamtion_frame == 5)
        {
            rb.AddForce(new Vector2(0, JUMPFORCE));
        }
        if (base.Character.IsGround() == false)
        {
            Jumpphase = JumpPhase.Jumping;
        }
        else if (base.Aniamtion_frame > 40)
        {
            Jumpphase = JumpPhase.Jumping;
        }
        if (base.Animation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
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
