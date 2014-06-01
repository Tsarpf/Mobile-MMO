using Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    class Player
    {
        GameObject prefab = (GameObject)Resources.Load("enemy", typeof(GameObject));
        public Player()
        {
            GameObject instance = GameObject.Instantiate(prefab) as GameObject;
            var derp = instance.GetComponent<PlayerMonoBehaviour>();
            //derp.Initialize(this);
        }

        public void Update()
        {

        }

        public void Start()
        {

        }



    }