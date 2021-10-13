using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    public GameObject attackTarget;
    public float attacksPerSecond;
    public int attackRange;
    public int damage;
    public int engageRange;
    public int disengageRange;
    public void SetAttackTarget(GameObject attackTarget)
    {
        this.attackTarget = attackTarget;
    }
    public void ClearAttackTarget()
    {
        attackTarget = null;
    }

    public void DecrementEnemyHealth() //called from animation event
    {
        attackTarget.GetComponent<HealthManager>().DecrementHealth(damage);
    }
}
