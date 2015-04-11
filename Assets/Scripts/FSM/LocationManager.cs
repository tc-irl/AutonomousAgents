using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace FSM
{

    public class LocationManager : MonoBehaviour
    {
        public Dictionary<Location, Transform> Locations = new Dictionary<Location, Transform>();

        void Awake()
        {
            var saloon = GameObject.FindGameObjectWithTag("Saloon");
            if (saloon == null) throw new NullReferenceException("Saloon game object is not available!");

            Locations.Add(Location.saloon, saloon.transform);

            var home = GameObject.FindGameObjectWithTag("Shack");
            if (home == null) throw new NullReferenceException("Home game object is not available!");

            Locations.Add(Location.shack, home.transform);

            var mine = GameObject.FindGameObjectWithTag("Mine");
            if (mine == null) throw new NullReferenceException("Mine game object is not available!");

            Locations.Add(Location.goldMine, mine.transform);

            var bank = GameObject.FindGameObjectWithTag("Bank");
            if (bank == null) throw new NullReferenceException("Bank game object is not available!");

            Locations.Add(Location.bank, bank.transform);

            var patrol1 = GameObject.FindGameObjectWithTag("Patrol1");
            if (patrol1 == null) throw new NullReferenceException("Patrol 1 is not available!");

            Locations.Add(Location.patrol1, patrol1.transform);

            var patrol2 = GameObject.FindGameObjectWithTag("Patrol2");
            if (patrol2 == null) throw new NullReferenceException("Patrol 2 is not available!");

            Locations.Add(Location.patrol2, patrol2.transform);

            var outlaw = GameObject.FindGameObjectWithTag("Outlaw");
            if (outlaw == null) throw new NullReferenceException("Outlaw is not available!");

            Locations.Add(Location.outlaw, outlaw.transform);
        }

        void Update()
        {
            var outlaw = GameObject.FindGameObjectWithTag("Outlaw");
            Locations[Location.outlaw] = outlaw.transform;
        }
    } 
}
