using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChacterBox : MonoBehaviour
{    // Start is called before the first frame update
    [SerializeField] public double floorAngle; /*{ get;private set; }*/
    public List<Collider2D> Floor = new List<Collider2D>();
    private void Start()
    {

    }
    private void Update()
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.rigidbody.CompareTag("Ground")) 
        {
            Debug.Log("FindFloor");
            if (collision.transform.rotation.z != 0) 
            {
                floorAngle = collision.transform.rotation.z;
            }
        }
    }
    
   
}
