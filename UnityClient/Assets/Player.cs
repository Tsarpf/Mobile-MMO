using Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    class Player
    {
        GameObject prefab = (GameObject)Resources.Load("enemy", typeof(GameObject));
        GameObject playerObj;
        Vector2 position;
        //PlayerMonoBehaviour 
        public Player()
        {
            GameObject instance = GameObject.Instantiate(prefab) as GameObject;
            var derp = instance.GetComponent<PlayerMonoBehaviour>();
            //derp.Initialize(this);
        }

        public void Update()
        {
            position = playerObj.transform.position;


            

        }

        public void Start()
        {

        }

        public void moveTo(Vector2 targetpos)
        {
            
            //Vector3 tgt = new Vector3(targetpos.x, 0, targetpos.y);
            if (!V2Equal(targetpos, position))
            {
                Vector3 direction = targetpos - position;
                direction = new Vector3(direction.normalized.x, 0, direction.normalized.z);
                playerObj.transform.rigidbody.velocity = direction * 3;
            }

        }

        bool V2Equal(Vector2 a, Vector2 b)
        {
            a = new Vector3(a.x, a.y, 0);
            b = new Vector3(b.x, b.y, 0);
            return Vector3.SqrMagnitude(a - b) < 0.001;
        }

    }