using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CreepAI : MonoBehaviour
{
    [SerializeField] private List<Transform> _wayPoints;

    private int _wayCount = 0;

    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(_wayPoints[_wayCount++].position);
    }

    private void Update()
    {
        if(_navMeshAgent != null)
        {
            // Check if reached destination
            if(!_navMeshAgent.pathPending)
            {
                if(_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    // Set next waypoint if it exists
                    if(_wayCount < _wayPoints.Count)
                    {
                        _navMeshAgent.SetDestination(_wayPoints[_wayCount++].position);
                    }
                }
            }
        }
    }

}
