using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToLocation : IState
{
    NavMeshAgent _navMeshAgent;
    Animator _animator;
    UnitMover _unitMover;

    private Vector3 lastPosition = new Vector3(0,0,0);
    public float TimeStuck;
    public MoveToLocation(NavMeshAgent navMeshAgent, Animator animator, UnitMover unitMover)
    {
        _navMeshAgent = navMeshAgent;
        _animator = animator;
        _unitMover = unitMover;
    }
    public void Tick()
    {
        if(Vector3.Distance(_unitMover.transform.position, lastPosition) <= 0f)
        {
            TimeStuck += Time.deltaTime;
        }
        lastPosition = _unitMover.transform.position;
    }


    public void OnEnter()
    {
        TimeStuck = 0f;
        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(_unitMover.targetDestination);
        _navMeshAgent.speed = _unitMover.wanderSpeed;
        _animator.SetFloat("Speed", 1f);
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
        _animator.SetFloat("Speed", 0f);
    }
}
