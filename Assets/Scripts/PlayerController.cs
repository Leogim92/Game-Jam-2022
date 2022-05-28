using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
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
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(health <= 0)
        {
            Debug.Log("GameOver");
            return;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && OnGround())
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
        else if(Input.GetKey(KeyCode.DownArrow) && OnGround())
        {
            normalCollider.gameObject.SetActive(false);
            crouchCollider.gameObject.SetActive(true);
        }
        else if(Input.GetKeyUp(KeyCode.DownArrow) && OnGround())
        {
            normalCollider.gameObject.SetActive(true);
            crouchCollider.gameObject.SetActive(false);
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, attackPosition.position, Quaternion.identity);
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
        health -= damage;
    }
}
