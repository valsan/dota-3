using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _clickParticle;
    [SerializeField] private HeroAI _heroAI;
    [SerializeField] private RectTransform _rectTransform;

    private NavMeshAgent _navMeshAgent;
    private Character _characterSelf => _heroAI.GetComponent<Character>();

    public bool LeftClickDown { get; private set; }

    Vector3 StartMousePosition;
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _rectTransform.gameObject.SetActive(true);
            StartMousePosition = Input.mousePosition;
            LeftClickDown = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _rectTransform.gameObject.SetActive(false);
            LeftClickDown = false;
        }

        if(LeftClickDown)
        {
            ResizeSelecionBox();
        }
    }
    
    private void ResizeSelecionBox()
    {
        float width = Input.mousePosition.x - StartMousePosition.x;
        float height = Input.mousePosition.y - StartMousePosition.y;

        _rectTransform.anchoredPosition = new Vector2(StartMousePosition.x, StartMousePosition.y ) + new Vector2(width / 2, height / 2);
        _rectTransform.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
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
