using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float addedForce = 20f;
    Vector2 mousePos;
    public Camera cam;
    public Rigidbody2D rb2d;
    public float damage;
    Vector3 shootpos;
    Vector3 shootdir;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            AimAndShoot();
        }
    }
    void AimAndShoot()
    {
        Vector2 lookDirection = mousePos - rb2d.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        transform.eulerAngles = new Vector3(0, 0, angle);

        RaycastHit2D raycast = Physics2D.Raycast(shootpos, shootdir);

        if(raycast.collider != null)
        {
            Target target = raycast.collider.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }
        }






    }
}



