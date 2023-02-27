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
    
    private List<Tourist> touristsOnBoard;
    private List<Seat> touristSeats;
    private readonly List<Pier> passedPiers = new List<Pier>();
    
    private void Awake()
    {
        touristSeats = GetComponentsInChildren<Seat>().ToList();
    }

    private void Start()
    {
        touristsOnBoard = new List<Tourist>();
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
        transform.position += new Vector3(0, 0, travelSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Pier pier = other.GetComponent<Pier>();
        if (pier)
        {
            OnPierReached(pier);
        }
    }

    private void OnPierReached(Pier pier)
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
                Invoke(nameof(TriggerStoryEvent), 10);
                break;
            case State.Stopped:
                Invoke(nameof(StartMoving), 5);
                break;
        }
    }

    private void TriggerStoryEvent()
    {
        EventManager.Invoke(EventType.TriggerStoryPrompt);
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

    public List<Tourist> GetTourists()
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
        
        return total;
    }

    public void CheckOutTourists()
    {
        touristsOnBoard.Clear();
    }
}
