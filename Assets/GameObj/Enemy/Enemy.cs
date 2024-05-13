using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : Character
{
    protected float Pressure = 0;
    public float Distance;
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
        if (Pressure < 30) 
        {
            CurrentEnemyRole = Enemy_Role.Chaseplayer;
        }
        DectionalTarget("Player");
        CalculateDistanceTarget();
        RoleManager();
        base.FixedUpdate();
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
            
        }
        if(CurrentEnemyRole == Enemy_Role.Chaseplayer) 
        {
            if(CharacterStateMachine.Current_state == Idle) 
            {
                if (Distance > 1)
                {
                    if ((MyCharacter.transform.position.x - Target.transform.position.x) > 0)
                    {
                        MyDirection = Direction.Left;
                    }
                    if ((MyCharacter.transform.position.x - Target.transform.position.x) < 0)
                    {
                        MyDirection = Direction.Right;
                    }
                    CharacterStateMachine.ChangeState(Walk);
                }
            }
            if(CharacterStateMachine.Current_state == Walk) 
            {
                if(Distance < 1) 
                {
                    CharacterStateMachine.ChangeState(Idle);
                }
            }
        }
    }
    
}
