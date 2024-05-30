using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Climb : State
{
    Rigidbody2D MyRigidbody;
    CheckPlayerCollider PlayerCollider;
    float HighestPos;
    float LowesrtPos;

    public Climb(Animator animator, GameObject Char) : base(animator, Char)
    {
        MyRigidbody = Char.GetComponent<Rigidbody2D>();
        if(Player != null) 
        {
            PlayerCollider = Player.GetComponent<CheckPlayerCollider>();
        }
    }

    public override void EnterState()
    {
        MyRigidbody.gravityScale = 0;
        base.EnterState();
    }

    public override void ExitState()
    {
        MyRigidbody.gravityScale = 1;
        base.ExitState();
    }

    public override void FrameUpdateState()
    {
        if(PlayerCollider.ClimbObject == null) 
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
    public void ClimbUp() 
    {
        MyRigidbody.velocity = new Vector2(0, 3);
        Animation.Play("ClimbUp");
    }
    public void ClimbDown() 
    {       
        MyRigidbody.velocity = new Vector2(0, -3);
        Animation.Play("ClimbDown");
    }
    public void ClimbHang() 
    {
        MyRigidbody.velocity = Vector2.zero;
        Animation.Play("ClimbHang");
    }
}
