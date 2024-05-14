using Assets.GameObj.MainCharacter.State;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float AngularVelocity { get ;private set; }
    private float PreviosPositionY;
    public enum Direction 
    {
        Left,
        Right
    }
    public Direction MyDirection;
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
        PerformedState();
    }
    virtual protected void FixedUpdate()
    {
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
        Vector2 Origin = new Vector2(MyCharacter.gameObject.transform.position.x, MyCharacter.gameObject.transform.position.y + 0.6f);
        Vector2 Directional;
        if (MyDirection == Direction.Left)
        {
            Directional = new Vector2(-10, 0);
        }
        else
        {
            Directional = new Vector2(10, 0);
        }
        if (Target == null)
        {
            RaycastHit2D[] result = Physics2D.RaycastAll(Origin, Directional, 1000);
            if (result.Length > 0)
            {
                foreach (RaycastHit2D r in result)
                {
                    if (r.rigidbody.CompareTag(Target_tag))
                    {
                        Target = r.rigidbody.gameObject;
                        break;
                    }
                }
            }
        }
            Debug.DrawRay(Origin, Directional, Color.green);  
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
        CharacterStateMachine.UpdateState();
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
}
