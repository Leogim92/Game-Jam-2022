using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    [SerializeField] Vector2 direction = Vector2.zero;
    [SerializeField] float speed = 100f;
    [SerializeField] string targetTag = "Player";
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            IDamageable damageable = collision.GetComponentInParent<IDamageable>();
            if(damageable != null)
            {
                damageable.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
