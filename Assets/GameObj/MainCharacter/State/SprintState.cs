using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SprintState : State
{
    private float TopSpeed = 3;
    private float AcceletorSpeed = 0.5f;
    private float DeceletorSpeed = 0.2f;
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
            if (Character.Velocity > -TopSpeed)
            {
                Character.Velocity -= AcceletorSpeed;
            }
            else if (Character.Velocity < -TopSpeed)
            {
                Character.Velocity += DeceletorSpeed;
            }
        }
        if (Character.MyDirection == Character.Direction.Right)
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
        rb.AddForce(new Vector2(0, -20));
        base.PhysicUpdateState();
    }

    protected override void SetStateLevel()
    {
        base.StateLevle = stateAbleToBypass.Full;
    }
}
