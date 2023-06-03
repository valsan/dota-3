using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageInfo
{
    public Character Attacker;
    public float Amount;

    public DamageInfo(Character attacker, float amount)
    {
        Attacker = attacker;
        Amount = amount;
    }
}
