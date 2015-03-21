/**
 * A Star based on playlist tutorial found here: https://www.youtube.com/watch?v=-L-WgKMFuhE&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW&index=1
 */

using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour 
{
    public bool walkable;
    public Vector3 worldPosition; 

    public Node(bool walkable, Vector3 worldPosition)
    {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
    }


}
