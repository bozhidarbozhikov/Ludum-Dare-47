using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spider : MonoBehaviour
{
    public GameObject cobwebPrefab;

    public Tilemap tilemap;
    List<Vector3Int> rails = new List<Vector3Int>();
    BoundsInt bounds = new BoundsInt();

    public float radius;
    public float attackDelay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ThrowCobweb());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator ThrowCobweb()
    {
        yield return new WaitForSeconds(attackDelay);

        bounds.SetMinMax(Vector3Int.RoundToInt(new Vector3(transform.position.x - radius, transform.position.y - radius)), Vector3Int.RoundToInt(new Vector3(transform.position.x + radius, transform.position.y + radius)));

        for (int i = bounds.xMin; i < bounds.xMax; i++)
        {
            for (int j = bounds.yMin; j < bounds.yMax; j++)
            {
                if (tilemap.HasTile(new Vector3Int(i, j, 0)))
                {
                    rails.Add(new Vector3Int(i, j, 0));
                }
            }
        }

        Vector3Int tilePos = PickRandomTile();

        if (CanSpawnCobweb(tilePos))
        {
            Instantiate(cobwebPrefab, tilePos + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
        }

        StartCoroutine(ThrowCobweb());
    }

    Vector3Int PickRandomTile()
    {
        return rails[Random.Range(0, rails.Count)];
    }

    bool CanSpawnCobweb(Vector3Int tilePos)
    {
        Vector2 tile = new Vector2();
        tile.x = tilePos.x;
        tile.y = tilePos.y;

        Collider2D[] hit = Physics2D.OverlapCircleAll(tile, 0.5f);

        foreach (Collider2D col in hit)
        {
            if (col.GetComponent<Cobweb>() != null)
            {
                return false;
            }
        }

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(radius * 2, radius * 2));
    }
}
