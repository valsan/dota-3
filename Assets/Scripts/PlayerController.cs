using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _previewSpehere;
    [SerializeField] private Stats _playerStats;
    [SerializeField] private Animator _animator;

    private NavMeshAgent _navMeshAgent;

    private Character target;
    public enum PlayerState
    {
        Moving,
        Attacking,
    }

    public PlayerState State { get; private set; } = PlayerState.Moving;

    private Character _characterSelf;
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _characterSelf = GetComponent<Character>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            OnRightClick();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            _animator.SetTrigger("AutoAttack");
        }

        // If I have a target, follow it
        if (target != null)
        {
            _navMeshAgent.SetDestination(target.transform.position);
            // Check distance from target
            if(!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if(State == PlayerState.Moving)
                {
                    State = PlayerState.Attacking;
                    _animator.SetTrigger("AutoAttack");
                }
            } else
            {
                if (State == PlayerState.Attacking)
                {
                    State = PlayerState.Moving;
                    _animator.SetTrigger("StopAttack");
                }
            }
        } else
        {
            if(State == PlayerState.Attacking)
            {
                State = PlayerState.Moving;
                _animator.SetTrigger("StopAttack");
            }
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
                if (character != _characterSelf)
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

    public void Attack()
    {
        if (target == null) return;
        target.GetComponent<Enemy>().Damage(20);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _playerStats.AttackRange);
    }
}
