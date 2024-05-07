using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : AttackState
{
    public JumpAttack(Animator animator, GameObject Char, Collider2D Hitbox) : base(animator, Char, Hitbox)
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
        PreAttackTiming = 12;
        AttackingTiming = 22;
        PostAttackTiming = 34;
        AnimationName = "Jump-Attack";
        base.SetAttackVariable();
    }
}
