using Assets.GameObj.MainCharacter.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Attack_1 attack_1;
    private Attack_2 attack_2;
    private Attack_3 attack_3;
    private CrouchAttackState CrouchAttack;
    private JumpAttack JumpAttack;
    private Attack_Run attack_run;
    protected override void SetupState()
    {
        Idle = new IdleState(MyAnimator, MyCharacter);
        Walk = new WalkState(MyAnimator, MyCharacter);
        Sprint = new SprintState(MyAnimator, MyCharacter);
        Attack = new AttackState(MyAnimator, MyCharacter, Attack_box);
        Crouch = new CrouchState(MyAnimator, MyCharacter);
        Jump = new JumpState(MyAnimator, MyCharacter);
        Fall = new FallState(MyAnimator, MyCharacter);

        attack_1 = new Attack_1(MyAnimator, MyCharacter, Attack_box);
        attack_2 = new Attack_2(MyAnimator, MyCharacter, Attack_box);
        attack_3 = new Attack_3(MyAnimator, MyCharacter, Attack_box);
        CrouchAttack = new CrouchAttackState(MyAnimator, MyCharacter, Attack_box);
        JumpAttack = new JumpAttack(MyAnimator, MyCharacter, Attack_box);
        attack_run = new Attack_Run(MyAnimator, MyCharacter, Attack_box);
        base.SetupState();
    }
    protected override void Start()
    {
        base.Start();       
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    override protected void FixedUpdate()
    {
        DectionalTarget("Enemy");
        base.FixedUpdate();
    }
    protected override void PerformedState()
    {
        base.PerformedState();
    }

    //Update State Character
    override protected void PerformedIdle()
    {
        if (CharacterStateMachine.Current_state == Idle)
        {
            if (Input.GetKey(KeyCode.S))
            {
                CharacterStateMachine.ChangeState(Crouch);
            }
            if (Input.GetKey(KeyCode.A))
            {
                MyDirection = Direction.Left;
                CharacterStateMachine.ChangeState(Walk);
            }
            if (Input.GetKey(KeyCode.D))
            {
                MyDirection = Direction.Right;
                CharacterStateMachine.ChangeState(Walk);
            }
            if (Input.GetKey(KeyCode.J))
            {
                Attack = attack_1;
                CharacterStateMachine.ChangeState(Attack);
            }
        }
    }
    override protected void PerformedRun()
    {
        if (CharacterStateMachine.Current_state == Walk)
        {
            if (Input.anyKey == false)
            {
                CharacterStateMachine.ChangeState(Idle);
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    CharacterStateMachine.ChangeState(Sprint);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    MyDirection = Direction.Left;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    MyDirection = Direction.Right;
                }
                if (Input.GetKey(KeyCode.J))
                {
                    Attack = attack_1;
                    CharacterStateMachine.ChangeState(Attack);
                }
            }
        }
    }
    override protected void PerformedSprint()
    {
        if (CharacterStateMachine.Current_state == Sprint)
        {
            if (Input.anyKey == false)
            {
                CharacterStateMachine.ChangeState(Idle);
            }
            else
            {
                if (Input.GetKey(KeyCode.J))
                {
                    Attack = attack_run;
                    CharacterStateMachine.ChangeState(Attack);
                }
                if (Input.GetKey(KeyCode.LeftShift) == false)
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        CharacterStateMachine.ChangeState(Walk);
                        MyDirection = Direction.Left;
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        CharacterStateMachine.ChangeState(Walk);
                        MyDirection = Direction.Right;
                    }
                }
                else
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        MyDirection = Direction.Left;
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        MyDirection = Direction.Right;
                    }
                }
            }

        }
    }
    override protected void PerformedAttack()
    {
        if (CharacterStateMachine.Current_state == Attack)
        {
            if (Attack.CurrentAttackPhase == AttackState.AttackPhase.PreAttack)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    MyDirection = Direction.Left;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    MyDirection = Direction.Right;
                }
            }
            else if (Attack.CurrentAttackPhase == AttackState.AttackPhase.PostAttack)
            {
                if (Input.GetKey(KeyCode.J))
                {
                    if (Attack == attack_1)
                    {
                        Attack = attack_2;
                        CharacterStateMachine.ChangeState(Attack);
                    }
                    else if (Attack == attack_2)
                    {
                        Attack = attack_3;
                        CharacterStateMachine.ChangeState(Attack);
                    }
                }

            }
            else if (Attack.CurrentAttackPhase == AttackState.AttackPhase.None)
            {
                AccesstoStateCrossraod();
            }
        }
    }
    override protected void PerformedCrouch()
    {
        if (CharacterStateMachine.Current_state == Crouch)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {

                Attack = CrouchAttack;
                CharacterStateMachine.ChangeState(Attack);
            }
            else if (Input.GetKey(KeyCode.S) == false)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    MyDirection = Direction.Left;
                    CharacterStateMachine.ChangeState(Walk);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    MyDirection = Direction.Right;
                    CharacterStateMachine.ChangeState(Walk);
                }
                else
                {
                    CharacterStateMachine.ChangeState(Idle);
                }
            }

        }
    }
    override protected void PerformedJump()
    {
        if (CharacterStateMachine.Current_state == Idle || CharacterStateMachine.Current_state == Walk
            || CharacterStateMachine.Current_state == Sprint && (CharacterStateMachine.Current_state != Jump))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CharacterStateMachine.ChangeState(Jump);
            }
        }
        if (CharacterStateMachine.Current_state == Jump)
        {
            if (AngularVelocity < 0f)
            {
                CharacterStateMachine.ChangeState(Fall);
                Debug.Log("Fall");
            }
            if (Input.GetKey(KeyCode.J))
            {
                Attack = JumpAttack;
                CharacterStateMachine.ChangeState(Attack);
            }
        }

    }
    override protected void PerformedFall()
    {
        if (MyRigidbody2D.angularVelocity < 0f)
        {
            CharacterStateMachine.ChangeState(Fall);
        }
        if (CharacterStateMachine.Current_state == Fall)
        {
            if (Input.GetKey(KeyCode.J))
            {
                Attack = JumpAttack;
                CharacterStateMachine.ChangeState(Attack);
            }
            AccesstoStateCrossraod();
        }
    }
    override public void AccesstoStateCrossraod()
    {
        if (AngularVelocity < 0)
        {
            CharacterStateMachine.ChangeState(Fall);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            CharacterStateMachine.ChangeState(Crouch);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            CharacterStateMachine.ChangeState(Walk);
            MyDirection = Direction.Left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            CharacterStateMachine.ChangeState(Walk);
            MyDirection = Direction.Right;
        }
        else
        {
            CharacterStateMachine.ChangeState(Idle);
        }
    }
}

