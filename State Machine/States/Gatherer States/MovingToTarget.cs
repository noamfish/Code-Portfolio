using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingToTarget : IState
{
    NavMeshAgent _navmeshAgent;
    Animator _animator;
    IUserMovable _activeUnit;
    Vector3 currentDestination;
    public MovingToTarget(NavMeshAgent navmeshAgent, Animator animator, IUserMovable activeUnit)
    {
        _navmeshAgent = navmeshAgent;
        _animator = animator;
        _activeUnit = activeUnit;

    }
    public void Tick()
    {
        if(_activeUnit.locationDestination != currentDestination)
        {
            _navmeshAgent.SetDestination(_activeUnit.locationDestination);
            currentDestination = _activeUnit.locationDestination;
        }
    }

    public void OnEnter()
    {
        currentDestination = _activeUnit.locationDestination;
        _navmeshAgent.enabled = true;
        _animator.SetBool("Walk", true);
        _navmeshAgent.SetDestination(currentDestination);
    }

    public void OnExit()
    {
        _navmeshAgent.enabled = false;
        _animator.SetBool("Walk", false);
        _activeUnit.ClearLocationDestination();
    }

}
