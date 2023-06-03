using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private NavMeshAgent _navMeshAgent;
    private Character _character;
    private void Awake()
    {
        _navMeshAgent = GetComponentInParent<NavMeshAgent>();
        _character = GetComponentInParent<Character>();

        _character.OnDeath += () =>
        {
            Debug.Log("DIE ANIMATION");
            _animator.SetTrigger("Die");
        };
    }

    private void Update()
    {
        if (_animator != null)
        {
            _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude / _navMeshAgent.speed);
        }
    }
}
