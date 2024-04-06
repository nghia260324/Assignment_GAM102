using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public GameObject effect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Player"))
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerController>()?.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
