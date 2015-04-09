using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace FSM
{
    public class Unit : MonoBehaviour
    {
        public Miner miner;
        public Transform currentTarget;
        public List<Transform> possibleLocations = new List<Transform>();

        //public float speed = 1;
        //Vector3[] path;
        //int targetIndex;

        //int currentLocation;

        void Start()
        {
            //currentLocation = (int)miner.location; // hacky way of doing it

            //switch (currentLocation)
            //{
            //    case 0:
            //        {
            //            currentTarget = possibleLocations[0];
            //            break;
            //        }
            //    case 1:
            //        {
            //            currentTarget = possibleLocations[1];
            //            break;
            //        }
            //    case 2:
            //        {
            //            currentTarget = possibleLocations[2];
            //            break;
            //        }
            //    case 3:
            //        {
            //            currentTarget = possibleLocations[3];
            //            break;
            //        }
            //}

           
        }

    }
}