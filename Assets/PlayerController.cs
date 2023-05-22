using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _previewSpehere;
    [SerializeField] private Stats _playerStats;

    private NavMeshAgent _navMeshAgent;

    Character target;
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1)) 
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane;
            
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if(Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                _previewSpehere.transform.position = raycastHit.point;
                _navMeshAgent.SetDestination(raycastHit.point);

                if(raycastHit.transform.TryGetComponent<Character>(out Character character))
                {
                    target = character;
                } else
                {
                    target = null;
                    _navMeshAgent.SetDestination(raycastHit.point);
                }
            }
        }

        // If I have a target, follow it
        if(target != null)
        {
            print("TARGET");
            _navMeshAgent.SetDestination(target.transform.position);

        }

        Vector3 destination = _navMeshAgent.destination;
        float distanceToDestination = (destination - transform.position).magnitude;
        if(distanceToDestination  <= _playerStats.AttackRange)
        {
            _navMeshAgent.isStopped = true;
            // Handle Attack
        } else
        {
            _navMeshAgent.isStopped = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _playerStats.AttackRange);
    }
}
