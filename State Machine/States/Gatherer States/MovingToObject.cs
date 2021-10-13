using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingToObject : IState
{
    NavMeshAgent _navmeshAgent;
    Animator _animator;
    GathererBehavior _gatherer;
    private NavMeshPath path;
    Transform currentObjectTransform;
    public MovingToObject(NavMeshAgent navmeshAgent, Animator animator, GathererBehavior gatherer)
    {
        _navmeshAgent = navmeshAgent;
        _animator = animator;
        _gatherer = gatherer;
        path = new NavMeshPath();
    }
    public void Tick()
    {
        if (_gatherer.objectDestination != currentObjectTransform)
        {
            if (_gatherer.objectDestination.CompareTag("Tree"))
            {
                _navmeshAgent.SetDestination(_gatherer.objectDestination.position);
                currentObjectTransform = _gatherer.objectDestination;
            }
            if (_gatherer.objectDestination.CompareTag("Rock"))
            {
                _navmeshAgent.CalculatePath(_gatherer.objectDestination.position, path);
                _navmeshAgent.SetPath(path);
                currentObjectTransform = _gatherer.objectDestination;
            }
        }
    }

    public void OnEnter()
    {
        _navmeshAgent.enabled = true;
        _animator.SetBool("Walk", true);
        if (_gatherer.objectDestination.CompareTag("Tree"))
        {
            _navmeshAgent.SetDestination(_gatherer.objectDestination.position);
        }
        if (_gatherer.objectDestination.CompareTag("Rock"))
        {
            _navmeshAgent.CalculatePath(_gatherer.objectDestination.position, path);
            _navmeshAgent.SetPath(path);
        }

    }

    public void OnExit()
    {
        _navmeshAgent.enabled = false;
        _animator.SetBool("Walk", false);
        _gatherer.ClearObjectDestination();
    }
}
