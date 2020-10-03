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

    //    Vector3 mousePos;
    //    public Camera cam;
    //    public Rigidbody2D rb;
    //    public float damage;
    //    Vector3 shootpos;
    //    Vector3 shootdir;
    //    RaycastHit2D hit;
    //    public LineRenderer lineRenderer;

    //    // Update is called once per frame
    //    void Update()
    //    {
    //        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    //        if (Input.GetKeyDown(KeyCode.Mouse0))
    //        {
    //            AimAndShoot();
    //        }
    //    }
    //    void AimAndShoot()
    //    {
    //        /*Vector2 lookDirection = mousePos - rb.position;
    //        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
    //        transform.eulerAngles = new Vector3(0, 0, angle);*/

    //        RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePos - transform.position);

    //        if (hit)
    //        {
    //            Target target = hit.transform.GetComponent<Target>();

    //            if (target != null) target.TakeDamage(damage);

    //            lineRenderer.SetPosition(0, transform.position);
    //            lineRenderer.SetPosition(1, mousePos);
    //        }
    //        else
    //        {
    //        }
    //    }

    //    private void OnDrawGizmosSelected()
    //    {
    //        Gizmos.DrawLine(transform.position, mousePos);
    //    }
}



