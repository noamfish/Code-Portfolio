using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttack : IState
{
    Animator _animator;
    CombatHandler _combatHandler;
    float timeUntilNextAttack;

    public SoldierAttack(Animator animator, CombatHandler combatHandler)
    {
        _animator = animator;
        _combatHandler = combatHandler;
    }
    public void Tick()
    {
        if (_combatHandler.attackTarget != null)
        {
            if (timeUntilNextAttack <= Time.time)
            {
                _combatHandler.gameObject.transform.LookAt(_combatHandler.attackTarget.transform.position);
                timeUntilNextAttack = Time.time + (1f / _combatHandler.attacksPerSecond);
                _animator.SetTrigger("Swing");
                _animator.SetTrigger("Jab");
                _animator.SetBool("Idle", true);
            }
        }
    }

    public void OnEnter()
    {

    }
    public void OnExit()
    {
        _animator.SetBool("Idle", true);
        _combatHandler.ClearAttackTarget();
    }
}
