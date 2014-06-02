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
		string json = fastJSON.JSON.ToJSON(obj["properties"]);
		Debug.Log(json);
			//obj["properties"]
		eventHandlers[obj["type"].ToString()](json);
        
        
		Debug.Log(obj["type"]);

		//Debug.Log(obj);
        //dicshunary[obj["type"]](dem, argumentit);
		//JObject o = JObject.Parse(derp);
		//Debug.Log(o.Property("eventType"));
		//Debug.Log(schema.Properties["eventType"]);

		//string  test = JsonConvert.DeserializeObject(derp) as eventType;

	}
    public void moveHandler(string data)
    {
		MoveEvent eventData = fastJSON.JSON.ToObject<MoveEvent>(data);

		Debug.Log(data);
        players[eventData.user].moveTo(eventData.to);

        /*
        Dictionary<string, object> movedata = data as Dictionary<string, object>;
        Debug.Log("sinep" + movedata.ToString());
        foreach(KeyValuePair<string, object> kvp in movedata)
        {
           Debug.Log("key: " + kvp.Key + " value: " + kvp.Value);
        }
        string username = movedata["user"].ToString();
        Dictionary<string, object> from = movedata["from"] as Dictionary<string, object>;
        Vector2 fromVector = new Vector2(int.Parse(from["x"].ToString()), int.Parse(from["y"].ToString()));
        Debug.Log("from: " + fromVector.x);
        List<object> to = movedata["to"] as List<object>;
        List<Vector2> toVectors = new List<Vector2>();

        foreach (var pair in to)
        {
            Dictionary<string, object> sinep = pair as Dictionary<string, object>;
            toVectors.Add(new Vector2(int.Parse(sinep["x"].ToString()), int.Parse(sinep["y"].ToString())));
        }

		Debug.Log(toVectors[0]);

        players[username].moveTo(toVectors[0]);
        */

	}

    public void loginHandler(string data)
    {
		data = data.Replace("\"", "");
        //fastJSON.JSON.ToObject<string>
		Debug.Log("sup: " + data);
        if (data.ToString() == "accepted")
        {
			Debug.Log("hei vain");
            JoinAreaRequestEvent r = new JoinAreaRequestEvent();
            r.areaId = "test";
            r.password = "";
            WriteQueue.Write(r);

        }
    }


    public void joinAreaHandler(string data)
    {
		JoinAreaEvent eventData = fastJSON.JSON.ToObject<JoinAreaEvent>(data);
		//Debug.Log("joinareaeventdata: ");
		//Debug.Log(eventData);

		////Create local player
		//GameObject local = GameObject.Instantiate(localPrefab) as GameObject;
		//string playerName = localUsername;
		//Player player = new Player(local, playerName, new Vector2(0, 0));

		////This is a bit ugly, but needed if we don't have a common Player base(=super) class for both local and remote players.
        
		//local.GetComponent<PlayerMonoBehaviour>().Initialize(player); 
		//players[playerName] = player;

        //Get player count from JSON
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

            //Get the remote player name and position from JSON
			//string remotePlayerName = eventData.playersData[i].username;
            Vector2JSON pos = eventData.playersData[i].position;
			Vector2 position = new Vector2(pos.x, pos.y);

			Player player = new Player(go, playerName, position);
		    go.GetComponent<PlayerMonoBehaviour>().Initialize(player); //This is a bit ugly, but needed if we don't have a common Player base(=super) class for both local and remote players.
			players[playerName] = player;
        }
		/*
	{ type: 'joinAreaEvent',
properties:
{ startingPosition: { x: 0, y: 0 },
dimensions: { x: 30, y: 20 },
playersData: [ [Object] ] } }

*/
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

  

    
	

}

