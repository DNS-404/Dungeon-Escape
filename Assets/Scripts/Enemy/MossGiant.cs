using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamagable
{
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
        _anim.SetTrigger("Hit");
        _anim.SetBool("InCombat", true);

        if(Health <= 0)
        {
            Die();
        }
    }
}
