using Assets.GameObj.MainCharacter.State;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour 
{
    // Start is called before the first frame update
    public Animator MyAnimator;
    public Rigidbody2D MyRigidbody2D;
    public GameObject Player;
    public GameObject Target;
    public Collider2D Attack_box;
    public Collider2D Hitted_box;
    public Collider2D IsGroundbox;

    private float AngularVelocity;
    private float PreviosPositionY;

    public MainCharacterStateMachine CharacterStateMachine;

    public IdleState Idle;
    public WalkState Walk;
    public SprintState Sprint;
    public AttackState Attack;

    private Attack_1 attack_1;
    private Attack_2 attack_2;
    private Attack_3 attack_3;

    private Attack_Run attack_run;

    private CrouchState Crouch;
    private JumpState Jump;
    private FallState Fall;
    public enum Direction 
    {
        Left,
        Right
    }
    
    public Direction MyDirection;

    void Start()
    {
        Player = gameObject;
        MyDirection = Direction.Right;
        Attack_box.enabled = false;

        Idle = new IdleState(MyAnimator, Player);
        Walk = new WalkState(MyAnimator, Player);
        Sprint = new SprintState(MyAnimator, Player);
        Attack = new AttackState(MyAnimator, Player, Attack_box);

        attack_1 = new Attack_1(MyAnimator, Player, Attack_box);
        attack_2 = new Attack_2(MyAnimator, Player, Attack_box);
        attack_3 = new Attack_3(MyAnimator, Player, Attack_box);

        attack_run = new Attack_Run(MyAnimator, Player, Attack_box);

        Crouch = new CrouchState(MyAnimator, Player);
        Jump = new JumpState(MyAnimator, Player);
        Fall = new FallState(MyAnimator, Player);

        CharacterStateMachine = new MainCharacterStateMachine(Idle);
        

        Attack_box.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {   
        PerformedIdle();
        PerformedRun();
        PerformedSprint();
        PerformedAttack();
        PerformedCrouch();
        PerformedJump();
        PerformedFall();
        
        CharacterStateMachine.UpdateState();

        
    }
   
    public void FixedUpdate()
    {
        CharacterStateMachine.FixedStateUpdate();
        DirectionManagement();
        DectionalTarget();
        CalAngularVelocity(PreviosPositionY);
    }
    //Behavior
    
    private void DirectionManagement() 
    {
        
        if (MyDirection == Direction.Left)
        {
           Player.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        if (MyDirection == Direction.Right)
        {
            Player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

    }
   
    private void DectionalTarget() 
    {
        Vector2 Origin = new Vector2(Player.gameObject.transform.position.x, Player.gameObject.transform.position.y+0.6f);
        Vector2 Directional;
        if (MyDirection == Direction.Left) 
        {
            Directional = new Vector2(-10, 0);
        }
        else 
        {
            Directional = new Vector2( 10, 0);
        }
        RaycastHit2D ThisTarget = Physics2D.Raycast(Origin,Directional,1000);
        if (ThisTarget.rigidbody != null) 
        {
            Debug.Log("RayHit");          
            if (ThisTarget.rigidbody.CompareTag("Enemy"))
            {
                Target = ThisTarget.rigidbody.gameObject;
            }
            else
            {
                Target = null;
            }
        }
        else
        {
            Target = null;
        }
        Debug.DrawRay(Origin, Directional, Color.green) ;


    }
    private void PerformedIdle() 
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
    private void PerformedRun() 
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
    private void PerformedSprint() 
    {
        if(CharacterStateMachine.Current_state == Sprint) 
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
    private void PerformedAttack() 
    {
        if(CharacterStateMachine.Current_state == Attack) 
        {
            if(Attack.CurrentAttackPhase == AttackState.AttackPhase.PreAttack) 
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
            else if(Attack.CurrentAttackPhase == AttackState.AttackPhase.PostAttack) 
            {
                if (Input.GetKey(KeyCode.J)) 
                {
                    if (Attack == attack_1) 
                    {
                        Attack = attack_2;
                        CharacterStateMachine.ChangeState(Attack);
                    }
                    else if(Attack == attack_2) 
                    {
                        Attack = attack_3;
                        CharacterStateMachine.ChangeState(Attack);
                    }
                }
                
            }
            else if (Attack.CurrentAttackPhase == AttackState.AttackPhase.None)
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
                if (Input.anyKey == false)
                {
                    Debug.Log("Idle");
                    CharacterStateMachine.ChangeState(Idle);
                }
            }
        }
    }
    private void PerformedCrouch() 
    {
        if(CharacterStateMachine.Current_state == Crouch) 
        {
            if (Input.GetKey(KeyCode.S) == false) 
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
    private void PerformedJump() 
    {
        if(CharacterStateMachine.Current_state == Idle|| CharacterStateMachine.Current_state == Walk
            || CharacterStateMachine.Current_state == Sprint&&(CharacterStateMachine.Current_state != Jump)) 
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                CharacterStateMachine.ChangeState(Jump);
            }
        }
        if (CharacterStateMachine.Current_state == Jump)
        {
            if (AngularVelocity < 0)
            {
                CharacterStateMachine.ChangeState(Fall);
                Debug.Log("Fall");
            }
        }
    }
    private void PerformedFall() 
    {
        if(MyRigidbody2D.angularVelocity < 0f) 
        {
            CharacterStateMachine.ChangeState(Fall);
        }
    }
    
    private void CalAngularVelocity(float PreviosFramePositipn) 
    {
        AngularVelocity = gameObject.transform.position.y - PreviosFramePositipn;
        PreviosPositionY = gameObject.transform.position.y;
    }
}
