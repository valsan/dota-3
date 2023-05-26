using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private NavMeshAgent _navMeshAgent;
    private void Awake()
    {
        _navMeshAgent = GetComponentInParent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_animator != null)
        {
            _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude / _navMeshAgent.speed);
        }
    }
}
