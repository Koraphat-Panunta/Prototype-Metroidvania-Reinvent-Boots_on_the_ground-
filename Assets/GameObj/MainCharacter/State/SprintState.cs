using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintState : State
{
    private float TopSpeed = 25;
    private float AcceletorSpeed = 10;
    private float CurrentSpeed;
    private Rigidbody2D rb;
    public enum SprintPhase 
    {
        Pre_run,
        Runing,
        None,
    }
    public SprintPhase sprintPhase = SprintPhase.None ;
    public SprintState(Animator animator, GameObject Char) : base(animator, Char)
    {
        rb = Char.GetComponent<Rigidbody2D>();
    }
    public override void EnterState()
    {
        if(Character.Idle.IsExit == true|| Character.Walk.IsExit == true) 
        {
            Animation.Play("Pre-Run");
            sprintPhase = SprintPhase.Pre_run ;
        }
        else 
        {
            sprintPhase = SprintPhase.Runing;
            Animation.Play("Run");
        }      
        base.EnterState();
    }

    public override void ExitState()
    {
        sprintPhase = SprintPhase.None;
        base.ExitState();
    }

    public override void FrameUpdateState()
    {
        base.FrameUpdateState();
        if(sprintPhase == SprintPhase.Pre_run) 
        {
            if (Animation.GetCurrentAnimatorStateInfo(0).normalizedTime>=1f)
            {
                sprintPhase = SprintPhase.Runing;
                Animation.Play("Run");
            }
        }
        
    }

    public override void PhysicUpdateState()
    {
        if (Character.MyDirection == Character.Direction.Left)
        {
            if (rb.velocity.x > -TopSpeed)
            {
                rb.AddForce(new Vector2(-rb.mass * AcceletorSpeed, 0));
            }
            else
            {
                rb.velocity = new Vector2(-TopSpeed, 0);
            }
        }
        if (Character.MyDirection == Character.Direction.Right)
        {
            if (rb.velocity.x < TopSpeed)
            {
                rb.AddForce(new Vector2(rb.mass * AcceletorSpeed, 0));
            }
            else
            {
                rb.velocity = new Vector2(TopSpeed, 0);
            }
        }
        CurrentSpeed = rb.velocity.x;
        base.PhysicUpdateState();
        base.PhysicUpdateState();
    }
}
