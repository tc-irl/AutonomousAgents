using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FSM
{
    public enum MessageType
    {
        HiHoneyImHome, 
        StewsReady
    }

    public struct Telegram
    {
        public double DispatchTime;
        public int Sender;
        public int Receiver;
        public MessageType messageType;

        public Telegram(double DispatchTime, int Sender, int Receiver, MessageType messageType)
        {
            this.DispatchTime = DispatchTime;
            this.Sender = Sender;
            this.Receiver = Receiver;
            this.messageType = messageType;
        }
    }

    public static class Message 
    {
        public static List<Telegram> telegramQueue = new List<Telegram>();

        
        public static void DispatchMessage(double delay, int sender, int receiver, MessageType messageType)
        {
            var agentManager = Object.FindObjectOfType<AgentManager>();
            Agent sendingAgent = agentManager.GetAgent(sender);
            Agent receivingAgent = agentManager.GetAgent(receiver);

            Telegram telegram = new Telegram(0, sender, receiver, messageType);

            if (delay <= 0.0f)
            {
                Debug.Log("Instant telegram dispatched by " + sender + " for " + receiver + " message is " + messageType.ToString());
                SendMessage(receivingAgent, telegram);
            }
            else
            {
                telegram.DispatchTime = Time.time + delay;
                telegramQueue.Add(telegram);
                Debug.Log ("Delayed telegram from " + sender + " recorded at time " + Time.time);
            }
        }

        // This sends any messages that are due for delivery; invoked at each tick by the game's Update() method
        public static void SendDelayedMessages()
        {
            var agentManager = Object.FindObjectOfType<AgentManager>();
            for (int i = 0; i < telegramQueue.Count; i++)
            {
                if (telegramQueue[i].DispatchTime <= Time.time)
                {
                    Agent receivingAgent = agentManager.GetAgent(telegramQueue[i].Receiver);
                    SendMessage(receivingAgent, telegramQueue[i]);
                    telegramQueue.RemoveAt(i);
                }
            }
        }

        // Attempt to send a message to a particular agent; called by the preceding two methods -- don't call this from your own agents
        public static void SendMessage(Agent agent, Telegram telegram)
        {
            if (!agent.HandleMessage(telegram))
            {
                Debug.Log("Message not handled");
            }
        }
    }

}

