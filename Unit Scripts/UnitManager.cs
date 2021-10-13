using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] GameObject gatherer;
    [SerializeField] GameObject rallyPoint;
    [SerializeField] GameObject soldier;
    GameObject newGatherer;
    GameObject newSoldier;
    GameObject newUnit;
    public List<GameObject> soldiers = new List<GameObject>();
    public List<GameObject> gatherers = new List<GameObject>();
    public List<SelectionManager> friendlyUnits = new List<SelectionManager>();

    public void InstantiateUnit(GameObject unit)
    {
        newUnit = Instantiate(unit, rallyPoint.transform.position, Quaternion.identity);
        friendlyUnits.Add(newUnit.GetComponent<SelectionManager>());
    }
}
