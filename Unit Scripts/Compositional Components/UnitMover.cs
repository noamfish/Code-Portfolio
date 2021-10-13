using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 targetDestination;
    public int wanderSpeed;
    public int runSpeed;
    private void Awake()
    {
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}
