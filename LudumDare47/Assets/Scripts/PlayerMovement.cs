using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public WaypointCreator waypointCreator;
    public Rigidbody2D rb;
    public Transform minecart;
    public float speed;
    public bool canMove = true;
    List<Vector3> waypoints;

    int index = 0;
    Vector3 direction;
    Vector3 currentWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waypointCreator.waypoints;
        currentWaypoint = waypoints[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, currentWaypoint) < 0.1f && canMove)
        {
            index++;

            if (index < waypoints.Count - 1)
            {
                currentWaypoint = waypoints[index];
                TurnMinecart(currentWaypoint);
            }
            else
            {
                index = 0;
                currentWaypoint = waypoints[0];
                TurnMinecart(currentWaypoint);
            }
        }
    }

    private void FixedUpdate()
    {
        direction = currentWaypoint - transform.position;

        if (canMove)
            transform.Translate(direction.normalized * speed * Time.fixedDeltaTime);
    }

    void TurnMinecart(Vector3 destination)
    {
        if ((int)destination.x == (int)transform.position.x)
        {
            minecart.eulerAngles = Vector3.zero;
        }
        else if ((int)destination.y == (int)transform.position.y)
        {
            minecart.eulerAngles = Vector3.forward * 90;
        }
    }
}
