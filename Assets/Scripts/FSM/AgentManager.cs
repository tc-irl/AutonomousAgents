using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FSM
{
    public class AgentManager : MonoBehaviour
    {
        public List<Agent> listOfAgents;

        public int AddAgent(Agent agent)
        {
            listOfAgents.Add(agent);
            return listOfAgents.IndexOf(agent);
        }

        public Agent GetAgent(int id)
        {
            var selectedAgent = (from agent in listOfAgents
                                 where agent.ID == id
                                 select agent).FirstOrDefault();

            return selectedAgent;
        }


        public void RemoveAgent(Agent agent)
        {
            listOfAgents.Remove(agent);
        }
    }
}