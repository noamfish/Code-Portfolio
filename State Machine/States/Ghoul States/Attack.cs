using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : IState
{
    Animator _animator;
    CombatHandler _combatHandler;
    float timeUntilNextAttack;

    public Attack(Animator animator, CombatHandler combatHandler)
    {
        _animator = animator;
        _combatHandler = combatHandler;
    }
    public void Tick()
    {
        if(_combatHandler.attackTarget != null)
        {
            if(timeUntilNextAttack <= Time.time)
            {
                timeUntilNextAttack = Time.time + (1f / _combatHandler.attacksPerSecond);
                _animator.SetTrigger("Attack");
                _animator.SetTrigger("Idle");
            }
        }
    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {
        _animator.SetTrigger("Idle");
    }
}
