using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Camera cam;
    public float damage;

    public Transform firepoint;
    public Rigidbody2D rb;
    public LineRenderer lineRenderer;

    Vector3 mousePos;
    Vector2 lookDir;
    float angle;

    private void Start()
    {
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        lookDir = mousePos - transform.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;

        rb.rotation = angle;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(firepoint.position, transform.rotation * Vector3.up);

        if (hit)
        {
            Target target = hit.transform.GetComponent<Target>();

            if (target != null) target.TakeDamage(damage);

            StartCoroutine(Line(hit));
        }
    }

    IEnumerator Line(RaycastHit2D hit)
    {
        lineRenderer.enabled = true;

        lineRenderer.SetPosition(0, firepoint.position);
        lineRenderer.SetPosition(1, hit.point);

        yield return new WaitForSeconds(0.05f);

        lineRenderer.enabled = false;
    }
}



