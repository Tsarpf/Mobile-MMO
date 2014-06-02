using UnityEngine;
using System.Collections.Generic;
using System.Net.Sockets;
using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using fastJSON;

using System.Threading;
using Newtonsoft.Json;

public class NetworkManager : MonoBehaviour
{
    Dictionary<string, eventHandlerDes> eventHandlers;
    public delegate void eventHandlerDes(string data);
    Dictionary<string, Player> players = new Dictionary<string, Player>();
	GameObject localPrefab;
	GameObject remotePrefab;

	string localUsername = "tsurba";

	void Start () {

        LoginRequestEvent levent = new LoginRequestEvent();
        levent.username = localUsername;
        levent.password = "test1";
		WriteQueue.Write(levent);


        localPrefab = Resources.Load<GameObject>("LocalPlayer");
        remotePrefab = Resources.Load<GameObject>("RemotePlayer");
		//NetworkLoop network = new NetworkLoop();
		//Thread oThread = new Thread(new ThreadStart(network.RunNetworkLoop));
		//oThread.Start();
        eventHandlers = new Dictionary<string, eventHandlerDes>();
        eventHandlers["moveEvent"] = moveHandler;
        eventHandlers["loginEvent"] = loginHandler;
        eventHandlers["joinAreaEvent"] = joinAreaHandler;
        eventHandlers["info"] = infoHandler;
        eventHandlers["registerEvent"] = registerHandler;
        eventHandlers["remotePlayerJoinEvent"] = remotePlayerJoinHandler;
        eventHandlers["quitEvent"] = quitHandler;
        

	}
	
	void Update () {
        //Get from readqueue
        var read  = ReadQueue.Read();
        if (read == null)
        {
			return;
        }
		Dictionary<string, object> obj = fastJSON.JSON.Parse(read) as Dictionary<string, object>;

		string json = fastJSON.JSON.ToJSON(obj["properties"]);
		Debug.Log(obj["type"].ToString());
		eventHandlers[obj["type"].ToString()](json);
	}

    public void quitHandler(string data)
	{
		data = data.Replace("\"", "");
		Debug.Log("got quit " + data);
		players[data].Destroy();
		players[data] = null;
		players.Remove(data);
	}
    public void moveHandler(string data)
    {
		MoveEvent eventData = fastJSON.JSON.ToObject<MoveEvent>(data);

        players[eventData.username].moveTo(eventData.to, eventData.from);
	}

    public void loginHandler(string data)
    {
		data = data.Replace("\"", "");
        //fastJSON.JSON.ToObject<string>
        if (data.ToString() == "accepted")
        {
            JoinAreaRequestEvent r = new JoinAreaRequestEvent();
            r.areaId = "test";
            r.password = "";
            WriteQueue.Write(r);

        }
    }


    public void joinAreaHandler(string data)
    {
		JoinAreaEvent eventData = fastJSON.JSON.ToObject<JoinAreaEvent>(data);
        int playerCount = eventData.playersData.Count;
        for(int i = 0; i < playerCount; i++)
        {
            string playerName = eventData.playersData[i].username;

			GameObject go = null;
			if(playerName == localUsername)
			{
                go = GameObject.Instantiate(localPrefab) as GameObject;
            }
            else
			{
			    go = GameObject.Instantiate(remotePrefab) as GameObject;
			}
            Vector2JSON pos = eventData.playersData[i].position;
			Vector2 position = new Vector2(pos.x, pos.y);

			Player player = new Player(go, playerName, position);
		    go.GetComponent<PlayerMonoBehaviour>().Initialize(player); //This is a bit ugly, but needed if we don't have a common Player base(=super) class for both local and remote players.
			players[playerName] = player;
        }
    }

    private void createNewPlayer()
	{

	}

    public void infoHandler(object data)
    {
        Debug.Log(data);
    }

    public void registerHandler(object data)
    {

    }

    public void remotePlayerJoinHandler(string data)
    {
        RemotePlayerData playerData = fastJSON.JSON.ToObject<RemotePlayerData>(data);
		string playerName = playerData.username;

        if(players.ContainsKey(playerName))
		{
			players[playerName].Destroy();
			players[playerName] = null;
			players.Remove(playerName);
		}

        GameObject go = GameObject.Instantiate(remotePrefab) as GameObject;
		Vector2JSON pos = playerData.position;
        Vector2 position = new Vector2(pos.x, pos.y);

        Player player = new Player(go, playerName, position);
        go.GetComponent<PlayerMonoBehaviour>().Initialize(player); //This is a bit ugly, but needed if we don't have a common Player base(=super) class for both local and remote players.
        players[playerName] = player;
    }
}

