using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;

    public Character Target { get; private set; }
    public Character Attacker { get; private set; }
    public float Damage { get; private set; }
    public void Shoot(Character target, Character attacker, float damage)
    {
        Target = target;
    }

    void Update()
    {
        if (Target == null) return;

        Vector3 direction = (Target.transform.position - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;

        if((Target.transform.position - transform.position).magnitude < 0.5f)
        {
            Target.GetComponent<IDamageable>().Damage(new DamageInfo(Attacker, Damage));
            Destroy(gameObject);
        }
    }

}
