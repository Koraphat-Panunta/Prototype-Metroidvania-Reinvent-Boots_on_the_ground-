using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Character;
using static Player;
using UnityEngine.UI;

public class CheckPlayerCollider : MonoBehaviour
{
    public enum IsHitWall
    {
        None,
        Left,
        Right,
    }
    public enum IsHitObjectClimbAble
    {
        None,
        Left,
        Right,
    }
    public enum CharacterGround
    {
        Ground,
        Platform,
        SlopePlatform,
        Air
    }
    public CharacterGround Characterground;
    public IsHitWall HitWall = IsHitWall.None;
    public IsHitObjectClimbAble HitObjectClimbAble = IsHitObjectClimbAble.None;
    public Collider2D ClimbObject { get; private set;}
    [SerializeField] private Player Player;
    public double Airtime = 0;
    void Start()
    {
        Player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCharacterCollider();
    }
    private void CheckCharacterCollider()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        Player.MyRigidbody2D.GetContacts(colliders);
        if (colliders.Count > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Wall"))
                {
                    if (collider.transform.position.x < gameObject.transform.position.x)
                    {
                        HitWall = IsHitWall.Left;
                    }
                    else if (collider.transform.position.x > gameObject.transform.position.x)
                    {
                        HitWall = IsHitWall.Right;
                    }
                }
                else
                {
                    HitWall = IsHitWall.None;
                }
                if (collider.CompareTag("ClimbObject"))
                {
                    if (collider.transform.position.x < gameObject.transform.position.x)
                    {
                        HitObjectClimbAble = IsHitObjectClimbAble.Left;
                        ClimbObject = collider;
                    }
                    else if (collider.transform.position.x > gameObject.transform.position.x)
                    {
                        HitObjectClimbAble = IsHitObjectClimbAble.Right;
                        ClimbObject = collider;
                    }
                }
                else
                {
                    HitObjectClimbAble = IsHitObjectClimbAble.None;
                    ClimbObject = null;
                }
                if (collider.CompareTag("Ground"))
                {
                    Characterground = CharacterGround.Ground;
                    Player.FootsAngle = collider.transform.rotation.eulerAngles.z;
                    if (Player.FootsAngle == 0)
                    {
                        Characterground = CharacterGround.Ground;
                    }
                    else
                    {
                        Characterground = CharacterGround.SlopePlatform;
                    }
                    break;
                }
                else if (collider.CompareTag("Platform"))
                {

                    if (Player.MyRigidbody2D.angularVelocity <= 0 && gameObject.transform.position.y > collider.transform.position.y)
                    {
                        Characterground = CharacterGround.Platform;
                        Player.FootsAngle = collider.transform.rotation.eulerAngles.z;
                        if (Player.FootsAngle == 0)
                        {

                        }
                        else
                        {

                        }
                        break;
                    }
                    else
                    {
                        Characterground = CharacterGround.Air;
                    }
                }
                else
                {
                    Characterground = CharacterGround.Air;
                }
            }
        }
        else
        {
            HitWall = IsHitWall.None;
            Characterground = CharacterGround.Air;
            HitObjectClimbAble = IsHitObjectClimbAble.None;
            ClimbObject = null;
        }
        
    }
    public bool IsGround()
    {
        if (Characterground == CharacterGround.Ground || Characterground == CharacterGround.SlopePlatform || Characterground == CharacterGround.Platform)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
