using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<Character>().TryGetComponent<Enemy>(out Enemy enemy)) 
        {
            enemy.GotAttack();
        }
    }
}
