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
    public delegate void eventHandlerDes(object data);
    Dictionary<string, Player> players = new Dictionary<string, Player>();
//    static readonly byte[] hardCodedServerCertificateHash = { 0xf1, 0x40, 0x8a, 0xd8, 0xb5, 0x1a, 0x42, 0xdb, 0x50, 0x44, 0x04, 0xa1, 0xa8, 0x92, 0xa8, 0xa8, 0x77, 0x41, 0x31, 0x2d };
	// Use this for initialization
	GameObject localPrefab;
	GameObject remotePrefab;

	string localUsername = "sinep";

	void Start () {

        LoginRequestEvent levent = new LoginRequestEvent();
        levent.username = localUsername;
        levent.password = "olut";
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
        

	}
	
	// Update is called once per frame
    
    
	void Update () {
        //Get from readqueue
        var read  = ReadQueue.Read();
        if (read == null)
        {
			return;
        }
        Debug.Log(read);
		Dictionary<string, object> obj = fastJSON.JSON.Parse(read) as Dictionary<string, object>;
        //foreach(KeyValuePair<string, object> kvp in obj)
        //{
        //   Debug.Log("key: " + kvp.Key + " value: " + kvp.Value);
        //}
        eventHandlers[obj["type"].ToString()](obj["properties"]);
        
        
		Debug.Log(obj["type"]);

		//Debug.Log(obj);
        //dicshunary[obj["type"]](dem, argumentit);
		//JObject o = JObject.Parse(derp);
		//Debug.Log(o.Property("eventType"));
		//Debug.Log(schema.Properties["eventType"]);

		//string  test = JsonConvert.DeserializeObject(derp) as eventType;

	}
    public void moveHandler(object data)
    {
        MoveEvent move = parseMoveEvent(data);

        players[move.user].moveTo(move.to);
     

    }

    public void loginHandler(object data)
    {
        if (data.ToString() == "accepted")
        {
            JoinAreaRequestEvent r = new JoinAreaRequestEvent();
            r.areaId = "test";
            r.password = "";
            WriteQueue.Write(r);

        }
    }


    public void joinAreaHandler(object data)
    {
        //Create local player
		GameObject local = GameObject.Instantiate(localPrefab) as GameObject;
        string playerName = localUsername;
        Player player = new Player(local, playerName, new Vector2(0, 0));

        //This is a bit ugly, but needed if we don't have a common Player base(=super) class for both local and remote players.
        
		local.GetComponent<PlayerMonoBehaviour>().Initialize(player); 
        players[playerName] = player;

        //Get player count from JSON
        int playerCount = 1;
        for(int i = 0; i < playerCount; i++)
        {
			GameObject remote = GameObject.Instantiate(remotePrefab) as GameObject;

            //Get the remote player name and position from JSON
			string remotePlayerName = "jotain";
			Vector2 position = new Vector2(0, 0);

			Player remotePlayer = new Player(remote, remotePlayerName, position);
		    remote.GetComponent<PlayerMonoBehaviour>().Initialize(remotePlayer); //This is a bit ugly, but needed if we don't have a common Player base(=super) class for both local and remote players.
			players[remotePlayerName] = remotePlayer;
        }
    }

    public void infoHandler(object data)
    {
        Debug.Log(data);
    }

    public void registerHandler(object data)
    {

    }

    public void remotePlayerJoinHandler(object data)
    {

    }

    class eventType 
	{
		string type = "";
    }

    MoveEvent parseMoveEvent(object data)
    {
        MoveEvent move = new MoveEvent();

        Dictionary<string, object> movedata = data as Dictionary<string, object>;
        
        move.user = movedata["user"].ToString();

        Dictionary<string, object> from = movedata["from"] as Dictionary<string, object>;

        Vector2 fromVector = new Vector2(int.Parse(from["x"].ToString()), int.Parse(from["y"].ToString()));
        move.from = new Vector2JSON(fromVector);
        Vector2JSON[] toVectors = new Vector2JSON[10];
        List<object> to = movedata["to"] as List<object>;

        int i = 0;
        foreach (var pair in to)
        {
            Dictionary<string, object> sinep = pair as Dictionary<string, object>;
            toVectors[i] = new Vector2JSON((new Vector2(int.Parse(sinep["x"].ToString()), int.Parse(sinep["y"].ToString()))));
            i++;
        }
        move.to = toVectors;

        Debug.Log(toVectors[0]);

        return move;
    }

    
	

}

