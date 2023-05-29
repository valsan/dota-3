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

    // Ranged auto attack
    [SerializeField] bool _isRanged;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _attackStartTransform;

    public PlayerState CurrentState { get; private set; } = PlayerState.Moving;
    public Character Target { get; set; }
    public float DistanceToTarget => (Target.transform.position - transform.position).magnitude;
    public bool IsTargetInRange => DistanceToTarget <= _playerStats.AttackRange;

    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Attack()
    {
        if(_isRanged)
        {
            RangedAttack();
        } else
        {
            MeleeAttack();
        }
    }

    private void RangedAttack()
    {
        if (Target == null) return;
        Projectile projectile = Instantiate(_projectilePrefab, _attackStartTransform.position, _attackStartTransform.rotation);
        projectile.SetTarget(Target);
    }


    private void MeleeAttack()
    {
        if (Target == null) return;
        Target.GetComponent<IDamageable>().Damage(20);
    }

    private void Update()
    {
        // If I have a target, follow it
        if (Target != null)
        {
            _navMeshAgent.SetDestination(Target.transform.position);

            if (IsTargetInRange)
            {
                if (CurrentState == PlayerState.Moving)
                {
                    _navMeshAgent.isStopped = true;
                    CurrentState = PlayerState.Attacking;
                    _animator.SetTrigger("AutoAttack");
                }
            }
            else
            {
                if (CurrentState == PlayerState.Attacking)
                {
                    CurrentState = PlayerState.Moving;
                    _animator.SetTrigger("StopAttack");
                    _navMeshAgent.isStopped = false;
                }
            }
        }
        else
        {
            if (CurrentState == PlayerState.Attacking)
            {
                _navMeshAgent.isStopped = false;
                CurrentState = PlayerState.Moving;
                _animator.SetTrigger("StopAttack");
            }
        }
    }

    private void LateUpdate()
    {
        if(CurrentState == PlayerState.Attacking && Target != null)
        {
            transform.LookAt(Target.transform);
        }

        // Rotate manually
        if (_navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(_navMeshAgent.velocity.normalized);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _playerStats.AttackRange);
    }
}
