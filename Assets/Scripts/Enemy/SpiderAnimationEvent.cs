using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
    Spider _spider;

    private void Start()
    {
        _spider = GetComponentInParent<Spider>();
    }
    public void Fire()
    {
        // Fire poison
        _spider.Attack();
    }
}
