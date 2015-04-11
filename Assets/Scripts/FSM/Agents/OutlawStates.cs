using UnityEngine;
using System.Collections;

namespace FSM
{
    public class OutlawWalkingTo : State<Outlaw>
    {
        public override void Enter(Outlaw outlaw)
        {
            var locManager = Object.FindObjectOfType<LocationManager>();

            //miner.Say(string.Format("Walkin' to {0}", e.Agent.TargetLocation));
            outlaw.ChangeLocation(locManager.Locations[outlaw.TargetLocation].position);

        }

        public override void Execute(Outlaw outlaw)
        {
            var locManager = Object.FindObjectOfType<LocationManager>();

            var target = locManager.Locations[outlaw.TargetLocation].position;

            target.y = 0;

            if (Vector3.Distance(target, outlaw.transform.position) <= 3.0f)
            {
                outlaw.Location = outlaw.TargetLocation;
                outlaw.StateMachine.RevertToPreviousState();
            }

        }
        public override void Exit(Outlaw outlaw)
        {
            //throw new System.NotImplementedException();
        }

        public override bool OnMessage(Outlaw outlaw, Telegram telegram)
        {
            // throw new System.NotImplementedException();
            return true;
        }

        public override bool OnSense(Outlaw agent, Sense sense)
        {
            // throw new System.NotImplementedException();
            return false;
        }

    }

    public class RobBank : State<Outlaw>
    {
        public override void Enter(Outlaw outlaw)
        {
            Debug.Log(outlaw.ID + "Time to rob the bank and there is nothin' that dumb ol' sheriff can do");

            outlaw.TargetLocation = Location.bank;

            if (outlaw.Location != outlaw.TargetLocation)
            {
                outlaw.StateMachine.ChangeState(new OutlawWalkingTo());
            }
        }

        public override void Execute(Outlaw outlaw)
        {
            outlaw.MoneyFromBank += 1;

            Debug.Log(outlaw.ID + "I'll be taking all of you money bank");

            if (outlaw.BankEmpty())
            {
                outlaw.MoneyFromBank = 0;
                Debug.Log(outlaw.ID + " Time to move on");
                outlaw.StateMachine.ChangeState(new RobShack());
            }
            else
            {
                outlaw.StateMachine.ChangeState(new RobBank());
            }
        }

        public override void Exit(Outlaw outlaw)
        {
            Debug.Log(outlaw.ID + "I've cleaned out this bank, time to rob ol' Bob and Elsa");
        }

        public override bool OnMessage(Outlaw sheriff, Telegram telegram)
        {
            return false;
        }

        public override bool OnSense(Outlaw agent, Sense sense)
        {
            // throw new System.NotImplementedException();
            return false;
        }

    }

    public class RobShack : State<Outlaw>
    {
        public override void Enter(Outlaw outlaw)
        {
            Debug.Log(outlaw.ID + "Time to rob the shack and there is nothin' that dumb ol' sheriff can do");

            outlaw.TargetLocation = Location.shack;

            if (outlaw.Location != outlaw.TargetLocation)
            {
                outlaw.StateMachine.ChangeState(new OutlawWalkingTo());
            }
        }

        public override void Execute(Outlaw outlaw)
        {
            outlaw.MoneyFromShack += 1;

            Debug.Log(outlaw.ID + "Give me all of your money Bob and Elsa");

            if (outlaw.ShackEmpty())
            {
                outlaw.MoneyFromShack = 0;
                Debug.Log(outlaw.ID + " Time to move on from here");
                outlaw.StateMachine.ChangeState(new RobSaloon());
            }
            else
            {
                outlaw.StateMachine.ChangeState(new RobShack());
            }
        }

        public override void Exit(Outlaw outlaw)
        {
            Debug.Log(outlaw.ID + "I've cleaned out this shack, time to rob the saloon");
        }

        public override bool OnMessage(Outlaw outlaw, Telegram telegram)
        {
            return false;
        }

        public override bool OnSense(Outlaw agent, Sense sense)
        {
            // throw new System.NotImplementedException();
            return false;
        }

    }

    public class RobSaloon : State<Outlaw>
    {
        public override void Enter(Outlaw outlaw)
        {
            Debug.Log(outlaw.ID + "Time to rob the saloon and there is nothin' that dumb ol' sheriff can do");

            outlaw.TargetLocation = Location.saloon;

            if (outlaw.Location != outlaw.TargetLocation)
            {
                outlaw.StateMachine.ChangeState(new OutlawWalkingTo());
            }
        }

        public override void Execute(Outlaw outlaw)
        {
            outlaw.MoneyFromSaloon += 1;

            Debug.Log(outlaw.ID + "I'll be taking all of you money saloon");

            if (outlaw.SaloonEmpty())
            {
                outlaw.MoneyFromSaloon = 0;
                Debug.Log(outlaw.ID + " Time to get outta here ");

                outlaw.StateMachine.ChangeState(new RobBank());
            }
            else
            {
                outlaw.StateMachine.ChangeState(new RobSaloon());
            }
        }

        public override void Exit(Outlaw outlaw)
        {
            Debug.Log(outlaw.ID + "I've cleaned out this bank, time to rob ol' Bob and Elsa");
        }

        public override bool OnMessage(Outlaw outlaw, Telegram telegram)
        {
            return false;
        }
        public override bool OnSense(Outlaw agent, Sense sense)
        {
            // throw new System.NotImplementedException();
            return false;
        }

    }
    
    // If the agent has a global state, then it is executed every Update() cycle
    public class OutlawGlobalState : State<Outlaw>
    {
        public override void Enter(Outlaw outlaw)
        {

        }

        public override void Execute(Outlaw outlaw)
        {

        }

        public override void Exit(Outlaw outlaw)
        {

        }

        public override bool OnMessage(Outlaw outlaw, Telegram telegram)
        {
            return false;
        }

        public override bool OnSense(Outlaw agent, Sense sense)
        {
            // throw new System.NotImplementedException();
            return false;
        }
    }
}
