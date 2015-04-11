/**
 * A Star based on playlist tutorial found here: https://www.youtube.com/watch?v=-L-WgKMFuhE&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW&index=1
 */

using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node>
{
    public bool walkable;
    public Vector3 worldPosition;

    public int gCost;
    public int hCost;
    public int gridX;
    public int gridY;

    public int movementPenalty;

    int heapIndex;
    public int nodeAttenutation;
    
    public Node parent;

    public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY, int penalty)
    {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;
        movementPenalty = penalty;
    }

    // Sample way of adding attenutation factors into my world

    //public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY, int penalty, LayerMask nodeType)
    //{
    //    this.walkable = walkable;
    //    this.worldPosition = worldPosition;
    //    this.gridX = gridX;
    //    this.gridY = gridY;
    //    movementPenalty = penalty;

    //    if (nodeType.ToString() == "Mountain")
    //    {
    //        nodeAttenutation = 5;
    //    }
    //    else if (nodeType.ToString() == "River")
    //    {
    //        nodeAttenutation = 1;
    //    }

    //    // or possibly checking if the node is walkable or not

    //    if (this.walkable)
    //    {
    //        nodeAttenutation = 1;
    //    }
    //    else
    //    {
    //        nodeAttenutation = 3;
    //    }
    //}

    public int fCost
    {
        get { return gCost + hCost; }
    }

    public int HeapIndex
    {
        get { return heapIndex; }
        set { heapIndex = value; }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);

        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }

        return -compare;
    }

}
