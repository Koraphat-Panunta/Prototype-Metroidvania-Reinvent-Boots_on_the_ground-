using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    protected Rigidbody2D rb;
    public int Aniamtion_frame { get; protected set; }
    protected float JUMPFORCE = 230;
    public bool JumpAble { get; protected set; }
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
       JumpAble = false;
        base.EnterState();
    }

    public override void ExitState()
    {
        Jumpphase = JumpPhase.None;
        base.ExitState();
    }

    public override void FrameUpdateState()
    {
        Aniamtion_frame += 1;
       
        base.FrameUpdateState();
    }
   
    public override void PhysicUpdateState()
    {
        base.PhysicUpdateState();
    }
    protected override void SetStateLevel()
    {
        base.StateLevle = stateAbleToBypass.None;
    }
    public float GetJumpForce() 
    {
        return JUMPFORCE;
    }
    public void SetJumpAble(bool JumpAble) 
    {
        this.JumpAble = JumpAble;
    }
}
