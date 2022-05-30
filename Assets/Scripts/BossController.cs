using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour, IDamageable
{
    [SerializeField] float bossHp = 100f;
    [Space]
    [SerializeField] Projectile projectile = null;
    [SerializeField] Transform upAttackPosition = null;
    [SerializeField] Transform downAttackPosition = null;
    [SerializeField] float attackRate = 2f;
    [Space]
    [SerializeField] AudioClip attack = null;

    float originalHp;
    Animator animator;
    AudioSource audioSource;
    public float BossHP => bossHp;


    private void Awake()
    {
        originalHp = bossHp;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    IEnumerator Start()
    {
        while(bossHp > 0)
        {
            yield return new WaitForSeconds(attackRate);
            if(bossHp <= 0)
            {
                break;
            }
            ShootProjectile();

            audioSource.PlayOneShot(attack);
            animator.SetTrigger("Attack");
        }
    }

    private void ShootProjectile()
    {
        if(Random.value > 0.5f)
        {
            Instantiate(projectile, upAttackPosition.position, Quaternion.identity);
        }
        else
        {
            Instantiate(projectile, downAttackPosition.position, Quaternion.identity);
        }
    }

    public void TakeDamage(float damage)
    {
        bossHp = Mathf.Max(0, bossHp - damage);
    }
    public void Heal()
    {
        bossHp = originalHp;
    }
}
