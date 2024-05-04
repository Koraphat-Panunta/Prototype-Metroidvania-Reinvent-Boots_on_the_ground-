using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Run : AttackState
{
    public Attack_Run(Animator animator, GameObject Char, Collider2D Hitbox) : base(animator, Char, Hitbox)
    {
    }
    public override void EnterState()
    {
        ObjCharacter.GetComponent<Rigidbody2D>().drag = 0f;
        SetAttackVariable();
        base.EnterState();
    }

    public override void ExitState()
    {
        ObjCharacter.GetComponent<Rigidbody2D>().drag = 2f;
        base.ExitState();
    }

    public override void FrameUpdateState()
    {
        //float forcepush = 10;
        //if(AttackFrame == 1) 
        //{
        //    if (Character.MyDirection == Character.Direction.Left)
        //    {
        //        ObjCharacter.GetComponent<Rigidbody2D>().AddForce(new Vector2(-forcepush,0));
        //    }
        //    else if(Character.MyDirection == Character.Direction.Right) 
        //    {
        //        ObjCharacter.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcepush, 0));
        //    }
        //}
       
        base.FrameUpdateState();
    }

    public override void PhysicUpdateState()
    {
        base.PhysicUpdateState();
    }

    protected override void SetAttackVariable()
    {
        PreAttackTiming = 11;
        AttackingTiming = 18;
        PostAttackTiming = 41;
        AnimationName = "Run-Attack";
        base.SetAttackVariable();
    }

}
