using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamagable
{
    public int gems = 0;
    public bool isInShop = false;

    [SerializeField] private float _health = 4f;
    [SerializeField] private float _jumpStrength = 6.5f;
    [SerializeField] private float _moveSpeed = 3f;
   
    private bool resetJumpNeeded = false;
    private bool isAlive = true;

    private Rigidbody2D _rigidbody;
    private PlayerAnimation _anim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordAnimationSprite;

    public float Health { get; set; }

    void Start()
    {
        isAlive = true;
        isInShop = false;
        Health = _health;
        _rigidbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordAnimationSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isAlive)
            Movement();
        // Debug.DrawRay(transform.position, Vector3.down, Color.green);
    }

    private void Movement()
    {
        float moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal") * _moveSpeed;// Input.GetAxisRaw("Horizontal") * _moveSpeed;
        bool isGrounded = CheckIfOnGround();
        
        // Attack on clicking left mouse button
        if (isGrounded && CrossPlatformInputManager.GetButtonDown("A_Button") && !isInShop)
        {
            _anim.Attack();
        }

        // Flip sprites left or right
        if (moveHorizontal > 0)
        {
            _playerSprite.flipX = false;
            _swordAnimationSprite.flipX = false;
            _swordAnimationSprite.flipY = false;
        }
        else if (moveHorizontal < 0)
        {
            _playerSprite.flipX = true;
            _swordAnimationSprite.flipX = true;
            _swordAnimationSprite.flipY = true;
        }
        float moveVertical = JumpMovement(isGrounded);
        
        _rigidbody.velocity = new Vector2(moveHorizontal, moveVertical);
        _anim.Move(moveHorizontal);
    }

    private float JumpMovement(bool isGrounded)
    {
        float moveVertical = _rigidbody.velocity.y;
        if (isGrounded)
        {
            _anim.Jump(false);
        }
        // Can jump only if on ground
        if (CrossPlatformInputManager.GetButtonDown("B_Button") && isGrounded)
        {
            moveVertical += _jumpStrength;
            _anim.Jump(true);
            StartCoroutine(ResetJumpNeeded());
        }
        return moveVertical;
    }

    private bool CheckIfOnGround()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, 1 << 8);
        
        if (hitInfo.collider != null && !resetJumpNeeded)
        {
            return true;
        }
        return false;
    }

    IEnumerator ResetJumpNeeded()
    {
        resetJumpNeeded = true;
        yield return new WaitForSeconds(0.1f);
        resetJumpNeeded = false;
    }

    public void Damage()
    {
        if (!isAlive) return;
        Health -= 1;
        UIManager.Instance.UpdateLife(Convert.ToInt32(Health));
        if(Health <= 0)
        {
            isAlive = false;
            _anim.Death();
            Destroy(this.gameObject, 5f);
        }
    }
}
