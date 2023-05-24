using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Stats", order = 1)]
public class Stats : ScriptableObject
{
    [SerializeField] private float _damage;
    [SerializeField] private float _attackRange;

    public float Damage => _damage;
    public float AttackRange => _attackRange;
}
