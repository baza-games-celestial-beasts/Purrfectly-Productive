using System;
using UnityEngine;

[Serializable]
public class Cooldown
{
    [SerializeField] private float valueOfCooldown;

    private float _timesUp;
    
    public float ValueOfCooldown
    {
        get => valueOfCooldown;
        set => valueOfCooldown = value;
    }

    public void Reset()
    {
        _timesUp = Time.time + valueOfCooldown;
    }

    public bool IsReady => _timesUp <= Time.time;
}