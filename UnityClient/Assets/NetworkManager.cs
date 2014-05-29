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

public class NetworkManager : MonoBehaviour
{

//    static readonly byte[] hardCodedServerCertificateHash = { 0xf1, 0x40, 0x8a, 0xd8, 0xb5, 0x1a, 0x42, 0xdb, 0x50, 0x44, 0x04, 0xa1, 0xa8, 0x92, 0xa8, 0xa8, 0x77, 0x41, 0x31, 0x2d };
	// Use this for initialization

	void Start () {
		//NetworkLoop network = new NetworkLoop();
		//Thread oThread = new Thread(new ThreadStart(network.RunNetworkLoop));
		//oThread.Start();
	}
	
	// Update is called once per frame
	void Update () {
        //Get from readqueue
        var derp = ReadQueue.Read();
        if (derp == null)
        {
			return;
        }

		var obj = fastJSON.JSON.Parse(derp) as Dictionary<string, object>;

		Debug.Log(obj["type"]);
		//Debug.Log(obj);

		//JObject o = JObject.Parse(derp);
		//Debug.Log(o.Property("eventType"));
		//Debug.Log(schema.Properties["eventType"]);

        Debug.Log(derp);
		//string  test = JsonConvert.DeserializeObject(derp) as eventType;

	}
    class eventType 
	{
		string type = "";
    }


    
	

}

