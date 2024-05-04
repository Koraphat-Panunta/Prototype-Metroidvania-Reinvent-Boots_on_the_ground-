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

    public MainCharacterStateMachine CharacterStateMachine;

    public IdleState Idle;
    public WalkState Walk;
    public SprintState Sprint;
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
        Idle = new IdleState(MyAnimator, Player);
        Walk = new WalkState(MyAnimator, Player);
        Sprint = new SprintState(MyAnimator, Player);
        CharacterStateMachine = new MainCharacterStateMachine(Idle);
    }

    // Update is called once per frame
    private void Update()
    {
        PerformedIdle();
        PerformedRun();
        PerformedSprint();
        CharacterStateMachine.UpdateState();
    }
    public void FixedUpdate()
    {
        CharacterStateMachine.FixedStateUpdate();
        DirectionManagement();
        DectionalTarget();                
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

}
