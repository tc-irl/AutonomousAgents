using UnityEngine;
using System.Collections;

namespace FSM
{
    public enum SenseType
    {
        Sight,
        Hearing,
        Smell,
        Touch
    }

    public struct Sense
    {
        public int Sender;
        public int Receiver;
        public SenseType senseType;


        public Sense(int Sender, int Receiver, SenseType senseType)
        {
            this.Sender = Sender;
            this.Receiver = Receiver;
            this.senseType = senseType;
        }
    };

    public class SenseManager : MonoBehaviour
    {
        Pathfinding path;
        // Only want to use sensing between sheriff and outlaw, 
        // can easily be expanded to be used for all agents
        AgentManager manager;
        Agent sheriff;
        Agent outlaw;

        public float range = 30.0f;

        void Awake()
        {
            manager = GameObject.FindGameObjectWithTag("AgentManager").GetComponent<AgentManager>();
            sheriff = GameObject.FindGameObjectWithTag("Sheriff").GetComponent<Agent>();
            outlaw = GameObject.FindGameObjectWithTag("Outlaw").GetComponent<Agent>();
            path = gameObject.GetComponent <Pathfinding>();
        }

        void Update()
        {
            if (Vector3.Distance(outlaw.transform.position, sheriff.transform.position) < range)
            {

                //if (GameObject.FindGameObjectWithTag("A*").GetComponent<Pathfinding>().SensePath(sheriff.transform.position, outlaw.transform.position))
                //{
                //    Sense sense = new Sense(sheriff.ID, outlaw.ID, SenseType.Sight);
                //    sheriff.HandleSense(sense);
                //}

                if (CheckForRayIntersection())
                {
                    Sense sense = new Sense(sheriff.ID, outlaw.ID, SenseType.Sight);
                    sheriff.HandleSense(sense);
                }
            }
        }

        bool CheckForRayIntersection()
        {
            RaycastHit hit;
            var rayDirection = outlaw.transform.position - sheriff.transform.position;

            if (Physics.Raycast(sheriff.transform.position, rayDirection, out hit))
            {
                if (hit.transform == outlaw.transform)
                {
                    Debug.DrawRay(sheriff.transform.position, rayDirection, Color.green);
                    return true;
                }
                else
                {
                    Debug.DrawRay(sheriff.transform.position, rayDirection, Color.red);
                    return false;
                }
            }

            return false;
        }
        


        //public float range = 5.0f;

       //for(int i = 0; i < manager.listOfAgents.Count(); i++;)
       // {

       // }

        //void Update()
        //{
        //    if (Vector3.Distance(sheriff.transform.position, outlaw.transform.position) < range)
        //    {

        //    }
        //}
    }
}
