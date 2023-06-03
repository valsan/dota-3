using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public float Health { get; }
    public void Damage(DamageInfo damageInfo);
}
