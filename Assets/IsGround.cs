using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGround : MonoBehaviour
{
    [SerializeField] private Character Character;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")) 
        {
            Character.CharacterStateMachine.ChangeState(Character.Idle);
        }
    }
}
