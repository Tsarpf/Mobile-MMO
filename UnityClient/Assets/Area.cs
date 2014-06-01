using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    class Area
    {
        Dictionary<string, Player> playersInRoom;



        public void joinRemotePlayer(string playername) 
        {
            playersInRoom.Add(playername, new Player());
        }

    }
}
