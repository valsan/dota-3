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

    private void Start()
    {
        Health = _maxHealth;

        PlayerController.SelectableUnits.Add(this);
    }

    public void Damage(float amount)
    {
        Health -= amount;
        _healthImage.fillAmount = Health / _maxHealth;
    }
}
