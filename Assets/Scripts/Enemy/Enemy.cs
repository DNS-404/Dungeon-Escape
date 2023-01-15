using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // protected: only those who inherit it can modify it
    [SerializeField] protected float health;
    [SerializeField] protected float speed;
    [SerializeField] protected int gems;
    [SerializeField] protected float sight;
    [SerializeField] protected bool isAlive;

    [SerializeField] protected Transform pointA = null, pointB = null;
    [SerializeField] protected GameObject player;
    [SerializeField] protected GameObject gemObject;

    enum Target { A, B }
    Target currentTarget;

    protected Vector3 currentTargetPosition;
    protected Animator _anim;
    protected SpriteRenderer _sprite;

    public virtual void Init()
    {
        if (pointA != null && pointB != null)
        {
            currentTarget = Target.B;
            currentTargetPosition = pointB.position;
        }
        isAlive = true;
        _anim = GetComponentInChildren<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (!isAlive) return;

        if (pointA != null && pointB != null)
        {
            // if walking, move
            if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                Move();
        }
        
        // if in combat mode, face player
        if (_anim.GetBool("InCombat"))
        {
            float dist = player.transform.position.x - transform.position.x;
            if (dist >= 0)
            {
                _sprite.flipX = false;
            }
            else
            {
                _sprite.flipX = true;
            }
            // if player is out of range, get out of combat mode
            if (Math.Abs(dist) > sight)
                _anim.SetBool("InCombat", false);
        }
    }

    private bool ReachedTarget()
    {
        if (currentTarget == Target.A)
        {
            return (transform.position.x - pointA.position.x < 0.2);
        }
        return (pointB.position.x - transform.position.x < 0.2);
    }

    private void ChangeTarget()
    {
        currentTarget = (currentTarget == Target.A) ? Target.B : Target.A;
        currentTargetPosition = (currentTarget == Target.A) ? pointA.position : pointB.position;
    }

    private void SetIdleAnimation()
    {
        _anim.SetTrigger("Idle");
    }

    public virtual void Move()
    {
        if (currentTarget == Target.A)
        {
            _sprite.flipX = true;
        }
        else
        {
            _sprite.flipX = false;
        }
        if (ReachedTarget())
        {
            ChangeTarget();
            SetIdleAnimation();
        }
        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, currentTargetPosition, step);
    }

    public void Die()
    {
        isAlive = false;
        _anim.SetTrigger("Death");
        GameObject gem = Instantiate(gemObject, transform.position, Quaternion.identity);
        gem.GetComponent<Gem>().value = gems;
        Destroy(this.gameObject, 3f);
    }
}
