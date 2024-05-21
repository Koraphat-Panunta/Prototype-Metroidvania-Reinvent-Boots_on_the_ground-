using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState :State
{
    
    public FallState(Animator animator, GameObject Char) : base(animator, Char)
    {
       
    }
    public override void EnterState()
    {
        Animation.Play("Fall");
        base.EnterState();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&Character.jumpCount > 0) 
        {
            Character.CharacterStateMachine.ChangeState(Character.Jump);
            if( Character.TryGetComponent<Player>(out Player player)) 
            {
                player.Airtime = 0;
            }
        }
        base.FrameUpdateState();
    }
    public override void PhysicUpdateState()
    {
        base.PhysicUpdateState();
    }
    protected override void SetStateLevel()
    {
        base.StateLevle = stateAbleToBypass.Semi;
    }
}
