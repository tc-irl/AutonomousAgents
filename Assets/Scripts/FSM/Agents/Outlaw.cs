using UnityEngine;
using System.Collections;

namespace FSM
{
    public class Outlaw : Agent
    {
        public int TakeFromBank = 3;
        public int TakeFromShack = 3;
        public int TakeFromSaloon = 3;

        private int moneyFromBank;
        private int moneyFromShack;
        private int moneyFromSaloon;

        private StateMachine<Outlaw> stateMachine;

        public StateMachine<Outlaw> StateMachine
        {
            get { return stateMachine; }
            set { stateMachine = value; }
        }

        public int MoneyFromBank
        {
            get { return moneyFromBank; }
            set { moneyFromBank = value; }
        }

        public int MoneyFromShack
        {
            get { return moneyFromShack; }
            set { moneyFromShack = value; }
        }

        public int MoneyFromSaloon
        {
            get { return moneyFromSaloon; }
            set { moneyFromSaloon = value; }
        }

        public Outlaw()
            : base()
        {
            stateMachine = new StateMachine<Outlaw>(this);
            stateMachine.CurrentState = new RobBank();
            stateMachine.GlobalState = new OutlawGlobalState();
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


        //public override bool HandleSense(Sense sense)
        //{
        //    return stateMachine.HandleSense(sense);
        //}

        public bool BankEmpty()
        {
            if (moneyFromBank >= TakeFromBank)
                return true;
            else
                return false;
        }

        public bool ShackEmpty()
        {
            if (moneyFromShack >= TakeFromShack)
                return true;
            else
                return false;
        }

        public bool SaloonEmpty()
        {
            if (moneyFromSaloon >= TakeFromSaloon)
                return true;
            else
                return false;
        }
    }
}
