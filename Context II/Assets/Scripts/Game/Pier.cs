using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pier : MonoBehaviour
{
    private List<Tourist> embarkingTourists;
    public Transform disembarkedCrowdCentre;

    private void Awake()
    {
        embarkingTourists = GetComponentsInChildren<Tourist>().ToList();
    }

    public void DropOffTourist(Tourist tourist)
    {
        tourist.transform.SetParent(disembarkedCrowdCentre);
        tourist.transform.position += new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f));
    }

    public List<Tourist> GetEmbarkingTourists()
    {
        return embarkingTourists;
    }
}
