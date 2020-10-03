using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public WaypointCreator waypointCreator;
    public Rigidbody2D rb;
    public float speed;
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
        if (Vector3.Distance(transform.position, currentWaypoint) < 0.1f)
        {
            index++;

            if (index < waypoints.Count - 1)
            {
                currentWaypoint = waypoints[index];
            }
            else
            {
                index = 0;
                currentWaypoint = waypoints[0];
            }
        }
    }

    private void FixedUpdate()
    {
        direction = currentWaypoint - transform.position;

        transform.Translate(direction.normalized * speed * Time.fixedDeltaTime);
    }
}
