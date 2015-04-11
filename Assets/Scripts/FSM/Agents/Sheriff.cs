using UnityEngine;
using System.Collections;

namespace FSM
{
    public class Sheriff : Agent
    {
        public int ScanArea = 3;
        private int currentScanTime;

        private StateMachine<Sheriff> stateMachine;

        public StateMachine<Sheriff> StateMachine
        {
            get { return stateMachine; }
            set { stateMachine = value; }
        }

        public int CurrentScanTime
        {
            get { return currentScanTime; }
            set { currentScanTime = value; }
        }

        public Sheriff()
            : base()
        {
            stateMachine = new StateMachine<Sheriff>(this);
            stateMachine.CurrentState = new PatrolArea1();
            stateMachine.GlobalState = new SheriffGlobalState();
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
      
        public bool ScannedEnough()
        {
            if (currentScanTime >= ScanArea)
                return true;
            else
                return false;
        }
    }
}
