using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _playerAnim;
    private Animator _effectsAnim;

    void Start()
    {
        _playerAnim = GetComponentInChildren<Animator>();
        _effectsAnim = transform.GetChild(1).GetComponent<Animator>(); // getting animator of 2nd child
    }

    public void Move(float moveValue)
    {
        _playerAnim.SetFloat("Move", Mathf.Abs(moveValue));
    }

    public void Jump(bool isJumping)
    {
        _playerAnim.SetBool("Jump", isJumping);
    }

    public void Attack()
    {
        _playerAnim.SetTrigger("Attack");
        _effectsAnim.SetTrigger("SwordArc");
    }
    
    public void Death()
    {
        _playerAnim.SetTrigger("Death");
    }
}
