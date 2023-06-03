using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CreepAI : MonoBehaviour
{
    [SerializeField] private Stats _playerStats;
    [SerializeField] private float _aggroRange = 2f;
    [SerializeField] private Animator _animator;

    private int _wayCount = 0;

    private NavMeshAgent _navMeshAgent;
    private Character _characterSelf;

    public List<Transform> WayPoints { get; set; } = new List<Transform>();

    public Character Target { get; set; }

    public PlayerState CurrentState { get; private set; } = PlayerState.Moving;

    public float DistanceToTarget => (Target.transform.position - transform.position).magnitude;
    public bool IsTargetInRange => DistanceToTarget <= _playerStats.AttackRange;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _characterSelf = GetComponent<Character>();
    }

    public void Attack()
    {
        MeleeAttack();
    }

    private void MeleeAttack()
    {
        if (Target == null) return;
        Target.GetComponent<IDamageable>().Damage(new DamageInfo(_characterSelf, _playerStats.Damage));
    }

    private void Start()
    {
        if (WayPoints.Count == 0) return;

        _navMeshAgent.SetDestination(WayPoints[_wayCount++].position);
    }

    private void Update()
    {
        if(Target == null)
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
            CheckForEnemiesAround();
        } else
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

    }

    private void CheckForEnemiesAround()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _aggroRange);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Character character))
            {
                if (character != _characterSelf && character.Team != _characterSelf.Team)
                {
                    Target = character;
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
