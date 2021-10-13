using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToAttack : IState
{

    NavMeshAgent _navMeshAgent;
    Animator _animator;
    CombatHandler _combatHandler;
    UnitMover _unitMover;

    public MoveToAttack(NavMeshAgent navMeshAgent, Animator animator, CombatHandler combatHandler, UnitMover unitMover)
    {
        _navMeshAgent = navMeshAgent;
        _animator = animator;
        _combatHandler = combatHandler;
        _unitMover = unitMover;
    }
    public void Tick()
    {
        _navMeshAgent.SetDestination(_combatHandler.attackTarget.transform.position);
    }
    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
        _animator.SetFloat("Speed", 5f);
        _navMeshAgent.speed = _unitMover.runSpeed;
    }
    public void OnExit()
    {
        _navMeshAgent.enabled = false;
        _animator.SetFloat("Speed", 0f);
    }
}
