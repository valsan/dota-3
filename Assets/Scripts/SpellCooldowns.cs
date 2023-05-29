using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCooldowns : MonoBehaviour
{
    [SerializeField] private Image _spell1Image;

    [SerializeField] private SpellController _spellController;

    private void Update()
    {
        _spell1Image.fillAmount = _spellController.Spell1Cooldown.ElapsedTime / _spellController.Spell1Cooldown.Duration;
    }
}
