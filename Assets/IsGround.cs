using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGround : MonoBehaviour
{
    [SerializeField] private Character Character;
    [SerializeField] private Collider2D FootsCollider;
    public bool isground;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")) 
        {
            Character.AccesstoStateCrossraod();
            isground = true;
        }
        
    }
    
}
