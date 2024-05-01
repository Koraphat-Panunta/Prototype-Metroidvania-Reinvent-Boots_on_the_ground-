using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Character MyCharacter;
  
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            MyCharacter.Attack();
        }
        else
        {
            if (Input.GetKey(KeyCode.A) && MyCharacter.Change_state_able == true)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    MyCharacter.Run(Character.Direction.Left);
                }
                else
                {
                    MyCharacter.Walk(Character.Direction.Left);
                }
            }
            else if (Input.GetKey(KeyCode.D) && MyCharacter.Change_state_able == true)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    MyCharacter.Run(Character.Direction.Right);
                }
                else
                {
                    MyCharacter.Walk(Character.Direction.Right);
                }
            }
            else if( MyCharacter.Change_state_able == true)
            {
                MyCharacter.idle();            
            }
           
        }

    }
    public Character GetCharacter() 
    {
        return MyCharacter;
    }
}
