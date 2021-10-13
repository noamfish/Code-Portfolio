using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SearchForWanderDest : IState
{
    private readonly UnitMover _unitMover;
    private readonly CombatHandler _combatHandler;

    public SearchForWanderDest(UnitMover unitMover, CombatHandler combatHandler)
    {
        _unitMover = unitMover;
        _combatHandler = combatHandler;
    }

    public void Tick()
    {
        _unitMover.targetDestination = PickDestination();
    }

    private Vector3 PickDestination()
    {
        Vector3 destination = new Vector3(_unitMover.startPosition.x + Random.Range(-30f, 30f), _unitMover.startPosition.y, _unitMover.startPosition.z + Random.Range(-30, 30));
        return destination;
    }

    public void OnEnter()
    {
        _combatHandler.attackTarget = null;
        _unitMover.targetDestination = PickDestination();
    }

    public void OnExit()
    {

    }
}
