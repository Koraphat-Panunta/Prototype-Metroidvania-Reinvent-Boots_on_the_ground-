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
    private float Walk_speed = 1.5f;
    private float Run_speed = 3.0f;
    public bool Change_state_able = true; 
    public bool ATK_Combo_Continue = false;
    public GameObject Target;
    public Collider2D Attack_box;
    public Collider2D Hitted_box;
    public bool Is_attacking = false;
    public enum Direction 
    {
        Left,
        Right
    }
    public Direction MyDirection = Direction.Right;
   

    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {  
    }
    public void FixedUpdate()
    {
        DirectionManagement();
        DectionalTarget();         
        PerformedAction();        
    }
    //Behavior
    public void Attack() 
    {
        
        float Input_buffer_time = 0.25f;
        if (MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") && MyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > Input_buffer_time) 
        {
            ATK_Combo_Continue = true;
        }
        else if(MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") && MyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > Input_buffer_time) 
        {
            ATK_Combo_Continue = true;
        }
        else if(Change_state_able == true)
        {
            Change_state_able = false;
            MyAnimator.Play("Attack 1");
        }
    }
    public void Walk(Direction direction) 
    {     
        MyAnimator.Play("Walk");
        MyDirection = direction;  
    }
    public void Run(Direction direction) 
    {      
        MyAnimator.Play("Run");
        MyDirection = direction;
    }
    public void idle() 
    {
        MyAnimator.Play("Idle");
    }

    //
    private void AttackMechanical() 
    {
        if (MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") ||
            MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") ||
            MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3"))
        {
            //AttackIsEnd
            if (MyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.85f && ATK_Combo_Continue == true)
            {
                ATK_Combo_Continue = false;
                if (MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1"))
                {
                    MyAnimator.Play("Attack 2");
                    Debug.Log("ATK_Con");
                }
                else if (MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2"))
                {
                    MyAnimator.Play("Attack 3");
                }
                else
                {
                    Change_state_able = true;
                }
            }
            else if (MyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Change_state_able = true;
            }
            //AttackIsDamaging
            if(MyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f&& MyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.9f) 
            {
                Is_attacking = true;
            }
            else 
            {
                Is_attacking = false;
            }
        }
        else
        {
            Change_state_able = true;
        }
        if(Is_attacking == true) 
        {
            Attack_box.GetComponent<SpriteRenderer>().enabled = true;
        }
        else 
        {
            Attack_box.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    private void DirectionManagement() 
    {
        if (MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            if (MyDirection == Direction.Left)
            {
                Debug.Log("Walk");
                MyRigidbody2D.velocity = new Vector2(-Walk_speed, 0);
            }
            else if (MyDirection == Direction.Right)
            {
                MyRigidbody2D.velocity = new Vector2(Walk_speed, 0);
            }
        }
        if (MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            if (MyDirection == Direction.Left)
            {
                MyRigidbody2D.velocity = new Vector2(-Run_speed, 0);
            }
            else if (MyDirection == Direction.Right)
            {
                MyRigidbody2D.velocity = new Vector2(Run_speed, 0);
            }
        }
        if (MyDirection == Direction.Left)
        {
           Player.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        if (MyDirection == Direction.Right)
        {
            Player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

    }
    private void PerformedAction() 
    { 
        AttackMechanical();
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

}
