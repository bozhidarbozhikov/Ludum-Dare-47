using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelTile : MonoBehaviour
{
    public Animator animator;

    public float explosionRadius;
    public float explosionDamage;
    public float barrelSpawnDelay;
    public float health;

    public CapsuleCollider2D capcol;
    public SpriteRenderer spriteRenderer;
    public Target target;


    public void ExplodeBarrel()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position + Vector3.up / 4, explosionRadius);

        foreach (Collider2D col in hit)
        {
            if (col.CompareTag("Enemy"))
            {
                col.GetComponent<Target>().TakeDamage(explosionDamage);
            }
        }

        StartCoroutine(SpawnBarrel());
    }

    public IEnumerator SpawnBarrel()
    {
        animator.ResetTrigger("Spawn");
        animator.SetTrigger("Destroy");

        yield return new WaitForSeconds(barrelSpawnDelay);

        animator.ResetTrigger("Destroy");
        animator.SetTrigger("Spawn");
        GetComponentInChildren<Target>().health = health;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up / 4, explosionRadius);
    }
}
