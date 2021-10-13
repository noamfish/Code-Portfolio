using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    CombatHandler combatHandler;
    private void Awake()
    {
        combatHandler = GetComponentInParent<CombatHandler>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<UnitAbstract>() != null && other.GetComponent<UnitAbstract>().unitDisposition == UnitAbstract.Disposition.Friendly) 
        {
            combatHandler.SetAttackTarget(other.gameObject);
        }
    }
}
