using UnityEngine;
using System.Collections;

namespace FSM
{
    public enum Senses
    {
        Sight,
        Hearing,
        Smell,
        Touch
    }
    public struct Sense
    {
        Senses sense; 

    };

    public class SenseManager : MonoBehaviour
    {
    }
}
