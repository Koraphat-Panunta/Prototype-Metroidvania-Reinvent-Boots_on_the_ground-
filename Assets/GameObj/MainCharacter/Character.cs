using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator MyAnimator;
    public Rigidbody2D MyRigidbody2D;
    private float WalkSpeed = 1.5f;
    private float RunSpeed = 3.0f;
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
        if (MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) 
        {
            if(MyDirection == Direction.Left) 
            {
                MyRigidbody2D.velocity = new Vector2(-WalkSpeed, 0);
            }
            else if(MyDirection == Direction.Right) 
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
        if(MyDirection == Direction.Left) 
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,180, 0));
        }
        if(MyDirection == Direction.Right) 
        {
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,0, 0));
        }
    }
    public void Attack() 
    {
        MyAnimator.ResetTrigger("Attack");
        MyAnimator.SetTrigger("Attack");
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
        MyAnimator.Play("idle");
    }
}
