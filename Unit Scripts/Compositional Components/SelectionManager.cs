using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public bool isSelected;

    public void CallObjectSelected()
    {
        isSelected = true;
        transform.GetComponent<IUserMovable>().CallObjectSelected();
    }

    public void CallObjectUnselected()
    {
        isSelected = false;
    }
}
