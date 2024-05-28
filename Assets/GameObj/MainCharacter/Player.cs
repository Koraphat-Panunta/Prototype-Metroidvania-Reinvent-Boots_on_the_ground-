using Assets.GameObj.MainCharacter.State;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : Character
{
    private Attack_1 attack_1;
    private Attack_2 attack_2;
    private Attack_3 attack_3;
    private CrouchAttackState CrouchAttack;
    private JumpAttack JumpAttack;
    private Attack_Run attack_run;

    public enum IsHitWall 
    {
        None,
        Left,
        Right,
    }
    public enum IsHitObjectClimbAble 
    {
        None,
        Left,
        Right,
    }
    public IsHitWall HitWall = IsHitWall.None;
    public IsHitObjectClimbAble HitObjectClimbAble = IsHitObjectClimbAble.None;
    public JumpGroundUp JumpGroundUp { get;private set; }
    public JumpWallBack JumpWallBack { get;private set; }
    public JumpWallSide JumpWallSide { get; private set; }
    public EjectWallSide EjectWallSide { get; private set; }
    public Climb Climb { get; private set; }
    public DropDown dropDown { get;private set; }

    protected override void SetupState()
    {
        Idle = new IdleState(MyAnimator, MyCharacter);
        Walk = new WalkState(MyAnimator, MyCharacter);
        Sprint = new SprintState(MyAnimator, MyCharacter);
        Attack = new AttackState(MyAnimator, MyCharacter, Attack_box);
        Crouch = new CrouchState(MyAnimator, MyCharacter);
        dropDown = new DropDown(MyAnimator, MyCharacter);
        Fall = new FallState(MyAnimator, MyCharacter);

        attack_1 = new Attack_1(MyAnimator, MyCharacter, Attack_box);
        attack_2 = new Attack_2(MyAnimator, MyCharacter, Attack_box);
        attack_3 = new Attack_3(MyAnimator, MyCharacter, Attack_box);
        CrouchAttack = new CrouchAttackState(MyAnimator, MyCharacter, Attack_box);
        JumpAttack = new JumpAttack(MyAnimator, MyCharacter, Attack_box);
        attack_run = new Attack_Run(MyAnimator, MyCharacter, Attack_box);

        JumpGroundUp = new JumpGroundUp(MyAnimator, MyCharacter);
        JumpWallBack = new JumpWallBack(MyAnimator, MyCharacter);
        JumpWallSide = new JumpWallSide(MyAnimator, MyCharacter);
        EjectWallSide = new EjectWallSide(MyAnimator, MyCharacter);
        Climb = new Climb(MyAnimator, MyCharacter);
        base.SetupState();
    }
    protected override void Start()
    {
        IsControlbyPlayer = true;
        HeathPoint = 2;
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
        CheckCharacterCollider();
        base.FixedUpdate();
    }
    protected override void PerformedState()
    {
        PerformedDropDown();
        PerformedClimb(ClimbObject);
        base.PerformedState();
    }

    //Update State Character
    override protected void PerformedIdle()
    {
        if (CharacterStateMachine.Current_state == Idle)
        {
            if (IsGround())
            {
                if (Input.GetKeyDown(KeyCode.Space)) 
                {
                    Jump = JumpGroundUp;
                    CharacterStateMachine.ChangeState(Jump);
                }
                else if (Input.GetKey(KeyCode.J))
                {
                    Attack = attack_1;
                    CharacterStateMachine.ChangeState(Attack);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    CharacterStateMachine.ChangeState(Crouch);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    MyDirection = Direction.Left;
                    CharacterStateMachine.ChangeState(Walk);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    MyDirection = Direction.Right;
                    CharacterStateMachine.ChangeState(Walk);
                }     
            }
            else if (Characterground == CharacterGround.Air) 
            {
                AccesstoStateCrossraod();
            }
        }
    }
    override protected void PerformedRun()
    {
        if (CharacterStateMachine.Current_state == Walk)
        {
            if (IsGround())
            {
                if (Input.GetKey(KeyCode.J))
                {
                    Attack = attack_1;
                    CharacterStateMachine.ChangeState(Attack);
                }
                else if (Input.GetKeyDown(KeyCode.Space)) 
                {
                    Jump = JumpGroundUp;
                    CharacterStateMachine.ChangeState(Jump);
                }
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    CharacterStateMachine.ChangeState(Sprint);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    MyDirection = Direction.Left;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    MyDirection = Direction.Right;
                }
                else 
                {
                    CharacterStateMachine.ChangeState(Idle);
                }
            }
            else if(Characterground == CharacterGround.Air) 
            {
                AccesstoStateCrossraod();
            }
        }
    }
    override protected void PerformedSprint()
    {
        if (CharacterStateMachine.Current_state == Sprint)
        {
            if (IsGround()) 
            {
                if (Input.GetKey(KeyCode.LeftShift) == false)
                {
                    AccesstoStateCrossraod();
                }
                else if (Input.GetKey(KeyCode.J))
                {
                    Attack = attack_run;
                    CharacterStateMachine.ChangeState(Attack);
                }
                else if (Input.GetKeyDown(KeyCode.Space)) 
                {
                    Jump = JumpGroundUp;
                    CharacterStateMachine.ChangeState(Jump);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    MyDirection = Direction.Left;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    MyDirection = Direction.Right;
                }
                else 
                {
                    AccesstoStateCrossraod();
                }
               
            }
            else if(IsGround() == false) 
            {
                AccesstoStateCrossraod();
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
                if (Input.GetKeyDown(KeyCode.J))
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
            else if (Attack.IsExit == true)
            {
                AccesstoStateCrossraod();
            }
        }
    }
    override protected void PerformedCrouch()
    {
        if (CharacterStateMachine.Current_state == Crouch)
        {
            if (IsGround() == true)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {

                    Attack = CrouchAttack;
                    CharacterStateMachine.ChangeState(Attack);
                }
                else if (Input.GetKeyDown(KeyCode.Space)&&Characterground == CharacterGround.Platform) 
                {
                    CharacterStateMachine.ChangeState(dropDown);
                }
                else if (Input.GetKey(KeyCode.S) == false)
                {
                    AccesstoStateCrossraod();
                }
            }
            else if(IsGround() == false) 
            {
                AccesstoStateCrossraod();
            }

        }
    }
    override protected void PerformedJump()
    {
        if (CharacterStateMachine.Current_state == Jump)
        {
            if (IsGround() == false)
            {
                if (Input.GetKey(KeyCode.J))
                {
                    Attack = JumpAttack;
                    CharacterStateMachine.ChangeState(Attack);
                }
                else if(Input.GetKeyDown(KeyCode.Space)) 
                {
                    if (JumpWallBack.JumpAble == true && IsOnWallBack == true)
                    {
                        Jump = JumpWallBack;
                        CharacterStateMachine.ChangeState(Jump);
                    }
                    else if(JumpWallSide.JumpAble == true&&HitWall!= IsHitWall.None) 
                    {
                       if(HitWall == IsHitWall.Left&&MyDirection == Direction.Left) 
                       {
                            if(Input.GetKey(KeyCode.A)&&Input.GetKey(KeyCode.W)) 
                            {
                                Jump = JumpWallSide;
                                CharacterStateMachine.ChangeState(Jump);    
                            }
                            else if (Input.GetKey(KeyCode.A)) 
                            {
                                Jump = EjectWallSide;
                                CharacterStateMachine.ChangeState(Jump);
                            }
                       }
                       else if(HitWall == IsHitWall.Right&& MyDirection == Direction.Right) 
                       {
                            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
                            {
                                Jump = JumpWallSide;
                                CharacterStateMachine.ChangeState(Jump);
                            }
                            else if (Input.GetKey(KeyCode.D))
                            {
                                Jump = EjectWallSide;
                                CharacterStateMachine.ChangeState(Jump);
                            }
                        }
                    }
                }
                else if(HitObjectClimbAble != IsHitObjectClimbAble.None&&Input.GetKey(KeyCode.E)) 
                {
                    CharacterStateMachine.ChangeState(Climb);
                }
                if (CharacterStateMachine.Current_state == Jump && Jump.IsExit == true)
                {
                    AccesstoStateCrossraod();
                }
            }
            else if (CharacterStateMachine.Current_state == Jump && IsGround() && Jump.Jumpphase == JumpState.JumpPhase.Jumping)
            {
                AccesstoStateCrossraod();
            }
        }


    }
    public double Airtime = 0;
    override protected void PerformedFall()
    {
        if (CharacterStateMachine.Current_state == Fall)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack = JumpAttack;
                CharacterStateMachine.ChangeState(Attack);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (JumpWallBack.JumpAble == true && IsOnWallBack == true)
                {
                    Jump = JumpWallBack;
                    CharacterStateMachine.ChangeState(Jump);
                }
                else if (JumpWallSide.JumpAble == true && HitWall != IsHitWall.None)
                {
                    if (HitWall == IsHitWall.Left && MyDirection == Direction.Left)
                    {
                        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
                        {
                            Jump = JumpWallSide;
                            CharacterStateMachine.ChangeState(Jump);
                        }
                        else if (Input.GetKey(KeyCode.A))
                        {
                            Jump = EjectWallSide;
                            CharacterStateMachine.ChangeState(Jump);
                        }
                    }
                    else if (HitWall == IsHitWall.Right && MyDirection == Direction.Right)
                    {
                        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
                        {
                            Jump = JumpWallSide;
                            CharacterStateMachine.ChangeState(Jump);
                        }
                        else if (Input.GetKey(KeyCode.D))
                        {
                            Jump = EjectWallSide;
                            CharacterStateMachine.ChangeState(Jump);
                        }
                    }
                }
            }
            else if (HitObjectClimbAble != IsHitObjectClimbAble.None && Input.GetKey(KeyCode.E))
            {
                CharacterStateMachine.ChangeState(Climb);
            }
            if (CharacterStateMachine.Current_state == Fall&& IsGround())
            {
                AccesstoStateCrossraod();
            }
        }

    }
    protected void PerformedDropDown() 
    {
        if(CharacterStateMachine.Current_state == dropDown) 
        {
            Hitted_box.isTrigger = true;
            if(dropDown.IsExit == true) 
            {
                AccesstoStateCrossraod();
            }
        }
    }
    protected void PerformedClimb(Collider2D collider) 
    {
        if(CharacterStateMachine.Current_state == Climb) 
        {
            if (Input.GetKey(KeyCode.W))
            {
                Climb.ClimbUp();
            }
            else if (Input.GetKey(KeyCode.S)) 
            {
                Climb.ClimbDown();
            }
            else if (Input.GetKeyDown(KeyCode.Space)) 
            {
                if(MyDirection == Direction.Left&&Input.GetKey(KeyCode.A)) 
                {
                    Jump = EjectWallSide;
                    CharacterStateMachine.ChangeState(Jump);
                }
                else if(MyDirection == Direction.Right && Input.GetKey(KeyCode.D)) 
                {
                    Jump = EjectWallSide;
                    CharacterStateMachine.ChangeState(Jump);
                }
                else 
                {
                    Jump = JumpWallSide;
                    CharacterStateMachine.ChangeState(Jump);
                }
            }
            else 
            {
                Climb.ClimbHang();
            }
            if(HitObjectClimbAble == IsHitObjectClimbAble.Left) 
            {
                MyDirection = Direction.Left;
                gameObject.transform.position = new Vector2(collider.transform.position.x+0.45F,gameObject.transform.position.y);
            }
            if(HitObjectClimbAble == IsHitObjectClimbAble.Right) 
            {
                MyDirection = Direction.Right;
                gameObject.transform.position = new Vector2(collider.transform.position.x-0.45F, gameObject.transform.position.y);
            }
            
        }
    }
    override public void AccesstoStateCrossraod()
    {
        if (IsGround())
        {
            if (Input.GetKey(KeyCode.S))
            {
                CharacterStateMachine.ChangeState(Crouch);
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
            {
                CharacterStateMachine.ChangeState(Sprint);
                MyDirection = Direction.Left;
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
            {
                CharacterStateMachine.ChangeState(Sprint);
                MyDirection = Direction.Right;
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
        else 
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack = JumpAttack;
                CharacterStateMachine.ChangeState(Attack);
            }
            else 
            {
                CharacterStateMachine.ChangeState(Fall);
            }
        }
    }
    private void CheckCharacterCollider() 
    {
        List<Collider2D> colliders = new List<Collider2D>();
        MyRigidbody2D.GetContacts(colliders);
        if (colliders.Count > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Wall"))
                {
                    if (collider.transform.position.x < gameObject.transform.position.x)
                    {
                        HitWall = IsHitWall.Left;
                    }
                    else if (collider.transform.position.x > gameObject.transform.position.x)
                    {
                        HitWall = IsHitWall.Right;
                    }
                }
                else
                {
                    HitWall = IsHitWall.None;
                }
                if (collider.CompareTag("ClimbObject"))
                {
                    if (collider.transform.position.x < gameObject.transform.position.x)
                    {
                        HitObjectClimbAble = IsHitObjectClimbAble.Left;
                        ClimbObject = collider;
                    }
                    else if (collider.transform.position.x > gameObject.transform.position.x)
                    {
                        HitObjectClimbAble = IsHitObjectClimbAble.Right;
                        ClimbObject = collider;
                    }
                }
                else
                {
                    HitObjectClimbAble = IsHitObjectClimbAble.None;
                    ClimbObject = null;
                }
                if (collider.CompareTag("Ground"))
                {
                    this.JumpWallSide.SetJumpAble(true);
                    Characterground = CharacterGround.Ground;
                    JumpWallBack.SetJumpAble(true);
                    FootsAngle = collider.transform.rotation.eulerAngles.z;
                    if (FootsAngle == 0)
                    {
                        Characterground = CharacterGround.Ground;
                    }
                    else
                    {
                        Characterground = CharacterGround.SlopePlatform;
                    }
                    break;
                }
                else if (collider.CompareTag("Platform"))
                {

                    if (MyRigidbody2D.angularVelocity <= 0 && gameObject.transform.position.y > collider.transform.position.y)
                    {
                        Characterground = CharacterGround.Platform;
                        JumpWallBack.SetJumpAble(true);
                        JumpWallSide.SetJumpAble(true);
                        FootsAngle = collider.transform.rotation.eulerAngles.z;
                        if (FootsAngle == 0)
                        {

                        }
                        else
                        {

                        }
                        break;
                    }
                    else
                    {
                        Characterground = CharacterGround.Air;
                    }
                }
                else
                {
                    Characterground = CharacterGround.Air;
                }
            }
        }
        else
        {
            HitWall = IsHitWall.None;
            Characterground = CharacterGround.Air;
            HitObjectClimbAble = IsHitObjectClimbAble.None;
            ClimbObject = null;
        }
        if (Characterground == CharacterGround.SlopePlatform && CharacterStateMachine.Current_state == Idle)
        {
            MyRigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            MyRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }
        if (Characterground == CharacterGround.Air)
        {
            Airtime += 1;
            MyRigidbody2D.drag = 0;
            if (HitWall != IsHitWall.None || HitObjectClimbAble != IsHitObjectClimbAble.None)
            {
                Hitted_box.isTrigger = false;
            }
            else
            {
                Hitted_box.isTrigger = true;
            }
        }
        else
        {
            Airtime = 0;
            MyRigidbody2D.drag = 1;
            if (CharacterStateMachine.Current_state == dropDown)
            {
                Hitted_box.isTrigger = true;
            }
            else
            {
                Hitted_box.isTrigger = false;
            }
            if (CharacterStateMachine.Current_state != Jump)
            {
                jumpCount = 2;
            }
        }
    }
    private Collider2D ClimbObject { get; set; }
}

