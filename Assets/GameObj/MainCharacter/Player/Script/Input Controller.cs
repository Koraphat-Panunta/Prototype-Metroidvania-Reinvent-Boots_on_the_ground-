using Assets.GameObj.MainCharacter.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Character;
using static Player;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    [SerializeField] private Player Player;
    [SerializeField] private CheckPlayerCollider PlayerCollider;
    void Start()
    {
        Player = GetComponent<Player>();
        PlayerCollider = GetComponent<CheckPlayerCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        PerformedIdle();
        PerformedRun();
        PerformedSprint();
        PerformedAttack();
        PerformedCrouch();
        PerformedJump();
        PerformedFall();
        PerformedDropDown();
        PerformedClimb(PlayerCollider.ClimbObject);
    }
    protected void PerformedIdle()
    {
        if (Player.CharacterStateMachine.Current_state == Player.Idle)
        {
            if (PlayerCollider.IsGround())
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Player.Jump = Player.JumpGroundUp;
                    Player.CharacterStateMachine.ChangeState(Player.Jump);
                }
                else if (Input.GetKey(KeyCode.J))
                {
                    Player.Attack = Player.attack_1;
                    Player.CharacterStateMachine.ChangeState(Player.Attack);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    Player.CharacterStateMachine.ChangeState(Player.Crouch);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    Player.MyDirection = Direction.Left;
                    Player.CharacterStateMachine.ChangeState(Player.Walk);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    Player.MyDirection = Direction.Right;
                    Player.CharacterStateMachine.ChangeState(Player.Walk);
                }
            }
            else if (PlayerCollider.Characterground == CheckPlayerCollider.CharacterGround.Air)
            {
                AccesstoStateCrossraod();
            }
        }
    }
    protected void PerformedRun()
    {
        if (Player.CharacterStateMachine.Current_state == Player.Walk)
        {
            if (PlayerCollider.IsGround())
            {
                if (Input.GetKey(KeyCode.J))
                {
                    Player.Attack = Player.attack_1;
                    Player.CharacterStateMachine.ChangeState(Player.Attack);
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    Player.Jump = Player.JumpGroundUp;
                    Player.CharacterStateMachine.ChangeState(Player.Jump);
                }
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    Player.CharacterStateMachine.ChangeState(Player.Sprint);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    Player.MyDirection = Direction.Left;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    Player.MyDirection = Direction.Right;
                }
                else
                {
                    Player.CharacterStateMachine.ChangeState(Player.Idle);
                }
            }
            else if (PlayerCollider.Characterground == CheckPlayerCollider.CharacterGround.Air)
            {
                AccesstoStateCrossraod();
            }
        }
    }
    protected void PerformedSprint()
    {
        if (Player.CharacterStateMachine.Current_state == Player.Sprint)
        {
            if (PlayerCollider.IsGround())
            {
                if (Input.GetKey(KeyCode.LeftShift) == false)
                {
                    AccesstoStateCrossraod();
                }
                else if (Input.GetKey(KeyCode.J))
                {
                    Player.Attack = Player.attack_run;
                    Player.CharacterStateMachine.ChangeState(Player.Attack);
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    Player.Jump = Player.JumpGroundUp;
                    Player.CharacterStateMachine.ChangeState(Player.Jump);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    Player.MyDirection = Direction.Left;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    Player.MyDirection = Direction.Right;
                }
                else
                {
                    AccesstoStateCrossraod();
                }

            }
            else if (PlayerCollider.IsGround() == false)
            {
                AccesstoStateCrossraod();
            }

        }
    }
    protected void PerformedAttack()
    {
        if (Player.CharacterStateMachine.Current_state == Player.Attack)
        {
            if (Player.Attack.CurrentAttackPhase == AttackState.AttackPhase.PreAttack)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    Player.MyDirection = Direction.Left;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    Player.MyDirection = Direction.Right;
                }
            }
            else if (Player.Attack.CurrentAttackPhase == AttackState.AttackPhase.PostAttack)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    if (Player.Attack == Player.attack_1)
                    {
                        Player.Attack = Player.attack_2;
                        Player.CharacterStateMachine.ChangeState(Player.Attack);
                    }
                    else if (Player.Attack == Player.attack_2)
                    {
                        Player.Attack = Player.attack_3;
                        Player.CharacterStateMachine.ChangeState(Player.Attack);
                    }
                }

            }
            else if (Player.Attack.IsExit == true)
            {
                AccesstoStateCrossraod();
            }
        }
    }
    protected void PerformedCrouch()
    {
        if (Player.CharacterStateMachine.Current_state == Player.Crouch)
        {
            if (PlayerCollider.IsGround() == true)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {

                    Player.Attack = Player.CrouchAttack;
                    Player.CharacterStateMachine.ChangeState(Player.Attack);
                }
                else if (Input.GetKeyDown(KeyCode.Space) && PlayerCollider.Characterground == CheckPlayerCollider.CharacterGround.Platform)
                {
                    Player.CharacterStateMachine.ChangeState(Player.dropDown);
                }
                else if (Input.GetKey(KeyCode.S) == false)
                {
                    AccesstoStateCrossraod();
                }
            }
            else if (PlayerCollider.IsGround() == false)
            {
                AccesstoStateCrossraod();
            }

        }
    }
    protected void PerformedJump()
    {
        if (Player.CharacterStateMachine.Current_state == Player.Jump)
        {
            if (PlayerCollider.IsGround() == false)
            {
                if (Input.GetKey(KeyCode.J))
                {
                    Player.Attack = Player.JumpAttack;
                    Player.CharacterStateMachine.ChangeState(Player.Attack);
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (Player.JumpWallBack.JumpAble == true && Player.IsOnWallBack == true)
                    {
                        Player.Jump = Player.JumpWallBack;
                        Player.CharacterStateMachine.ChangeState(Player.Jump);
                    }
                    else if (Player.JumpWallSide.JumpAble == true && PlayerCollider.HitWall != CheckPlayerCollider.IsHitWall.None)
                    {
                        if (PlayerCollider.HitWall == CheckPlayerCollider.IsHitWall.Left && Player.MyDirection == Direction.Left)
                        {
                            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
                            {
                                Player.Jump = Player.JumpWallSide;
                                Player.CharacterStateMachine.ChangeState(Player.Jump);
                            }
                            else if (Input.GetKey(KeyCode.A))
                            {
                                Player.Jump = Player.EjectWallSide;
                                Player.CharacterStateMachine.ChangeState(Player.Jump);
                            }
                        }
                        else if (PlayerCollider.HitWall == CheckPlayerCollider.IsHitWall.Right && Player.MyDirection == Direction.Right)
                        {
                            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
                            {
                                Player.Jump = Player.JumpWallSide;
                                Player.CharacterStateMachine.ChangeState(Player.Jump);
                            }
                            else if (Input.GetKey(KeyCode.D))
                            {
                                Player.Jump = Player.EjectWallSide;
                                Player.CharacterStateMachine.ChangeState(Player.Jump);
                            }
                        }
                    }
                }
                else if (PlayerCollider.HitObjectClimbAble != CheckPlayerCollider.IsHitObjectClimbAble.None && Input.GetKey(KeyCode.E))
                {
                    Player.CharacterStateMachine.ChangeState(Player.Climb);
                }
                if (Player.CharacterStateMachine.Current_state == Player.Jump && Player.Jump.IsExit == true)
                {
                    AccesstoStateCrossraod();
                }
            }
            else if (Player.CharacterStateMachine.Current_state == Player.Jump && PlayerCollider.IsGround() && Player.Jump.Jumpphase == JumpState.JumpPhase.Jumping)
            {
                AccesstoStateCrossraod();
            }
        }


    }
    protected void PerformedFall()
    {
        if (Player.CharacterStateMachine.Current_state == Player.Fall)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Player.Attack = Player.JumpAttack;
                Player.CharacterStateMachine.ChangeState(Player.Attack);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Player.JumpWallBack.JumpAble == true && Player.IsOnWallBack == true)
                {
                    Player.Jump = Player.JumpWallBack;
                    Player.CharacterStateMachine.ChangeState(Player.Jump);
                }
                else if (Player.JumpWallSide.JumpAble == true && PlayerCollider.HitWall != CheckPlayerCollider.IsHitWall.None)
                {
                    if (PlayerCollider.HitWall == CheckPlayerCollider.IsHitWall.Left && Player.MyDirection == Direction.Left)
                    {
                        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
                        {
                            Player.Jump = Player.JumpWallSide;
                            Player.CharacterStateMachine.ChangeState(Player.Jump);
                        }
                        else if (Input.GetKey(KeyCode.A))
                        {
                            Player.Jump = Player.EjectWallSide;
                            Player.CharacterStateMachine.ChangeState(Player.Jump);
                        }
                    }
                    else if (PlayerCollider.HitWall == CheckPlayerCollider.IsHitWall.Right && Player.MyDirection == Direction.Right)
                    {
                        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
                        {
                            Player.Jump = Player.JumpWallSide;
                            Player.CharacterStateMachine.ChangeState(Player.Jump);
                        }
                        else if (Input.GetKey(KeyCode.D))
                        {
                            Player.Jump = Player.EjectWallSide;
                            Player.CharacterStateMachine.ChangeState(Player.Jump);
                        }
                    }
                }
            }
            else if (PlayerCollider.HitObjectClimbAble != CheckPlayerCollider.IsHitObjectClimbAble.None && Input.GetKey(KeyCode.E))
            {
                Player.CharacterStateMachine.ChangeState(Player.Climb);
            }
            if (Player.CharacterStateMachine.Current_state == Player.Fall && PlayerCollider.IsGround())
            {
                AccesstoStateCrossraod();
            }
        }

    }
    protected void PerformedDropDown()
    {
        if (Player.CharacterStateMachine.Current_state == Player.dropDown)
        {
            Player.Hitted_box.isTrigger = true;
            if (Player.dropDown.IsExit == true)
            {
                AccesstoStateCrossraod();
            }
        }
    }
    protected void PerformedClimb(Collider2D collider)
    {
        if (Player.CharacterStateMachine.Current_state == Player.Climb)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Player.Climb.ClimbUp();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Player.Climb.ClimbDown();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Player.MyDirection == Direction.Left && Input.GetKey(KeyCode.A))
                {
                    Player.Jump = Player.EjectWallSide;
                    Player.CharacterStateMachine.ChangeState(Player.Jump);
                }
                else if (Player.MyDirection == Direction.Right && Input.GetKey(KeyCode.D))
                {
                    Player.Jump = Player.EjectWallSide;
                    Player.CharacterStateMachine.ChangeState(Player.Jump);
                }
                else
                {
                    Player.Jump = Player.JumpWallSide;
                    Player.CharacterStateMachine.ChangeState(Player.Jump);
                }
            }
            else
            {
                Player.Climb.ClimbHang();
            }
            if (PlayerCollider.HitObjectClimbAble == CheckPlayerCollider.IsHitObjectClimbAble.Left)
            {
                Player.MyDirection = Direction.Left;
                gameObject.transform.position = new Vector2(collider.transform.position.x + 0.45F, gameObject.transform.position.y);
            }
            if (PlayerCollider.HitObjectClimbAble == CheckPlayerCollider.IsHitObjectClimbAble.Right)
            {
                Player.MyDirection = Direction.Right;
                gameObject.transform.position = new Vector2(collider.transform.position.x - 0.45F, gameObject.transform.position.y);
            }

        }
    }
    public void AccesstoStateCrossraod()
    {
        if (PlayerCollider.IsGround())
        {
            if (Input.GetKey(KeyCode.S))
            {
                Player.CharacterStateMachine.ChangeState(Player.Crouch);
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
            {
                Player.CharacterStateMachine.ChangeState(Player.Sprint);
                Player.MyDirection = Direction.Left;
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
            {
                Player.CharacterStateMachine.ChangeState(Player.Sprint);
                Player.MyDirection = Direction.Right;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Player.CharacterStateMachine.ChangeState(Player.Walk);
                Player.MyDirection = Direction.Left;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Player.CharacterStateMachine.ChangeState(Player.Walk);
                Player.MyDirection = Direction.Right;
            }
            else
            {
                Player.CharacterStateMachine.ChangeState(Player.Idle);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Player.Attack = Player.JumpAttack;
                Player.CharacterStateMachine.ChangeState(Player.Attack);
            }
            else
            {
                Player.CharacterStateMachine.ChangeState(Player.Fall);
            }
        }
    }
}
