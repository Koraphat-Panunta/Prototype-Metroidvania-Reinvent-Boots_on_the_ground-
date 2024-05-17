using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] protected float Pressure = 0;
    public float Distance;

    private AttackNormal_1 AttackNormal1;
    private WalkBack WalkBack;
    public enum Enemy_Role 
    {
        Stand,
        Chaseplayer,
        Reteat,
    }
    public Enemy_Role CurrentEnemyRole;
    protected override void SetupState()
    {
        Idle = new IdleState(MyAnimator,MyCharacter);
        Walk = new WalkState(MyAnimator,MyCharacter);
        WalkBack = new WalkBack(MyAnimator,MyCharacter);
        Attack = new AttackState(MyAnimator,MyCharacter,Attack_box);   

        AttackNormal1 = new AttackNormal_1 (MyAnimator,MyCharacter,Attack_box);
        base.SetupState();
    }
    protected override void Start()
    {
        CurrentEnemyRole = Enemy_Role.Stand;
        base.Start();
    }

    protected override void Update()
    {       
        base.Update();        
    }
    protected override void FixedUpdate()
    {
        if (Pressure > 0)
        {
            if (CharacterStateMachine.Current_state != Attack)
            {
                Pressure -= 0.5f;
            }
        }
        if (Pressure < 30) 
        {
            CurrentEnemyRole = Enemy_Role.Chaseplayer;
        }
        else if(Pressure >= 70) 
        {
            CurrentEnemyRole = Enemy_Role.Reteat;
        }
        CalculateDistanceTarget();
        RoleManager();
        base.FixedUpdate();
        DectionalTarget("Player");
    }
    protected override void PerformedState()
    {
        base.PerformedState();
    }
    private void CalculateDistanceTarget() 
    {
        if(Target != null) 
        {
            Distance = math.abs(Target.transform.position.x - MyCharacter.transform.position.x);
        }
    }
    private void RoleManager() 
    {
        if(CurrentEnemyRole == Enemy_Role.Stand) 
        {
            if(CharacterStateMachine.Current_state == Idle) 
            {
                
            }
        }
        if(CurrentEnemyRole == Enemy_Role.Reteat) 
        {
            if(CharacterStateMachine.Current_state == Walk) 
            {
                if (Target != null)
                {
                    if ((MyCharacter.transform.position.x - Target.transform.position.x) > 0)
                    {
                        MyDirection = Direction.Left;
                    }
                    if ((MyCharacter.transform.position.x - Target.transform.position.x) < 0)
                    {
                        MyDirection = Direction.Right;
                    }
                }
                CharacterStateMachine.ChangeState(WalkBack);
            }
            if (CharacterStateMachine.Current_state == WalkBack)
            {
                if (Target != null)
                {
                    if ((MyCharacter.transform.position.x - Target.transform.position.x) > 0)
                    {
                        MyDirection = Direction.Left;
                    }
                    if ((MyCharacter.transform.position.x - Target.transform.position.x) < 0)
                    {
                        MyDirection = Direction.Right;
                    }
                }
            }
        }
        if(CurrentEnemyRole == Enemy_Role.Chaseplayer) 
        {
            if(CharacterStateMachine.Current_state == Idle) 
            {
                if (Distance > 1)
                {
                    if (Target != null)
                    {
                        if ((MyCharacter.transform.position.x - Target.transform.position.x) > 0)
                        {
                            MyDirection = Direction.Left;
                        }
                        if ((MyCharacter.transform.position.x - Target.transform.position.x) < 0)
                        {
                            MyDirection = Direction.Right;
                        }
                    }
                    CharacterStateMachine.ChangeState(Walk);
                }
                else if(Distance < 1)
                {
                    if (Target != null)
                    {
                        Attack = AttackNormal1;
                        CharacterStateMachine.ChangeState(Attack);
                    }
                }
            }
            if(CharacterStateMachine.Current_state == Walk) 
            {
                if (Target != null)
                {
                    if ((MyCharacter.transform.position.x - Target.transform.position.x) > 0)
                    {
                        MyDirection = Direction.Left;
                    }
                    if ((MyCharacter.transform.position.x - Target.transform.position.x) < 0)
                    {
                        MyDirection = Direction.Right;
                    }
                }
                if (Distance < 1) 
                {
                    CharacterStateMachine.ChangeState(Idle);
                }
            }
            if(CharacterStateMachine.Current_state == WalkBack) 
            {
                if (Target != null)
                {
                    if ((MyCharacter.transform.position.x - Target.transform.position.x) > 0)
                    {
                        MyDirection = Direction.Left;
                    }
                    if ((MyCharacter.transform.position.x - Target.transform.position.x) < 0)
                    {
                        MyDirection = Direction.Right;
                    }
                }
                CharacterStateMachine.ChangeState(Walk);
            }
        }
    }
    protected override void PerformedAttack()
    {
        if(CharacterStateMachine.Current_state == Attack) 
        {
            if (Attack.IsExit == true) 
            {
                AccesstoStateCrossraod();
            }
            if(Attack.IsEnter == true) 
            {
                Pressure += 25;
            }
        }
        base.PerformedAttack();
    }
    public override void AccesstoStateCrossraod()
    {
        if(CurrentEnemyRole == Enemy_Role.Stand) 
        {
            CharacterStateMachine.ChangeState(Idle);
        }
        if(CurrentEnemyRole == Enemy_Role.Chaseplayer) 
        {
            CharacterStateMachine.ChangeState(Walk);
        }
        if(CurrentEnemyRole == Enemy_Role.Reteat) 
        {
            CharacterStateMachine.ChangeState(WalkBack);
        }
        
 
        base.AccesstoStateCrossraod();
    }
    

}
