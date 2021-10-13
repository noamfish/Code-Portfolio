using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : IState
{
    Animator animator;

    public Idle(Animator animator)
    {
        this.animator = animator;
    }
    public void Tick()
    {

    }

    public void OnEnter()
    {
        animator.SetBool("Idle", true);
    }

    public void OnExit()
    {
        animator.SetBool("Idle", false);
    }


}
