using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAbstract : MonoBehaviour
{
    
    public enum Disposition 
    {
        Friendly,
        Hostile,
        Neutral
    }

    [SerializeField]
    public Disposition unitDisposition;

    [SerializeField]
    public int health;

    [SerializeField]
    public int movementSpeed;
}
