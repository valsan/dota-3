using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[Serializable]
public class Cooldown
{
    [SerializeField] private float _duration;
    public float Duration => _duration;
    public float StartTime { get; private set; }
    public float EndTime => StartTime + Duration;
    public bool IsCoolingDown { get; private set; }
    public float ElapsedTime => Time.time - StartTime;

    //public void PutOnCooldown()
    //{
    //    StartCoroutine(PutOnCooldownCoroutine());
    //}
    
    public IEnumerator PutOnCooldownCoroutine()
    {
        IsCoolingDown = true;
        StartTime = Time.time;
        yield return new WaitForSeconds(Duration);
        IsCoolingDown = false;
    }
}
