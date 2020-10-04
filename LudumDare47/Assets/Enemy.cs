using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public AIPath aiPath;

    public Animator animator;

    public float attackRate;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        FindObjectOfType<PlayerHealth>().TakeDamage(damage);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Treasure"))
        {
            aiPath.canMove = false;
            animator.SetBool("Walk", false);

            InvokeRepeating("Attack", attackRate, attackRate);
        }
    }
}
