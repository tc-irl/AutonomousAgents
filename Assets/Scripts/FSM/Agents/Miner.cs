﻿using UnityEngine;
using System.Collections;

namespace FSM
{
    public class Miner : Agent
    {
        public int MaxNuggets = 3;
        public int ThirstLevel = 5;
        public int ComfortLevel = 5;
        public int TirednessThreshold = 5;

        private StateMachine<Miner> stateMachine;
        private int wifeId; 
        private int goldCarrying;
        private int moneyInBank;
        private int howThirsty;
        private int howFatigued;

        public StateMachine<Miner> StateMachine
        {
            get { return stateMachine; }
            set { stateMachine = value; }
        }

        public int WifeId
        {
            get { return wifeId; }
            set { wifeId = value; }
        }
         
        public int GoldCarrying
        {
            get { return goldCarrying; }
            set { goldCarrying = value; }
        }
        public int MoneyInBank
        {
            get { return moneyInBank; }
            set { moneyInBank = value; }
        }

        public int HowThirsty
        {
            get { return howThirsty; }
            set { howThirsty = value; }
        }
        public int HowFatigued
        {
            get { return howFatigued; }
            set { howFatigued = value; }
        }

        public Miner()
            : base()
        {
            stateMachine = new StateMachine<Miner>(this);
            stateMachine.CurrentState = new GoHomeAndSleepTillRested();
            stateMachine.GlobalState = new MinerGlobalState();
            wifeId = this.ID + 1;  // hack hack
        }

        void Start()
        {
            StartCoroutine(PerformUpdate());
        }
        
        // Update is called once per frame
        IEnumerator PerformUpdate()
        {
            while (true)
            {
                howThirsty += 1;
                stateMachine.Update();
                yield return new WaitForSeconds(0.8f);

            }
           
        }

        public override bool HandleMessage(Telegram telegram)
        {
            return stateMachine.HandleMessage(telegram);
        }

        public override bool HandleSense(Sense sense)
        {
            return stateMachine.HandleSense(sense);
        }

        public bool PocketsFull()
        {
            if (goldCarrying >= MaxNuggets)
                return true;
            else
                return false;
        }

        // This method checks whether the agent is thirsty or not, depending on the predefined level
        public bool Thirsty()
        {
            if (howThirsty >= ThirstLevel)
                return true;
            else
                return false;
        }

        // This method checks whether the agent is fatigued or not, depending on the predefined level
        public bool Fatigued()
        {
            if (howFatigued >= TirednessThreshold)
                return true;
            else
                return false;
        }

        // This method checks whether the agent feels rich enough, depending on the predefined level
        public bool Rich()
        {
            if (moneyInBank >= ComfortLevel)
                return true;
            else
                return false;
        }
    }
}
