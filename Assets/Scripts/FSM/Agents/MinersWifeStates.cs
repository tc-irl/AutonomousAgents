using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FSM
{
    public class DoHouseWork : State<MinersWife>
    {

        public override void Enter(MinersWife minersWife)
        {
            Debug.Log(minersWife.ID + " Time to do some more housework!");
        }

        public override void Execute(MinersWife minersWife)
        {
            switch (Random.Range(0,2))
            {
                case 0:
                    Debug.Log(minersWife.ID + " Moppin' the floor");
                    break;
                case 1:
                    Debug.Log(minersWife.ID + " Washin' the dishes");
                    break;
                case 2:
                    Debug.Log(minersWife.ID + " Makin' the bed");
                    break;
                default:
                    break;
            }
        }

        public override void Exit(MinersWife minersWife)
        {

        }

        public override bool OnMessage(MinersWife minersWife, Telegram telegram)
        {
            return false;
        }

        public override bool OnSense(MinersWife agent, Sense sense)
        {
            // throw new System.NotImplementedException();
            return false;
        }
    }

    public class VisitBathroom : State<MinersWife>
    {
        public override void Enter(MinersWife minersWife)
        {
            Debug.Log(minersWife.ID + " Walkin' to the can. Need to powda mah pretty li'lle nose");
        }

        public override void Execute(MinersWife minersWife)
        {
            Debug.Log(minersWife.ID + " Ahhhhhh! Sweet relief!");
            minersWife.StateMachine.RevertToPreviousState();  // this completes the state blip
        }

        public override void Exit(MinersWife minersWife)
        {
            Debug.Log(minersWife.ID + " Leavin' the Jon");
        }

        public override bool OnMessage(MinersWife minersWife, Telegram telegram)
        {
            return false;
        }

        public override bool OnSense(MinersWife agent, Sense sense)
        {
            // throw new System.NotImplementedException();
            return false;
        }
    }

    public class CookStew : State<MinersWife>
    {
        public override void Enter(MinersWife minersWife)
        {
            if (!minersWife.Cooking)
            {
                // MinersWife sends a delayed message to herself to arrive when the food is ready
                Debug.Log(minersWife.ID + " Putting the stew in the oven");
                Message.DispatchMessage(2, minersWife.ID, minersWife.HusbandId, MessageType.StewsReady);
                minersWife.Cooking = true;
            }
        }

        public override void Execute(MinersWife minersWife)
        {
           Debug.Log(minersWife.ID + " Fussin' over food");
        }

        public override void Exit(MinersWife minersWife)
        {
          Debug.Log(minersWife.ID + " Puttin' the stew on the table");
        }

        public override bool OnMessage(MinersWife minersWife, Telegram telegram)
        {
            switch (telegram.messageType)
            {
                case MessageType.HiHoneyImHome:
                    // Ignored here; handled in WifesGlobalState below
                    return false;
                case MessageType.StewsReady:
                    // Tell Miner that the stew is ready now by sending a message with no delay
                    Debug.Log("Message handled by " + minersWife.ID + " at time ");
                    Debug.Log(minersWife.ID + " StewReady! Lets eat");
                    Message.DispatchMessage(0, minersWife.ID, minersWife.HusbandId, MessageType.StewsReady);
                    minersWife.Cooking = false;
                    minersWife.StateMachine.ChangeState(new DoHouseWork());
                    return true;
                default:
                    return false;
            }
        }

        public override bool OnSense(MinersWife agent, Sense sense)
        {
            // throw new System.NotImplementedException();
            return false;
        }
    }

    public class WifesGlobalState : State<MinersWife>
    {
        public override void Enter(MinersWife minersWife)
        {
           
        }

        public override void Execute(MinersWife minersWife)
        {
            // There's always a 10% chance of a state blip in which MinersWife goes to the bathroom
            if (Random.Range(0,9) == 1 && !minersWife.StateMachine.IsInState(new VisitBathroom()))
            {
                minersWife.StateMachine.ChangeState(new VisitBathroom());
            }
        }

        public override void Exit(MinersWife minersWife)
        {

        }

        public override bool OnMessage(MinersWife minersWife, Telegram telegram)
        {
            switch (telegram.messageType)
            {
                case MessageType.HiHoneyImHome:
                    Debug.Log("Message handled by " + minersWife.ID + " at time ");
                    Debug.Log(minersWife.ID + "Hi honey. Let me make you some of mah fine country stew");
                    minersWife.StateMachine.ChangeState(new CookStew());
                    return true;
                case MessageType.StewsReady:
                    return false;
                default:
                    return false;
            }                 
        }

        public override bool OnSense(MinersWife agent, Sense sense)
        {
            // throw new System.NotImplementedException();
            return false;
        }
    }
}
