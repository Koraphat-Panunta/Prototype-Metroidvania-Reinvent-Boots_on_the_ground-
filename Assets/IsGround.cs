using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGround : MonoBehaviour
{
    [SerializeField] private Character Character;
    [SerializeField] private Collider2D FootsCollider;
    [SerializeField] private LayerMask GroundLayer;



    private void Update()
    {
        //if (Physics2D.OverlapCircle(gameObject.transform.position, 0.001f,GroundLayer))        
        //{
        //    Character.Isground = true;
        //}
        //else 
        //{
        //    Character.Isground= false;
        //}
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Ground"))
    //    {
    //        Character.AccesstoStateCrossraod();
           
    //    }
    //}

}
