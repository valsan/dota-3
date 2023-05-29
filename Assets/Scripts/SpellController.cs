using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    [SerializeField] private float _radius = 3f;
    [SerializeField] private Cooldown _spell1Cooldown;
    [SerializeField] private ParticleSystem _explosionFX;
    public Cooldown Spell1Cooldown => _spell1Cooldown;
    
    private Character _character;
    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    public void Spell1()
    {
        if (Spell1Cooldown.IsCoolingDown) return;
        StartCoroutine(Spell1Cooldown.PutOnCooldownCoroutine());

        _explosionFX.Play();
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Character character))
            {
                if(character.Team != _character.Team)
                {
                    character.Damage(20f);
                }
            }
        }
    }
}
