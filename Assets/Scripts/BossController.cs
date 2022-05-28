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
        }
        Debug.Log("Win");
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
        bossHp -= damage;
    }
}
