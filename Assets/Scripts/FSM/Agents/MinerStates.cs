﻿using UnityEngine;
using System.Collections;

namespace FSM
{

    public class WalkingTo : State<Miner>
    {
        public override void Enter(Miner miner)
        {
            var locManager = Object.FindObjectOfType<LocationManager>();

            //miner.Say(string.Format("Walkin' to {0}", e.Agent.TargetLocation));
            miner.ChangeLocation(locManager.Locations[miner.TargetLocation].position);

        }

        public override void Execute(Miner miner)
        {
            var locManager = Object.FindObjectOfType<LocationManager>();

            var target = locManager.Locations[miner.TargetLocation].position;


            target.y = 0;

            if (Vector3.Distance(target, miner.transform.position) <= 3.0f)
            {
                miner.Location = miner.TargetLocation;
                miner.StateMachine.RevertToPreviousState();
            }



        }
        public override void Exit(Miner agent)
        {
            //throw new System.NotImplementedException();
        }

        public override bool OnMessage(Miner agent, Telegram telegram)
        {
            // throw new System.NotImplementedException();
            return true;
        }

        public override bool OnSense(Miner agent, Sense sense)
        {
            // throw new System.NotImplementedException();
            return false;
        }
    }

    public class EnterMineAndDigForNugget : State<Miner>
    {
        public override void Enter(Miner miner)
        {
            Debug.Log(miner.ID + " Walkin' to the goldmine");
            //var goldMinePosition = GameObject.FindGameObjectWithTag("Mine");
            miner.TargetLocation = Location.goldMine;

            if (miner.Location != miner.TargetLocation)
            {
                miner.StateMachine.ChangeState(new WalkingTo());
            }

        }

        public override void Execute(Miner miner)
        {
            miner.GoldCarrying += 1;
            miner.HowFatigued += 1;
            Debug.Log(miner.ID + " Pickin' up a nugget");

            if (miner.PocketsFull())
            {
                miner.StateMachine.ChangeState(new VisitBankAndDepositGold());
            }

            if (miner.Thirsty())
            {
                miner.StateMachine.ChangeState(new QuenchThirst());
            }
        }

        public override void Exit(Miner miner)
        {
            if (miner.Location == miner.TargetLocation)
                Debug.Log(miner.ID + "Ah'm leaving the gold mine with mah pockets full o' sweet gold");
        }

        public override bool OnMessage(Miner agent, Telegram telegram)
        {
            return false;
        }

        public override bool OnSense(Miner agent, Sense sense)
        {
            // throw new System.NotImplementedException();
            return false;
        }

    }

    // In this state, the miner goes to the bank and deposits gold
    public class VisitBankAndDepositGold : State<Miner>
    {
        public override void Enter(Miner miner)
        {
            Debug.Log(miner.ID + " Goin' to the bank. Yes siree");

            miner.TargetLocation = Location.bank;

            if (miner.Location != miner.TargetLocation)
            {
                miner.StateMachine.ChangeState(new WalkingTo());
            }
        }

        public override void Execute(Miner miner)
        {
            miner.MoneyInBank += miner.GoldCarrying;
            miner.GoldCarrying = 0;

            Debug.Log(miner.ID + " Depositing gold. Total savings now: " + miner.MoneyInBank);
            if (miner.Rich())
            {
                Debug.Log(miner.ID + " WooHoo! Rich enough for now. Back home to mah li'lle lady");
                miner.StateMachine.ChangeState(new GoHomeAndSleepTillRested());
            }
            else
            {
                miner.StateMachine.ChangeState(new EnterMineAndDigForNugget());
            }
        }

        public override void Exit(Miner miner)
        {
            Debug.Log(miner.ID + " Leavin' the Bank");
        }

        public override bool OnMessage(Miner agent, Telegram telegram)
        {
            return false;
        }

        public override bool OnSense(Miner agent, Sense sense)
        {
            // throw new System.NotImplementedException();
            return false;
        }
    }

    // In this state, the miner goes home and sleeps
    public class GoHomeAndSleepTillRested : State<Miner>
    {
        public override void Enter(Miner miner)
        {
            Debug.Log(miner.ID + " Walkin' Home");
            miner.TargetLocation = Location.shack;

            if (miner.Location != miner.TargetLocation)
            {
                miner.StateMachine.ChangeState(new WalkingTo());
            }
            else
                Message.DispatchMessage(0, miner.ID, miner.WifeId, MessageType.HiHoneyImHome);
        }

        public override void Execute(Miner miner)
        {
            if (miner.HowFatigued < miner.TirednessThreshold)
            {
                Debug.Log(miner.ID + " All mah fatigue has drained away. Time to find more gold!");
                miner.StateMachine.ChangeState(new EnterMineAndDigForNugget());
            }
            else
            {
                miner.HowFatigued--;
                Debug.Log(miner.ID + " ZZZZZ....");
            }
        }

        public override void Exit(Miner miner)
        {

        }

        public override bool OnMessage(Miner miner, Telegram telegram)
        {
            switch (telegram.messageType)
            {
                case MessageType.HiHoneyImHome:
                    return false;
                case MessageType.StewsReady:
                    Debug.Log("Message handled by " + miner.ID + " at time ");
                    Debug.Log(miner.ID + " Okay Hun, ahm a comin'!");
                    miner.StateMachine.ChangeState(new EatStew());
                    return true;
                default:
                    return false;
            }
        }

        public override bool OnSense(Miner agent, Sense sense)
        {
            // throw new System.NotImplementedException();
            return false;
        }
    }

    // In this state, the miner goes to the saloon to drink
    public class QuenchThirst : State<Miner>
    {
        public override void Enter(Miner miner)
        {
            miner.TargetLocation = Location.saloon;

            if (miner.Location != miner.TargetLocation)
            {
                miner.StateMachine.ChangeState(new WalkingTo());
            }
        }

        public override void Execute(Miner miner)
        {
            // Buying whiskey costs 2 gold but quenches thirst altogether
            miner.HowThirsty = 0;
            miner.MoneyInBank -= 2;
            Debug.Log(miner.ID + " That's mighty fine sippin' liquer");
            miner.StateMachine.ChangeState(new EnterMineAndDigForNugget());
        }

        public override void Exit(Miner miner)
        {
            Debug.Log(miner.ID + " Leaving the saloon, feelin' good");
        }

        public override bool OnMessage(Miner agent, Telegram telegram)
        {
            return false;
        }

        public override bool OnSense(Miner agent, Sense sense)
        {
            // throw new System.NotImplementedException();
            return false;
        }
    }

    // In this state, the miner eats the food that Elsa has prepared
    public class EatStew : State<Miner>
    {
        public override void Enter(Miner miner)
        {
            Debug.Log(miner.ID + " Smells Reaaal goood Elsa!");
        }

        public override void Execute(Miner miner)
        {
            Debug.Log(miner.ID + " Tastes real good too!");
            miner.StateMachine.RevertToPreviousState();
        }

        public override void Exit(Miner miner)
        {
            Debug.Log(miner.ID + " Thankya li'lle lady. Ah better get back to whatever ah wuz doin'");
        }

        public override bool OnMessage(Miner agent, Telegram telegram)
        {
            return false;
        }

        public override bool OnSense(Miner agent, Sense sense)
        {
            // throw new System.NotImplementedException();
            return false;
        }
    }

    // If the agent has a global state, then it is executed every Update() cycle
    public class MinerGlobalState : State<Miner>
    {
        public override void Enter(Miner miner)
        {

        }

        public override void Execute(Miner miner)
        {

        }

        public override void Exit(Miner miner)
        {

        }

        public override bool OnMessage(Miner agent, Telegram telegram)
        {
            return false;
        }

        public override bool OnSense(Miner agent, Sense sense)
        {
            // throw new System.NotImplementedException();
            return false;
        }
    }

}
