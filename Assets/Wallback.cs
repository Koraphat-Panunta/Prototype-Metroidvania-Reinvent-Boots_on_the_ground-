using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallback : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponentInParent<Player>();
            player.IsOnWallBack = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponentInParent<Player>();
            player.IsOnWallBack = false;
        }
    }
}
