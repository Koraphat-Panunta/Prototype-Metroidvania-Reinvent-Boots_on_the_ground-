using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchAttackState : AttackState
{
    public CrouchAttackState(Animator animator, GameObject Char, Collider2D Hitbox) : base(animator, Char, Hitbox)
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
        PreAttackTiming = 5;
        AttackingTiming = 11;
        PostAttackTiming = 22;
        AnimationName = "Crouch-Attack";
        base.SetAttackVariable();
    }
}
