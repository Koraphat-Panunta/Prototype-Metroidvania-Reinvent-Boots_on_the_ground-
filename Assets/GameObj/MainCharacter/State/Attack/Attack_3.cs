using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GameObj.MainCharacter.State
{
    public class Attack_3 : AttackState
    {
        public Attack_3(Animator animator, GameObject Char, Collider2D Hitbox) : base(animator, Char, Hitbox)
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
            PreAttackTiming = 21;
            AttackingTiming = 28;
            PostAttackTiming = 40;
            AnimationName = "Attack 3";
            base.SetAttackVariable();
        }
    }
}
