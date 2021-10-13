using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingToEnemy : IState
{
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;
    private readonly CombatHandler _combatHandler;
    SoldierBehavior _soldier;

    public MovingToEnemy(NavMeshAgent navMeshAgent, Animator animator, SoldierBehavior soldier, CombatHandler combatHandler)
    {
        _navMeshAgent = navMeshAgent;
        _animator = animator;
        _combatHandler = combatHandler;
        _soldier = soldier;
    }

    public void Tick()
    {
        _navMeshAgent.SetDestination(_combatHandler.attackTarget.transform.position);
    }

    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
        _animator.SetBool("Walk", true);
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
        _animator.SetBool("Walk", false);
        _soldier.ClearObjectDestination();
    }
}
