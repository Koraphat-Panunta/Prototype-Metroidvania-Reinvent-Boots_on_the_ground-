using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State
{
    //Default Value
    private float TopSpeed = 2;
    private float AcceletorSpeed = 0.2f;
    private float DeceletorSpeed = 0.2f;
    

    
    public WalkState(Animator animator, GameObject Char,float Speed,float Acceletor) : base(animator, Char)
    {
        TopSpeed = Speed;
        AcceletorSpeed = Acceletor;
        
    }
    public WalkState(Animator animator, GameObject Char) : base(animator, Char)
    {

    }

    public override void EnterState()
    {
        if (Player.Sprint != null)
        {
            if (Player.Sprint.IsExit == true)
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
            if (Character.MyRigidbody2D.velocity.x < TopSpeed)
            {
                Character.Velocity += AcceletorSpeed;
            }
            else if (Character.Velocity > TopSpeed)
            {
                Character.Velocity -= DeceletorSpeed;
            }
        }
        Character.MyRigidbody2D.velocity = new Vector2(Character.Velocity / Character.MyRigidbody2D.mass, Character.MyRigidbody2D.velocity.y);
        base.PhysicUpdateState();
    }
    protected override void SetStateLevel()
    {
        base.StateLevle = stateAbleToBypass.Full;
    }
}
