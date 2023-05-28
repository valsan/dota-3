using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Radiant,
    Dire
}
public class Character : MonoBehaviour, ITargetable
{
    [SerializeField] private Team team;
    
    public Team Team { get { return team; } }
}
