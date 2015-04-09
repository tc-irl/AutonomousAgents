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
        }

    } 
}
