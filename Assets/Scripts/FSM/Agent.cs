using UnityEngine;
using System.Collections;

namespace FSM
{
    public abstract class Agent : MonoBehaviour
    {
        private static int agents = 0;


        public Location TargetLocation { get; set; }
        public Location Location { get; set; }

        public float speed = 1;
        Vector3[] path;
        int targetIndex;

        public int ID
        {
            get;
            private set;
        }

        public Agent()
        {
            ID = agents++;
        }

        public void ChangeLocation(Vector3 currentTarget)
        {
            PathRequestManager.RequestPath(transform.position, currentTarget, OnPathFound);
        }

        public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
        {
            if (pathSuccessful)
            {
                path = newPath;

                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
        }

        IEnumerator FollowPath()
        {
            Vector3 currentWayPoint = path[0];

            while (true)
            {
                if (transform.position == currentWayPoint)
                {
                    targetIndex++;

                    if (targetIndex >= path.Length)
                    {
                        targetIndex = 0;
                        path = new Vector3[0];
                        yield break;
                    }

                    currentWayPoint = path[targetIndex];
                }

                currentWayPoint.y = transform.position.y; // 

                transform.position = Vector3.MoveTowards(transform.position, currentWayPoint, speed * Time.deltaTime);
                yield return null;
            }
        }

        public void OnDrawGizmos()
        {
            if (path != null)
            {
                for (int i = targetIndex; i < path.Length; i++)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(path[i], Vector3.one);

                    if (i == targetIndex)
                    {
                        Gizmos.DrawLine(transform.position, path[i]);
                    }
                    else
                    {
                        Gizmos.DrawLine(path[i - 1], path[i]);
                    }
                }
            }
        }

        abstract public bool HandleMessage(Telegram telegram);
    }
}
