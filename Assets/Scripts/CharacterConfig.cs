using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterConfig : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
}
