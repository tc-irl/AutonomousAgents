using UnityEngine;
using System.Collections;

namespace FSM
{
    public class MinersWife : Agent
    {
        private StateMachine<MinersWife> stateMachine;
        private int husbandId;
        private Location location;
        private bool cooking;

        public StateMachine<MinersWife> StateMachine
        {
            get { return stateMachine; }
            set { stateMachine = value; }
        }
        public int HusbandId
        {
            get { return husbandId; }
            set { husbandId = value; }
        }

        public Location MinerLocation
        {
            get { return location; }
            set { location = value; }
        }

        public bool Cooking
        {
            get { return cooking; }
            set { cooking = value; }
        }

        public MinersWife() : base()
        {
            stateMachine = new StateMachine<MinersWife>(this);
            stateMachine.CurrentState = new DoHouseWork();
            stateMachine.GlobalState = new WifesGlobalState();
            husbandId = this.ID - 1;  // hack hack
        }

        // Update is called once per frame
        public override void Update()
        {
            stateMachine.Update();
        }

        // This method is invoked when the agent receives a message
        public override bool HandleMessage(Telegram telegram)
        {
            return stateMachine.HandleMessage(telegram);
        }
    }

}
