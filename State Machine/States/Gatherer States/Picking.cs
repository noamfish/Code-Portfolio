using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picking : IState
{
    Animator _animator;
    GathererBehavior _gatherer;
    Transform _pickAx;
    Transform _ax;
    public Picking(Animator animator, GathererBehavior gatherer, Transform pickAx, Transform ax)
    {
        _animator = animator;
        _pickAx = pickAx;
        _ax = ax;
        _gatherer = gatherer;
    }
    public void Tick()
    {

    }
    public void OnEnter()
    {

        _animator.SetBool("Pick", true);
        _gatherer.currentResource.AddUser();
        _pickAx.gameObject.SetActive(true);
        _ax.gameObject.SetActive(false);
    }
    public void OnExit()
    {
        _animator.SetBool("Pick", false);
        _gatherer.previousResource.RemoveUser();
    }
}
