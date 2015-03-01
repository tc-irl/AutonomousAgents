using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FSM
{
    public class AgentManager : MonoBehaviour
    {
        static List<Agent> listOfAgents = new List<Agent>();

        public static int AddAgent(Agent agent)
        {
            listOfAgents.Add(agent);
            return listOfAgents.IndexOf(agent);
        }

        public static Agent GetAgent(int id)
        {
            return listOfAgents[id];
        }

        public static void RemoveAgent(Agent agent)
        {
            listOfAgents.Remove(agent);
        }
    }
}
