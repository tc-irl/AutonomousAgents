using UnityEngine;
using System.Collections;

namespace FSM
{
    public enum SenseType
    {
        Sight,
        Hearing,
        Smell,
        Touch
    }

    public struct Sense
    {
        public int Sender;
        public int Receiver;
        public SenseType senseType;

        public Sense(double DispatchTime, int Sender, int Receiver, SenseType senseType)
        {
            this.Sender = Sender;
            this.Receiver = Receiver;
            this.senseType = senseType;
        }
    };

    public class SenseManager : MonoBehaviour
    {

    }
}
