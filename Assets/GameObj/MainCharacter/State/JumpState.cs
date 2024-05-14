using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    private Rigidbody2D rb;
    private int Aniamtion_frame = 0;
    public enum JumpPhase 
    {
        None,
        PreJump,
        Jumping
    }
    public JumpPhase Jumpphase;
    public JumpState(Animator animator, GameObject Char) : base(animator, Char)
    {
        rb = Char.GetComponent<Rigidbody2D>();
    }
    public override void EnterState()
    {
        Animation.Play("Jump");
        Jumpphase = JumpPhase.PreJump;
        Aniamtion_frame = 0;
        base.EnterState();
    }

    public override void ExitState()
    {
        Jumpphase = JumpPhase.None;
        base.ExitState();
    }

    public override void FrameUpdateState()
    {
        float Jumpforce = 700;
        Aniamtion_frame += 1;
        if(Aniamtion_frame == 5) 
        {
            rb.AddForce(new Vector2(0, Jumpforce));
            Jumpphase = JumpPhase.Jumping;
        }
        base.FrameUpdateState();
    }

    public override void PhysicUpdateState()
    {
        base.PhysicUpdateState();
    }
}
