using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CreepAI : MonoBehaviour
{
    private int _wayCount = 0;

    private NavMeshAgent _navMeshAgent;
    public List<Transform> WayPoints { get; set; }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _navMeshAgent.SetDestination(WayPoints[_wayCount++].position);
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
                    if(_wayCount < WayPoints.Count)
                    {
                        _navMeshAgent.SetDestination(WayPoints[_wayCount++].position);
                    }
                }
            }
        }
    }

    private void LateUpdate()
    {
        // Rotate manually
        if (_navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(_navMeshAgent.velocity.normalized);
        }
    }
}
