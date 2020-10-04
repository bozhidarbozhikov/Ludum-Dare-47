using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Treasure"))
        {
            collision.transform.GetComponent<PlayerHealth>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
