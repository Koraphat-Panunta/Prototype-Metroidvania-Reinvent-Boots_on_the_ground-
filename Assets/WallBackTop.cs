using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBackTop : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.rigidbody.tag == "Player") 
        {
            Debug.Log("Player on platform");
        }
    }
}
