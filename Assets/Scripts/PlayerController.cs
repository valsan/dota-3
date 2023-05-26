using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _clickParticle;
    [SerializeField] private HeroAI _heroAI;

    private NavMeshAgent _navMeshAgent;

    private Character _characterSelf => _heroAI.GetComponent<Character>();
    private void Awake()
    {
        _navMeshAgent = _heroAI.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            OnRightClick();
        }
    }

    private void OnRightClick()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            _navMeshAgent.SetDestination(raycastHit.point);

            // Targeting an enemy
            if (raycastHit.transform.TryGetComponent(out Character character))
            {
                if (character != _characterSelf)
                {
                    _heroAI.Target = character;
                }
            }

            // Targeting the ground
            else
            {
                _heroAI.Target = null;
                _navMeshAgent.SetDestination(raycastHit.point);

                // Move Particle up a little so it doesn't clip in the ground
                _clickParticle.transform.position = raycastHit.point - ray.direction.normalized;
                _clickParticle.Play();
            }
        }
    }
}
