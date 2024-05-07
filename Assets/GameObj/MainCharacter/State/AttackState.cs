using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public enum AttackPhase 
    {
        PreAttack,
        Attacking,
        PostAttack,
        None
    }
    public AttackPhase CurrentAttackPhase = AttackPhase.None;

   
    public float PreAttackTiming; //Override
    public float AttackingTiming; //Override
    public float PostAttackTiming; //Override

    public Collider2D AttackBox;

    protected string AnimationName; //Override

    protected float AttackFrame;
    public AttackState(Animator animator, GameObject Char,Collider2D Hitbox) : base(animator, Char)
    {
        AttackBox = Hitbox;
    }

    public override void EnterState()
    {
        CurrentAttackPhase = AttackPhase.PreAttack;
        AttackFrame = 0;
        Animation.Play(AnimationName);
        SetAttackVariable();
        base.EnterState();
    }

    public override void ExitState()
    {
        CurrentAttackPhase = AttackPhase.None;
        base.ExitState();
    }

    public override void FrameUpdateState()
    {
        if (AttackFrame <= PreAttackTiming)
        {
            CurrentAttackPhase = AttackPhase.PreAttack;
        }
        else if (AttackFrame <= AttackingTiming)
        {
            CurrentAttackPhase = AttackPhase.Attacking;
            
        }
        else if (AttackFrame < PostAttackTiming)
        {
            CurrentAttackPhase = AttackPhase.PostAttack;
        }
        else 
        {
            ExitState();
        }

        if(CurrentAttackPhase == AttackPhase.Attacking) 
        {
            AttackBox.gameObject.SetActive(true);
            AttackBox.enabled = true;     
        }
        else
        {
            AttackBox.gameObject.SetActive(false);
            AttackBox.enabled = false;
        }
        base.FrameUpdateState();
    }

    public override void PhysicUpdateState()
    {
        AttackFrame += 1;
        base.PhysicUpdateState();
    }
    protected virtual void SetAttackVariable() 
    {
        
    }
}
