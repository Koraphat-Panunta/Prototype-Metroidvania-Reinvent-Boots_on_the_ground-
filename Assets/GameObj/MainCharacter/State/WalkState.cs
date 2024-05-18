using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State
{
    private float TopSpeed = 2;
    private float AcceletorSpeed = 0.2f;
    private float DeceletorSpeed = 0.2f;
    private Rigidbody2D rb;

    
    public WalkState(Animator animator, GameObject Char) : base(animator, Char)
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
        if(Character.MyDirection == Character.Direction.Left) 
        {           
            if(Character.Velocity > -TopSpeed) 
            {
                Character.Velocity -= AcceletorSpeed;
            }
            else if(Character.Velocity < -TopSpeed)
            {
                Character.Velocity += DeceletorSpeed;
            }
        }
        if(Character.MyDirection == Character.Direction.Right) 
        {
            if (rb.velocity.x < TopSpeed)
            {
                Character.Velocity += AcceletorSpeed;
            }
            else if (Character.Velocity > TopSpeed)
            {
                Character.Velocity -= DeceletorSpeed;
            }
        }
        rb.velocity = new Vector2(Character.Velocity / rb.mass, rb.velocity.y);
        base.PhysicUpdateState();
    }
    protected override void SetStateLevel()
    {
        base.StateLevle = stateAbleToBypass.Full;
    }
}
