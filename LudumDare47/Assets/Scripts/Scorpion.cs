using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Scorpion : MonoBehaviour
{
    public GameObject spitPrefab;

    public Transform firepoint;
    public AIPath aiPath;
    public Animator animator;
    Transform target;

    public float attackCooldown;
    float cooldown;
    public float attackDelay;
    public float damage;
    public float spitForce;
    bool inRange = false;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>().transform;
        cooldown = -4534562;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && cooldown <= 0)
        {
            StartCoroutine(Attack());
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDelay);

        aiPath.canMove = false;
        animator.SetBool("Walk", false);

        yield return new WaitForSeconds(attackDelay);

        GameObject spit = Instantiate(spitPrefab, firepoint.position, transform.rotation);
        spit.GetComponent<EnemyProjectile>().damage = damage;
        spit.GetComponent<Rigidbody2D>().AddForce((target.position - firepoint.position) * spitForce);
    }

    IEnumerator Cooldown()
    {
        cooldown = attackCooldown;

        while (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            yield return null;
        }

        cooldown = 0;
    }

    bool LineOfSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(firepoint.position, target.position - firepoint.position);

        Debug.LogWarning(hit.transform.name + " " + hit.point);

        if (hit.transform.CompareTag("Treasure")) return true;
        else return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Treasure"))
        {
            inRange = true;
        }
    }
}
