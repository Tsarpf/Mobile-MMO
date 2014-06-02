
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    public class PlayerMonoBehaviour : MonoBehaviour
    {

        Player player;
        public void Initialize(Player player)
        {
            this.player = player;
        }
        void Start()
        {
            player.Start();
        }

        void Update()
        {
            player.Update();
        }
    }