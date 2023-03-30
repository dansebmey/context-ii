using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boat : MonoBehaviour
{
    private enum State { Moving, Stopped }
    private State state = State.Moving;

    public float travelSpeed = 0.1f;
    private float initTravelSpeed;

    // Components
    private CameraController camCtrl;
    private List<Seat> touristSeats;
    
    private List<Tourist> touristsOnBoard;
    private readonly List<Pier> passedPiers = new List<Pier>();
    public bool isMoving = true;

    private void Awake()
    {
        camCtrl = FindObjectOfType<CameraController>();
        touristSeats = GetComponentsInChildren<Seat>().ToList();
    }

    private void Start()
    {
        touristsOnBoard = new List<Tourist>();
        initTravelSpeed = travelSpeed;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Moving:
                UpdateMoving();
                break;
            case State.Stopped:
                UpdateStopped();
                break;
        }
    }
    
    private void UpdateMoving()
    {
        
    }
    
    private void UpdateStopped()
    {
        
    }
    
    private void FixedUpdate()
    {
        if (state == State.Moving)
        {
            FixedUpdateMoving();
        }
    }
    
    private void FixedUpdateMoving()
    {
        if (!isMoving) return;
        
        transform.position += new Vector3(0, 0, travelSpeed);
    }

    public void OnPierReached(Pier pier)
    {
        if (!passedPiers.Contains(pier))
        {
            Disembark(pier);
            Embark(pier); 
            
            SetState(State.Stopped);
            EventManager.Invoke(EventType.OnPierReached);
            
            passedPiers.Add(pier);
        }
    }

    private void SetState(State s)
    {
        switch (s)
        {
            case State.Moving:
                break;
            case State.Stopped:
                Invoke(nameof(StartMoving), 5);
                break;
        }
    }

    private void StartMoving()
    {
        SetState(State.Moving);
    }

    private void Disembark(Pier pier)
    {
        foreach (Tourist tourist in touristsOnBoard)
        {
            pier.DropOffTourist(tourist);
        }
    }

    private void Embark(Pier pier)
    {
        foreach (Tourist tourist in pier.GetEmbarkingTourists())
        {
            touristsOnBoard.Add(tourist);
            AssignSeat(tourist);
        }
    }

    private void AssignSeat(Tourist tourist)
    {
        List<Seat> availableSeats = touristSeats.Where(s => !s.IsOccupied()).ToList();
        Seat seat = availableSeats[Random.Range(0, availableSeats.Count)];
        
        seat.AssignTo(tourist);
    }

    public List<Tourist> GetTouristsOnBoard()
    {
        return touristsOnBoard;
    }
    
    public int CollectTipsFromTourists()
    {
        int total = 0;
        foreach (Tourist tourist in touristsOnBoard)
        {
            total += tourist.GetTipFromCheckout();
        }
        
        // temp
        // FindObjectOfType<TipCounter>().AddToCounter(total);
            
        return total;
    }
    
    public int CollectTipFromTourist(int index)
    {
        return touristsOnBoard[index].GetTipFromCheckout();
    }

    public void CheckOutTourists()
    {
        touristsOnBoard.Clear();
    }

    public void StartSlowingDown(Trigger trigger)
    {
        StartCoroutine(SlowDown(trigger));
    }

    private IEnumerator SlowDown(Trigger trigger)
    {
        while (travelSpeed > 0)
        {
            travelSpeed -= initTravelSpeed * 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        travelSpeed = 0;
        trigger.OnBoatArrived(this);
    }

    public void StartSpeedingUp()
    {
        StartCoroutine(SpeedUp());
    }

    private IEnumerator SpeedUp()
    {
        while (travelSpeed < initTravelSpeed)
        {
            travelSpeed += initTravelSpeed * 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        travelSpeed = initTravelSpeed;
    }

    public CameraController GetCameraController()
    {
        return camCtrl;
    }

    public void Teleport(Transform destinationTx)
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, destinationTx.position.z);
    }
}
