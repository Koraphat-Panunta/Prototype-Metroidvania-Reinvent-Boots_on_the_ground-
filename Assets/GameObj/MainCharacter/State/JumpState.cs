using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    private Rigidbody2D rb;
    public int Aniamtion_frame { get;private set; }
    public const float JUMPFORCE = 230;
    private bool IsDoubleJump;
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
        Character.jumpCount -= 1;
        if (Character.Isground == true)
        {
            Animation.Play("Jump");
            Jumpphase = JumpPhase.PreJump;
            Aniamtion_frame = 0;
            IsDoubleJump = false;
        }
        else if(Character.Isground == false) 
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, JUMPFORCE));
            Animation.Play("Jump");
            Jumpphase = JumpPhase.PreJump;
            IsDoubleJump = true;
            Aniamtion_frame = 0;
        }
        if (Character.TryGetComponent<Player>(out Player player))
        {
            player.Airtime = 0;
        }
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
        if(Aniamtion_frame == 5&&IsDoubleJump == false) 
        {
            //rb.velocity = new Vector2(rb.velocity.x,Jumpforce);
            rb.AddForce(new Vector2(0,JUMPFORCE));
        }
        if(Character.Isground == false) 
        {
            Jumpphase = JumpPhase.Jumping;
        }
        else if(Aniamtion_frame > 40) 
        {
            Jumpphase = JumpPhase.Jumping;
        }
        if (Input.GetKeyDown(KeyCode.Space)&&Character.jumpCount>0) 
        {
            Character.CharacterStateMachine.ChangeState(this);
        }
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
}
