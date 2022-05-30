using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    enum PlayerStates
    {
        Idle,
        Jump,
        Crouch,
        Attack
    }

    [SerializeField] float health = 100f;
    [SerializeField] Transform feetPosition = null;
    [SerializeField] LayerMask groundLayer = default;
    [SerializeField] float groundDistanceCheck = 1f;
    [SerializeField] float jumpForce = 100f;
    [SerializeField] Transform normalCollider = null;
    [SerializeField] Transform crouchCollider = null;

    [Space]
    [SerializeField] Projectile projectile = null;
    [SerializeField] Transform attackPosition = null;
    [SerializeField] float attackRate = 0.3f;

    [Space]
    [SerializeField] AudioClip crouch = null;
    [SerializeField] AudioClip jump = null;
    [SerializeField] AudioClip attack = null;

    PlayerStates playerState = PlayerStates.Idle;

    Rigidbody2D rb;
    Animator animator;

    float attackTimer;
    float originalHp;
    AudioSource audioSource;

    public float Health => health;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        originalHp = health;
    }
    private void Update()
    {
        if(health <= 0)
        {
            return;
        }

        if(playerState == PlayerStates.Idle)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.AddForce(Vector2.up * jumpForce);
                animator.SetBool("Jump", true);
                audioSource.PlayOneShot(jump);

                playerState = PlayerStates.Jump;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                animator.SetBool("Crouch", true);
                normalCollider.gameObject.SetActive(false);
                crouchCollider.gameObject.SetActive(true);
                audioSource.PlayOneShot(crouch);

                playerState = PlayerStates.Crouch;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Attack");
                Instantiate(projectile, attackPosition.position, Quaternion.identity);
                audioSource.PlayOneShot(attack);

                playerState = PlayerStates.Attack;
            }

        }
        else if(playerState == PlayerStates.Jump)
        {
            if(rb.velocity.y < 0 && OnGround())
            {
                animator.SetBool("Jump", false);
                playerState = PlayerStates.Idle;
            }
        }
        else if(playerState == PlayerStates.Crouch)
        {
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                animator.SetBool("Crouch", false);
                normalCollider.gameObject.SetActive(true);
                crouchCollider.gameObject.SetActive(false);

                playerState = PlayerStates.Idle;
            }
        }
        else if(playerState == PlayerStates.Attack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackRate)
            {
                attackTimer = 0;

                playerState = PlayerStates.Idle;
            }
        }
    }


    private bool OnGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(feetPosition.position, Vector2.down, groundDistanceCheck, groundLayer);
        if(hit.transform != null)
        {
            return true;
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(feetPosition.position, (Vector2)feetPosition.position + Vector2.down * groundDistanceCheck);
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Max(0, health - damage);
    }
    public void Heal()
    {
        health = originalHp;
    }
}
