using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Enemy"))
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<EnemyController>()?.TakeDamage(damage);
            }
            EffectManager.instance.PlayEffectBomb(transform);
            Destroy(gameObject);
        }
    }
}
