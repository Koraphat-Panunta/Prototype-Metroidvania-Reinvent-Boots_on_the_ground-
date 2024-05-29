using Assets.GameObj.MainCharacter.State;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static CheckPlayerCollider;

public class Player : Character
{
    //Define Character State
    [SerializeField] CheckPlayerCollider playerCollider;
   
    public WalkState Walk;
    public SprintState Sprint;
    public AttackState Attack;//Current Attack state
    public CrouchState Crouch;
    public JumpState Jump;
    public FallState Fall;

    public Attack_1 attack_1;
    public Attack_2 attack_2;
    public Attack_3 attack_3;
    public CrouchAttackState CrouchAttack;
    public JumpAttack JumpAttack;
    public Attack_Run attack_run;

   
    public JumpGroundUp JumpGroundUp { get;private set; }
    public JumpWallBack JumpWallBack { get;private set; }
    public JumpWallSide JumpWallSide { get; private set; }
    public EjectWallSide EjectWallSide { get; private set; }
    public Climb Climb { get; private set; }
    public DropDown dropDown { get;private set; }

    protected override void SetupState()
    {
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
        playerCollider = GetComponent<CheckPlayerCollider>();
        IsControlbyPlayer = true;
        HeathPoint = 2;
        base.Start();       
    }

    // Update is called once per frame
    override protected void Update()
    {
        if (CharacterStateMachine.Current_state != Walk && CharacterStateMachine.Current_state != Sprint && CharacterStateMachine.Current_state != Jump && CharacterStateMachine.Current_state != Fall)
        {
            Velocity = 0f;
        }
        if(playerCollider.IsGround()) 
        {
            JumpWallSide.SetJumpAble(true);
            JumpWallBack.SetJumpAble(true);
        }
        UpdateComponent();
        base.Update();
    }

    override protected void FixedUpdate()
    {
        DectionalTarget("Enemy");
        base.FixedUpdate();
    }
    private void UpdateComponent()
    {
        if (playerCollider.Characterground == CharacterGround.SlopePlatform && CharacterStateMachine.Current_state == Idle)
        {
            MyRigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            MyRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }
        if (playerCollider.Characterground == CharacterGround.Air)
        {
            playerCollider.Airtime += 1;
            MyRigidbody2D.drag = 0;
            if (playerCollider.HitWall != IsHitWall.None || playerCollider.HitObjectClimbAble != IsHitObjectClimbAble.None)
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
            playerCollider.Airtime = 0;
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



}

