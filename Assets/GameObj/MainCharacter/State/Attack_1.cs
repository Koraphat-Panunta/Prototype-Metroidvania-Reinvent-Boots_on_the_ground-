using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_1 : AttackState
{
    
    public Attack_1(Animator animator, GameObject Char, Collider2D Hitbox) : base(animator, Char, Hitbox)
    {
        
    }

    public override void EnterState()
    {
        SetAttackVariable();
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

    protected override void SetAttackVariable()
    {
        PreAttackTiming = 9;
        AttackingTiming = 17;
        PostAttackTiming = 36;
        AnimationName = "Attack 1";
        base.SetAttackVariable();
    }
}
