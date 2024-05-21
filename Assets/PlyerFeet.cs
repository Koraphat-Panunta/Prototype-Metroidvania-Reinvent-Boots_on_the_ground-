using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyerFeet : MonoBehaviour
{
    public Player player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<EnemyHead>(out EnemyHead enemyHead)) 
        {
            player.CharacterStateMachine.ChangeState(player.Jump);
        }
    }
}
