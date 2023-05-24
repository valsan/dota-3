using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _previewSpehere;
    [SerializeField] private Stats _playerStats;

    private NavMeshAgent _navMeshAgent;

    private Character target;

    private Character _character;
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _character = GetComponent<Character>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            OnRightClick();
        }

        // If I have a target, follow it
        if (target != null)
        {
            _navMeshAgent.SetDestination(target.transform.position);
        }
    }

    private void OnRightClick()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            _previewSpehere.transform.position = raycastHit.point;
            _navMeshAgent.SetDestination(raycastHit.point);

            // Targeting an enemy
            if (raycastHit.transform.TryGetComponent(out Character character))
            {
                if (character != _character)
                {
                    target = character;
                    _navMeshAgent.stoppingDistance = _playerStats.AttackRange;
                }
            }

            // Targeting the ground
            else
            {
                target = null;
                _navMeshAgent.stoppingDistance = 0;
                _navMeshAgent.SetDestination(raycastHit.point);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _playerStats.AttackRange);
    }
}
