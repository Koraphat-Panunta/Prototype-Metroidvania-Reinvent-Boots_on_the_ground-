using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator MyAnimator;
    public Rigidbody2D MyRigidbody2D;
    private float WalkSpeed = 1.5f;
    private float RunSpeed = 3.0f;
    public bool Change_state_able = true; 
    public bool ATK_Combo_Continue = false;
    public GameObject Target;
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
    private void FixedUpdate()
    {
        DirectionManagement();
        AttackCombo();
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
    private void AttackCombo() 
    {
        if (MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") ||
            MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") ||
            MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3"))
        {
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
        }
        else
        {
            Change_state_able = true;
        }
    }
    private void DirectionManagement() 
    {
        if (MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            if (MyDirection == Direction.Left)
            {
                MyRigidbody2D.velocity = new Vector2(-WalkSpeed, 0);
            }
            else if (MyDirection == Direction.Right)
            {
                MyRigidbody2D.velocity = new Vector2(WalkSpeed, 0);
            }
        }
        if (MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            if (MyDirection == Direction.Left)
            {
                MyRigidbody2D.velocity = new Vector2(-RunSpeed, 0);
            }
            else if (MyDirection == Direction.Right)
            {
                MyRigidbody2D.velocity = new Vector2(RunSpeed, 0);
            }
        }
        if (MyDirection == Direction.Left)
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        if (MyDirection == Direction.Right)
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

    }

}
