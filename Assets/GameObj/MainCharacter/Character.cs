using Assets.GameObj.MainCharacter.State;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
   //Define Component of Character
    public Animator MyAnimator;
    public Rigidbody2D MyRigidbody2D;
    public GameObject MyCharacter;
    public GameObject Target;
    public Collider2D Attack_box;
    public Collider2D Hitted_box;
    public Collider2D IsGroundbox;
    public GameObject Raypoint;

    //Define Character State
    public IdleState Idle;
    public WalkState Walk;
    public SprintState Sprint;
    public AttackState Attack;//Current Attack state
    public CrouchState Crouch;
    public JumpState Jump;
    public FallState Fall;
    public CharacterStateMachine CharacterStateMachine;//Define Current Character State

    //For Calculate Angular Velocity of Character,Object
    [SerializeField] public float AngularVelocity;
    private float PreviosPositionY;

    //public bool Isground;
    //public bool IsOnSlope;
    public enum CharacterGround 
    {
        Ground,
        Platform,
        SlopePlatform,
        Air
    }
    public CharacterGround Characterground;
    public bool IsOnWallBack;
    [SerializeField] public float FootsAngle;
    public float Velocity;
    public int jumpCount = 2;
    
    public int HeathPoint { get;protected set; }
    public enum Direction 
    {
        Left,
        Right
    }
    public Direction MyDirection;
    public bool IsControlbyPlayer { get;protected set; }
    protected virtual void SetupState() 
    {
        CharacterStateMachine = new CharacterStateMachine(Idle);
    }
    
    virtual protected void Start()
    {
        MyCharacter = gameObject;
        MyDirection = Direction.Right;
        Attack_box.enabled = false;
        SetupState();
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        CharacterStateMachine.UpdateState();
        PerformedState();
    }
    virtual protected void FixedUpdate()
    {
        if(CharacterStateMachine.Current_state != Walk && CharacterStateMachine.Current_state != Sprint) 
        {
            Velocity = 0f;
        }
        if(MyRigidbody2D.bodyType == RigidbodyType2D.Kinematic&& MyRigidbody2D.velocity.x != 0) 
        {
            MyRigidbody2D.velocity = Vector3.zero;
        }
        CharacterStateMachine.FixedStateUpdate();
        DirectionManagement();
        CalAngularVelocity(PreviosPositionY);
    }
    //Behavior
    
    private void DirectionManagement() 
    {
        
        if (MyDirection == Direction.Left)
        {
            MyCharacter.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        if (MyDirection == Direction.Right)
        {
            MyCharacter.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

    }
   
    virtual protected void DectionalTarget(string Target_tag) 
    {
        Vector2 Origin = new Vector2(Raypoint.gameObject.transform.position.x, Raypoint.gameObject.transform.position.y);
        Vector2 Directional;
        if (MyDirection == Direction.Left)
        {
            Directional = new Vector3(-10, 0);
        }
        else
        {
            Directional = new Vector3(10, 0);
        }
        RaycastHit2D result = Physics2D.Raycast(Origin, Directional,1000);
        if (result.rigidbody != null)
        {
            if (result.rigidbody.CompareTag(Target_tag))
            {
                Target = result.rigidbody.gameObject;
            }
        }
        Debug.DrawRay(Origin, Directional);
    }
    virtual protected void PerformedState() 
    {
        PerformedIdle();
        PerformedRun();
        PerformedSprint();
        PerformedAttack();
        PerformedCrouch();
        PerformedJump();
        PerformedFall();
    }
    virtual protected void PerformedIdle() 
    {
        
    }
    virtual protected void PerformedRun() 
    {
       
    }
    virtual protected void PerformedSprint() 
    {
       
    }
    virtual protected void PerformedAttack() 
    {
        
        
    }
    virtual protected void PerformedCrouch() 
    {
       
    }
    virtual protected void PerformedJump() 
    {
       

    }
    virtual protected void PerformedFall() 
    {
        
    }
    
    private void CalAngularVelocity(float PreviosFramePositipn) 
    {
        AngularVelocity = gameObject.transform.position.y - PreviosFramePositipn;
        PreviosPositionY = gameObject.transform.position.y;
    }
    virtual public void AccesstoStateCrossraod() 
    {
        
    }
    virtual public void GotAttack() 
    {
        Debug.Log("GotAttack");
    }
    public bool IsGround() 
    {
        if(Characterground == CharacterGround.Ground || Characterground == CharacterGround.SlopePlatform || Characterground == CharacterGround.Platform) 
        {
            return true;
        }
        else 
        { 
            return false; 
        }
    }
    
   
}
