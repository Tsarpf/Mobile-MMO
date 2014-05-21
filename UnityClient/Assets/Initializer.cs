using UnityEngine;
using System.Collections;
using SocketIOClient; 
using System;

public class Initializer : MonoBehaviour {

	public int SpaceDimensionX;
	public int SpaceDimensionY;
	Client client;
	Floor floor;
	// Use this for initialization
	void Start () {
		//SpaceDimensionX = 30; SpaceDimensionY = 20;
		

		client = new Client ("http://datisbox.net:3002");
		client.Opened += SocketOpened;
		client.Message += SocketMessage;

		client.Connect ();
		//client.Send("Derp");

        Material grid = Resources.Load("Materials/grid") as Material;
        Material floorMat = Resources.Load("Materials/grayfloor") as Material;

		floor = new Floor(floorMat, grid, SpaceDimensionX, SpaceDimensionY);

	}

	private void SocketOpened(object sender, EventArgs e)
	{
		Debug.Log("socket opened");
        client.Emit("register", "fcuk you");
		client.Send ("I was opened lul");
	}
    private void SocketMessage(object sender, MessageEventArgs e)
	{
		Debug.Log("socket message" + e);
        client.Emit("register", "fcuk you");
		client.Send ("messagin'");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
