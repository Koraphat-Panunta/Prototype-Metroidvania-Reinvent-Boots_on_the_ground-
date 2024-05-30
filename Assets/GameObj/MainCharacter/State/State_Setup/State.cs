using System;
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
    protected Player Player;

    public enum stateAbleToBypass
    {
        Full,
        Semi,
        None
    }
    public stateAbleToBypass StateLevle { get;protected set; }
    public State(Animator animator,GameObject Char) 
    {
        Animation = animator;
        ObjCharacter = Char;
        Character = ObjCharacter.GetComponent<Character>();
        if(Character.IsControlbyPlayer == true) 
        {
            Player = Character.GetComponent<Player>();
        }
    }
    public virtual void EnterState() 
    {
        IsEnter = true;
        IsExit = false;
    }
    private bool nextframeIsExit=false;
    public virtual void FrameUpdateState() 
    {
        if (IsEnter == true) 
        {
            IsEnter = false;      
        }
        if(IsExit == true) 
        {
            if (nextframeIsExit == true)
            {
                IsExit = false;
                nextframeIsExit = false;
            }
            if (IsExit == true)
            {
                nextframeIsExit = true;
            }
        }
    }
    public virtual void PhysicUpdateState() { }
    public virtual void ExitState() 
    {
        IsEnter = false;
        IsExit = true;
    }
    protected abstract void SetStateLevel();//Set a state Level that let other state to bypass
    
   
}
