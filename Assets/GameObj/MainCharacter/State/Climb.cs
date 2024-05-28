using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : State
{
    Rigidbody2D MyRigidbody;
    public Climb(Animator animator, GameObject Char) : base(animator, Char)
    {
        MyRigidbody = Char.GetComponent<Rigidbody2D>();
    }

    public override void EnterState()
    {
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

    protected override void SetStateLevel()
    {
        throw new System.NotImplementedException();
    }
    public void ClimbUp() 
    {
        MyRigidbody.velocity = new Vector2(0,3);
        Animation.Play("ClimbUp");
    }
    public void ClimbDown() 
    {
        MyRigidbody.velocity = new Vector2(0,-3);
        Animation.Play("ClimbDown");
    }
    public void ClimbHang() 
    {
        MyRigidbody.velocity = Vector2.zero;
        Animation.Play("ClimbHang");
    }
}
