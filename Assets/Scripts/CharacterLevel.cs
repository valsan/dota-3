using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLevel : MonoBehaviour
{
    [SerializeField] private int _levelUpExperience = 100;
    private int _accumulatedExperience = 0;

    public int Level { get; private set; } = 1;

    public delegate void IntEvent(int value);
    public event IntEvent OnLevelUp;
    public void TakeExperience(int amount)
    {
        _accumulatedExperience += amount;

        while(_accumulatedExperience >= _levelUpExperience)
        {
            Level++;
            _accumulatedExperience = -_levelUpExperience;
        }
    }
}
