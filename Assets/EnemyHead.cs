using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    public Enemy enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlyerFeet>(out PlyerFeet player)) 
        {
            Player Player = player.GetComponentInParent<Player>();
            if (Player.transform.position.x > enemy.transform.position.x)
            {
                enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(-400, 0));
            }
            else if (Player.transform.position.x < enemy.transform.position.x)
            {
                enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(400, 0));
            }
        }
    }
}
