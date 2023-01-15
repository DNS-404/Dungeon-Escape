using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamagable
{
    [SerializeField] GameObject acidPrefab = null;
    public float Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public void Damage()
    {
        if (!isAlive) return;
        Health -= 1;

        if(Health <= 0)
        {
            Die();
        }
    }

    public void Attack()
    {
        Instantiate(acidPrefab, transform);
    }
}
