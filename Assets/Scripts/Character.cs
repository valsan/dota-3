using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Team
{
    Radiant,
    Dire
}
public class Character : MonoBehaviour, ITargetable, IDamageable
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private Image _healthImage;

    [SerializeField] private Team team;
    
    public Team Team { get { return team; } }

    public float Health { get; private set; }

    private void Awake()
    {
        Health = _maxHealth;
    }

    public void Damage(float amount)
    {
        Health -= amount;
        _healthImage.fillAmount = Health / _maxHealth;
    }
}
