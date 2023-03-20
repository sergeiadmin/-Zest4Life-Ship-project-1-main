using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class ShipMovement : MonoBehaviour
{
    // Launches ship through waypoints, returns in to docks after delay timer, adds new coins and oil to storage
    
    [SerializeField] private Transform shipPath;
    [SerializeField] private Transform preDockingPosition;
    [SerializeField] private Transform dockingPosition;

    [SerializeField] private Storage storage;
    [SerializeField] private TradeMenu tradeMenu;
    [SerializeField] private TimeManager timeManager;

    private NavMeshAgent _navMeshAgent;
    private List<Transform> _currentWaypoint;
    private int _waypointIndex;
    private bool _singleDelivery;

    public int WaypointIndex
    {
        get => _waypointIndex;
        set => _waypointIndex = value;
    }

    public bool SingleDelivery
    {
        get => _singleDelivery;
        set => _singleDelivery = value;
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _currentWaypoint = GetShipWaypoints();
    }

    private void Update()
    {
        var position = transform.position;
        var waypointDistance = Vector3.Distance(position, _currentWaypoint[_waypointIndex].position);
        var preDockingDistance = Vector3.Distance(position, preDockingPosition.position);
        var dockingDistance = Vector3.Distance(position, dockingPosition.position);

        if (dockingDistance > 0.3f && tradeMenu.ShipAway && _waypointIndex < _currentWaypoint.Count-1)
        {
            _navMeshAgent.destination = _currentWaypoint[_waypointIndex].position;
        }
        if (waypointDistance <= 0.1f && _waypointIndex < _currentWaypoint.Count-1)
        {
            _waypointIndex++;
            _navMeshAgent.destination = _currentWaypoint[_waypointIndex].position;
        }
        
        if (preDockingDistance <= 0.3f && timeManager.TimeLeft <= 0)
        {
            _navMeshAgent.destination = dockingPosition.position;
        }
        if (dockingDistance <= 0.1f && !_singleDelivery)
        {
            if (tradeMenu.OilAmount > 0)
            {
                storage.Oil += tradeMenu.OilAmount;
            }
            if (tradeMenu.OilAmount < 0)
            {
                storage.Coins += tradeMenu.ProfitCoins;
            }
            _singleDelivery = true;
            tradeMenu.ShipAway = false;
            tradeMenu.ResetTrading();
        }
    }

    public void FollowPath()
    {
        _waypointIndex = 0;
        _navMeshAgent.destination = _currentWaypoint[_waypointIndex].position;
        timeManager.StartShipTimer();
        tradeMenu.ShipAway = true;
    }

    public void ReturnToDocks()
    {
        _singleDelivery = false;
        _navMeshAgent.destination = dockingPosition.position;
    }

    private List<Transform> GetShipWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in shipPath)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }
}