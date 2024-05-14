using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBack : State
{
    private float CurrentSpeed;
    private float TopSpeed = 10;
    private float AcceletorSpeed = 6;
    private Rigidbody2D rb;
    public WalkBack(Animator animator, GameObject Char) : base(animator, Char)
    {
        rb = Char.GetComponent<Rigidbody2D>();
    }
    public override void EnterState()
    {
        if (Character.Sprint != null)
        {
            if (Character.Sprint.IsExit == true)
            {
                Animation.Play("Post-Run");
            }
            else
            {
                Animation.Play("Walk");
            }
        }
        else
        {
            Animation.Play("Walk");
        }
        base.EnterState();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdateState()
    {
        if (Animation.GetCurrentAnimatorStateInfo(0).IsName("Post-Run"))
        {
            if (Animation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Animation.Play("Walk");
            }
        }
        base.FrameUpdateState();
    }
    public override void PhysicUpdateState()
    {
        float SpeedDeadZone = 1;
        if (Character.MyDirection == Character.Direction.Left)
        {
            if (rb.velocity.x < TopSpeed)
            {
                rb.AddForce(new Vector2(rb.mass * AcceletorSpeed, 0));
            }
            else if (rb.velocity.x > TopSpeed + SpeedDeadZone)
            {

            }
            else
            {
                rb.velocity = new Vector2(TopSpeed, 0);
            }
        }
        if (Character.MyDirection == Character.Direction.Right)
        {
            if (rb.velocity.x > -TopSpeed)
            {
                rb.AddForce(new Vector2(-rb.mass * AcceletorSpeed, 0));
            }
            else if (rb.velocity.x < -TopSpeed - SpeedDeadZone)
            {

            }
            else
            {
                rb.velocity = new Vector2(-TopSpeed, 0);
            }
        }
        CurrentSpeed = rb.velocity.x;
        base.PhysicUpdateState();
    }
}
