/**
 * A Star based on playlist tutorial found here: https://www.youtube.com/watch?v=-L-WgKMFuhE&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW&index=1
 */

using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour 
{
    public Transform player;
    public Vector2 gridSize;
    public float nodeRadius;
    float nodeDiameter;
    int gridSizeX, gridSizeY;

    public LayerMask unwalkableMask;

    Node[,] grid;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        CreateGrid();
    }

    //Vector3 worldBottomLeft = transform.position - Vector3.right * gridSizeX / 2 * nodeDiameter - Vector3.forward * gridSizeY / 2 * nodeDiameter;

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridSizeX/2 * nodeDiameter- Vector3.forward * gridSizeY / 2 * nodeDiameter;

        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));

                grid[x, y] = new Node(walkable, worldPoint); 
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        Vector3 localPosition = new Vector3(worldPosition.x - transform.position.x - nodeRadius, 0, worldPosition.z - transform.position.z - nodeRadius);
        float percentX = (localPosition.x + gridSize.x / 2) / gridSize.x;
        float percentY = (localPosition.z + gridSize.y / 2) / gridSize.y;
        int x = Mathf.RoundToInt((gridSizeX) * percentX);
        x = Mathf.Clamp(x, 0, gridSizeX - 1);
        int y = Mathf.RoundToInt((gridSizeY) * percentY);
        y = Mathf.Clamp(y, 0, gridSizeY - 1);
        return grid[x, y];
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1, gridSize.y));

        if (grid != null)
        {
            Node playerNode = NodeFromWorldPoint(player.position);

            foreach (Node n in grid)
            {
                if (n.walkable)
                {
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.red;
                }

                if (playerNode.worldPosition == n.worldPosition)
                {
                    Gizmos.color = Color.cyan;
                }

                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }

}
