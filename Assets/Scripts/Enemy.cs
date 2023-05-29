using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private Image _healthImage;
    public float Health { get ; private set; }

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
