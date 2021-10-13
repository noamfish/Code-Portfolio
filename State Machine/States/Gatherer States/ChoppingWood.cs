using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingWood : IState
{
    Animator _animator;
    GathererBehavior _gatherer;
    Transform _ax;
    Transform _pickAx;
    Transform _unitTransform;
    public ChoppingWood(Animator animator, Transform unitTransform, GathererBehavior gatherer, Transform ax, Transform pickAx)
    {
        _animator = animator;
        _gatherer = gatherer;
        _ax = ax;
        _pickAx = pickAx;
        _unitTransform = unitTransform;
    }
    public void Tick()
    {

    }
    public void OnEnter()
    {
        _unitTransform.Rotate(0f, -50f, 0f);
        _animator.SetBool("Chop", true);
        _gatherer.currentResource.AddUser();
        _pickAx.gameObject.SetActive(false);
        _ax.gameObject.SetActive(true);
    }
    public void OnExit()
    {
        _animator.SetBool("Chop", false);
        _gatherer.previousResource.RemoveUser();
        _unitTransform.Rotate(0f, 0f, 0f);
    }
}
