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

        public override bool OnSense(Sheriff agent, Sense sense)
        {
            // throw new System.NotImplementedException();
            return false;
        }
    }


 
    public class PatrolArea1 : State<Sheriff>
    {
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
            sheriff.CurrentScanTime += 1;

            Debug.Log(sheriff.ID + " Patrolling Area 1 ");

            if (sheriff.ScannedEnough())
            {
                sheriff.CurrentScanTime = 0;
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
        }

        public override bool OnMessage(Sheriff sheriff, Telegram telegram)
        {
            return false;
        }

        public override bool OnSense(Sheriff sheriff, Sense sense)
        {
            switch (sense.senseType)
            {
                case SenseType.Touch:
                    return false;
                case SenseType.Hearing:
                    return false;
                case SenseType.Smell:
                    return false;
                case SenseType.Sight:
                    Debug.Log("<color=red> I see you, you darn outlaw </color>");
                    sheriff.StateMachine.ChangeState(new Chase());
                    return true;
                default:
                    return false;
            }

            // throw new System.NotImplementedException();
        }
    }

    public class PatrolArea2 : State<Sheriff>
    {
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
            sheriff.CurrentScanTime += 1;

            Debug.Log(sheriff.ID + " Patrolling Area 2 ");

            if (sheriff.ScannedEnough())
            {
                sheriff.CurrentScanTime = 0;
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
        }

        public override bool OnMessage(Sheriff sheriff, Telegram telegram)
        {
            return false;
        }

        public override bool OnSense(Sheriff sheriff, Sense sense)
        {
            switch (sense.senseType)
            {
                case SenseType.Touch:
                    return false;
                case SenseType.Hearing:
                    return false;
                case SenseType.Smell:
                    return false;
                case SenseType.Sight:
                    Debug.Log("<color=red> I see you, you darn outlaw </color>");
                    sheriff.StateMachine.ChangeState(new Chase());
                    return true;
                default:
                    return false;
            }

            // throw new System.NotImplementedException();
        }
    }

    public class Chase : State<Sheriff>
    {
        public override void Enter(Sheriff sheriff)
        {
            Debug.Log(sheriff.ID + "I'm gonna catch you!" );

            sheriff.TargetLocation = Location.outlaw;

            var outlaw = GameObject.FindGameObjectWithTag("Outlaw");

            var locManager = Object.FindObjectOfType<LocationManager>();

           sheriff.ChangeLocation(outlaw.transform.position);
        }

        public override void Execute(Sheriff sheriff)
        {
            Debug.Log(sheriff.ID + "I'll chase you until the end of days!");

            sheriff.StateMachine.ChangeState(new Chase()); // Just keep chasing
        }

        public override void Exit(Sheriff sheriff)
        {
            Debug.Log(sheriff.ID + " Leavin' the area");
        }

        public override bool OnMessage(Sheriff sheriff, Telegram telegram)
        {
            return false;
        }

        public override bool OnSense(Sheriff sheriff, Sense sense)
        {
            switch (sense.senseType)
            {
                case SenseType.Touch:
                    return false;
                case SenseType.Hearing:
                    return false;
                case SenseType.Smell:
                    return false;
                case SenseType.Sight:
                    Debug.Log("<color=red> I see you, you darn outlaw </color>");
                    sheriff.StateMachine.ChangeState(new Chase());
                    return true;
                default:
                    return false;
            }

            // throw new System.NotImplementedException();
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

        public override bool OnSense(Sheriff agent, Sense sense)
        {
            // throw new System.NotImplementedException();
            return false;
        }

    }

}