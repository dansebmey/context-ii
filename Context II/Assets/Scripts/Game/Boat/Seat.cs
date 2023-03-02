using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    public Tourist tourist;

    public void AssignTo(Tourist t)
    {
        tourist = t;
        tourist.transform.SetParent(transform);
        tourist.transform.localPosition = Vector3.zero;
    }

    public void ClearSeat()
    {
        tourist = null;
    }

    public bool IsOccupied()
    {
        return tourist != null;
    }
}
