using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum PlayerState
{
    Moving,
    Attacking,
}
public class HeroAI : MonoBehaviour
{
    [SerializeField] private Stats _playerStats;
    [SerializeField] private Animator _animator;

    public Character Target { get; set; }
    public PlayerState State { get; private set; } = PlayerState.Moving;

    private NavMeshAgent _navMeshAgent;
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Attack()
    {
        if (Target == null) return;
        Target.GetComponent<Enemy>().Damage(20);
    }

    private void Update()
    {
        // If I have a target, follow it
        if (Target != null)
        {
            _navMeshAgent.stoppingDistance = _playerStats.AttackRange;
            _navMeshAgent.SetDestination(Target.transform.position);

            // Check distance from target
            if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (State == PlayerState.Moving)
                {
                    State = PlayerState.Attacking;
                    _animator.SetTrigger("AutoAttack");
                }
            }
            else
            {
                if (State == PlayerState.Attacking)
                {
                    State = PlayerState.Moving;
                    _animator.SetTrigger("StopAttack");
                }
            }
        }
        else
        {
            _navMeshAgent.stoppingDistance = 0;
            if (State == PlayerState.Attacking)
            {
                State = PlayerState.Moving;
                _animator.SetTrigger("StopAttack");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _playerStats.AttackRange);
    }
}
