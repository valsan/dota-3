using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public enum Team
{
    Radiant,
    Dire
}
public class Character : MonoBehaviour, ITargetable, IDamageable
{
    [SerializeField] private CharacterConfig _config;
    [SerializeField] private float _maxHealth;
    [SerializeField] private Image _healthImage;
    [SerializeField] private Team team;
    public Team Team { get { return team; } }
    public CharacterConfig Config { get { return _config; } }

    public float Health { get; private set; }
    public bool IsDead => Health <= 0;

    public delegate void OnDeathHandler();
    public event OnDeathHandler OnDeath;

    private void Start()
    {
        Health = _maxHealth;

        PlayerController.SelectableUnits.Add(this);
    }

    public void Damage(DamageInfo damageInfo)
    {
        if(IsDead) return;
        Health -= Mathf.Max(damageInfo.Amount, 0);
        _healthImage.fillAmount = Health / _maxHealth;

        if(IsDead)
        {
            OnDeath?.Invoke();
            Destroy(gameObject, 2f);
        }
    }
}
