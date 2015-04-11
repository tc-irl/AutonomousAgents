using UnityEngine;
using System.Collections;

namespace FSM
{
    public class SheriffWalkingTo : State<Sheriff>
    {
        public override void Enter(Sheriff sheriff)
        {
            var locManager = Object.FindObjectOfType<LocationManager>();

            //miner.Say(string.Format("Walkin' to {0}", e.Agent.TargetLocation));
            sheriff.ChangeLocation(locManager.Locations[sheriff.TargetLocation].position);

        }

        public override void Execute(Sheriff sheriff)
        {
            var locManager = Object.FindObjectOfType<LocationManager>();

            var target = locManager.Locations[sheriff.TargetLocation].position;


            target.y = 0;

            if (Vector3.Distance(target, sheriff.transform.position) <= 3.0f)
            {
                sheriff.Location = sheriff.TargetLocation;
                sheriff.StateMachine.RevertToPreviousState();
            }

        }
        public override void Exit(Sheriff sheriff)
        {
            //throw new System.NotImplementedException();
        }

        public override bool OnMessage(Sheriff sheriff, Telegram telegram)
        {
            // throw new System.NotImplementedException();
            return true;
        }

    }
 
    public class PatrolArea1 : State<Sheriff>
    {
        private bool doOnce = true;

        public override void Enter(Sheriff sheriff)
        {
            Debug.Log(sheriff.ID + "Better check out area 1 for that darn outlaw");

            sheriff.TargetLocation = Location.patrol1;

            if (sheriff.Location != sheriff.TargetLocation)
            {
                sheriff.StateMachine.ChangeState(new SheriffWalkingTo());
            }
        }

        public override void Execute(Sheriff sheriff)
        {
            if (doOnce)
            {
                sheriff.CurrentScanTime = 0;
                doOnce = false;
            }

            sheriff.CurrentScanTime += 1;

            Debug.Log(sheriff.ID + " Patrolling Area 1 ");

            if (sheriff.ScannedEnough())
            {
                Debug.Log(sheriff.ID + " Time to move on and patrol area 2 for that outlaw");
                sheriff.StateMachine.ChangeState(new PatrolArea2());
            }
            else
            {
                sheriff.StateMachine.ChangeState(new PatrolArea1());
            }
        }

        public override void Exit(Sheriff sheriff)
        {
            Debug.Log(sheriff.ID + " Leavin' the area");
            doOnce = true;
        }

        public override bool OnMessage(Sheriff sheriff, Telegram telegram)
        {
            return false;
        }

    }

    public class PatrolArea2 : State<Sheriff>
    {
        private bool doOnce = true;

        public override void Enter(Sheriff sheriff)
        {
            Debug.Log(sheriff.ID + "Better check out area 2 for that darn outlaw");

            sheriff.TargetLocation = Location.patrol2;

            if (sheriff.Location != sheriff.TargetLocation)
            {
                sheriff.StateMachine.ChangeState(new SheriffWalkingTo());
            }
        }

        public override void Execute(Sheriff sheriff)
        {
            if (doOnce)
            {
                sheriff.CurrentScanTime = 0;
                doOnce = false;
            }

            sheriff.CurrentScanTime += 1;

            Debug.Log(sheriff.ID + " Patrolling Area 2 ");

            if (sheriff.ScannedEnough())
            {
                Debug.Log(sheriff.ID + " Time to move on and patrol area 1 for that outlaw");
                sheriff.StateMachine.ChangeState(new PatrolArea1());
            }
            else
            {
                sheriff.StateMachine.ChangeState(new PatrolArea2());
            }
        }

        public override void Exit(Sheriff sheriff)
        {
            Debug.Log(sheriff.ID + " Leavin' the area");
            doOnce = true;
        }

        public override bool OnMessage(Sheriff sheriff, Telegram telegram)
        {
            return false;
        }

    }

    // If the agent has a global state, then it is executed every Update() cycle
    public class SheriffGlobalState : State<Sheriff>
    {
        public override void Enter(Sheriff sheriff)
        {

        }

        public override void Execute(Sheriff sheriff)
        {

        }

        public override void Exit(Sheriff sheriff)
        {

        }

        public override bool OnMessage(Sheriff sheriff, Telegram telegram)
        {
            return false;
        }
    }

}