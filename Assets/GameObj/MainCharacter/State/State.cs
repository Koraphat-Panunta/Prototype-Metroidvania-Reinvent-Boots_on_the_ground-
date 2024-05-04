using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class State 
{
    public bool IsEnter = false;
    public bool IsExit = false;
    protected Animator Animation;
    protected GameObject ObjCharacter;
    protected Character Character;
    public State(Animator animator,GameObject Char) 
    {
        Animation = animator;
        ObjCharacter = Char;
        Character = ObjCharacter.GetComponent<Character>();
    }
    public virtual void EnterState() 
    {
        IsEnter = true;
    }
    public virtual void FrameUpdateState() 
    {
        IsExit = false;
        IsExit = false;
    }
    public virtual void PhysicUpdateState() { }
    public virtual void ExitState() 
    {
        IsExit = true;
    }
}
