using UnityEngine;
using System.Collections;

namespace FSM
{
    public abstract class Agent
    {
        private static int agents = 0;
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public Agent()
        {
            id = agents++;
        }

        abstract public void Update();
        abstract public bool HandleMessage(Telegram telegram);
    }
}
