using UnityEngine;
using System.Collections;

namespace FSM
{
    public class StateMachine<T>
    {
        private T owner;
        private State<T> currentState = null;
        private State<T> previousState = null;
        private State<T> globalState = null;

        public StateMachine(T owner)
        {
            this.owner = owner;
        }

        // Unity properties
        public State<T> CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }

        public State<T> PreviousState
        {
            get { return previousState; }
            set { previousState = value; }
        }

        public State<T> GlobalState
        {
            get { return globalState; }
            set { globalState = value; }
        }


        // Update is called once per frame
        public void Update()
        {
            if (globalState != null)
            {
                globalState.Execute(owner);
            }

            if (currentState != null)
            {
                currentState.Execute(owner);
            }
        }

        public bool HandleMessage(Telegram telegram)
        {
            if (globalState != null)
            {
                if(globalState.OnMessage(owner,telegram))
                {
                    return true;
                }
            }

            if (currentState != null)
            {
                if (currentState.OnMessage(owner, telegram))
                {
                    return true;
                }
            }

            return false;
        }

        // Handle sense event
        public bool HandleSense(Sense sense)
        {
            if (globalState != null)
            {
                if (globalState.OnSense(owner, sense))
                {
                    return true;
                }

            }
            if (currentState != null)
            {
                if (currentState.OnSense(owner, sense))
                {
                    return true;
                }
            }
            return false;
        }


        public void ChangeState(State<T> newState)
        {
            if (newState == null)
            {
                Debug.Log("Trying to change to a null State");
            }
            else
            {
                previousState = currentState;
                currentState.Exit(owner);
                currentState = newState;
                currentState.Enter(owner);
            }
        }

        public void RevertToPreviousState()
        {
            ChangeState(previousState);
        }

        public bool IsInState(State<T> state)
        {
            return state.Equals(currentState);
        }
    }
}
