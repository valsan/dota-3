using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float _range = 3f;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _attackStartTransform;

    public Character Target { get; private set; }

    private Character _characterSerlf;

    private void Awake()
    {
        _characterSerlf = GetComponent<Character>();
    }

    private void Update()
    {
        FindTarget();

        if(Target != null)
        {
            ShootTarget(Target);
        }
    }

    IEnumerator ShootTarget(Character target)
    {
        if(target == null) { yield break; }
        while(Target == target)
        {
            Projectile projectile = Instantiate(_projectilePrefab, _attackStartTransform.position, _attackStartTransform.rotation);
            projectile.SetTarget(Target);
            yield return new WaitForSeconds(1f);
        }
    }

    private void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _range, _layerMask);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Character character))
            {
                if (character.Team != _characterSerlf.Team && character != Target)
                {
                    Target = character;
                    StartCoroutine(ShootTarget(Target));
                    break;
                }
            }
        }
    }
}
