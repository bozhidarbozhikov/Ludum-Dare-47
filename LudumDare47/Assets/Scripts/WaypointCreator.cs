using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaypointCreator : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject waypointPrefab;
    public GameObject startWaypoint;
    public GameObject finishWaypoint;

    //[HideInInspector]
    public List<Vector3> waypoints = new List<Vector3>();
    Transform waypoint;

    int roadTileCounter = 0;
    bool roadTileBool = false;


    private Vector3Int currentWaypointPos = new Vector3Int();
    private Vector3Int lastWaypointPos = new Vector3Int();
    [SerializeField]
    private Vector3Int startWaypointPos;

    private Vector3Int upTile = new Vector3Int();
    private Vector3Int downTile = new Vector3Int();
    private Vector3Int rightTile = new Vector3Int();
    private Vector3Int leftTile = new Vector3Int();

    // Start is called before the first frame update
    void Awake()
    {
        startWaypointPos = Vector3Int.RoundToInt(startWaypoint.gameObject.transform.position);
        CreateWaypoints();
    }

    void CreateWaypoints()
    {
        //CreateStartAndFinish(startWaypointPos);

        currentWaypointPos = Vector3Int.RoundToInt(tilemap.WorldToCell(startWaypoint.transform.position));

        while (!roadTileBool)
        {
            waypoint = Instantiate(waypointPrefab, tilemap.CellToWorld(currentWaypointPos) + new Vector3(0.5f, 0.5f), Quaternion.identity).transform;
            waypoint.name = roadTileCounter.ToString();


            upTile = Vector3Int.RoundToInt(tilemap.WorldToCell(waypoint.position)) + Vector3Int.up;
            downTile = Vector3Int.RoundToInt(tilemap.WorldToCell(waypoint.position)) + Vector3Int.down;
            rightTile = Vector3Int.RoundToInt(tilemap.WorldToCell(waypoint.position)) + Vector3Int.right;
            leftTile = Vector3Int.RoundToInt(tilemap.WorldToCell(waypoint.position)) + Vector3Int.left;

            if (tilemap.HasTile(upTile) && upTile != lastWaypointPos && upTile != startWaypointPos)
            {
                lastWaypointPos = currentWaypointPos;
                currentWaypointPos = upTile;
            }
            else if (tilemap.HasTile(downTile) && downTile != lastWaypointPos && downTile != startWaypointPos)
            {
                lastWaypointPos = currentWaypointPos;
                currentWaypointPos = downTile;
            }
            else if (tilemap.HasTile(rightTile) && rightTile != lastWaypointPos && rightTile != startWaypointPos)
            {
                lastWaypointPos = currentWaypointPos;
                currentWaypointPos = rightTile;
            }
            else if (tilemap.HasTile(leftTile) && leftTile != lastWaypointPos && leftTile != startWaypointPos)
            {
                lastWaypointPos = currentWaypointPos;
                currentWaypointPos = leftTile;
            }
            else
            {
                roadTileBool = true;
            }

            waypoints.Add(waypoint.position);

            roadTileCounter++;
        }
    }
}
