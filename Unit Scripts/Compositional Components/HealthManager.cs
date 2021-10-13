using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private UnitAbstract thisUnit;
    public bool isAlive = true;
    public Animator animator;
    private void Start()
    {
        thisUnit = GetComponent<UnitAbstract>();
    }
    public void DecrementHealth(int amount)
    {
        thisUnit.health = thisUnit.health - amount;
        if (thisUnit.health <= 0)
        {
            PlayKillAnim();
        }
    }

    private void PlayKillAnim()
    {
        if (isAlive == true)
        {
            isAlive = false;
            animator.SetBool("Dead", true);
            DropItems();
            DestroyObject();
        }
    }
    private void DestroyObject()
    {
        Destroy(gameObject, 3f);

    }
    public bool IsAlive()
    {
        return isAlive;
    }

    private void DropItems()
    {
        if(thisUnit.GetComponent<IItemDroppable>() != null)
        {
            thisUnit.GetComponent<IItemDroppable>().DropItems();
        }
    }
}
