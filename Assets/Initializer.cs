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
		

		client = new Client ("http://datisbox.net:3001");
		client.Opened += SocketOpened;

		client.Connect ();

        Material grid = Resources.Load("Materials/grid") as Material;
        Material floorMat = Resources.Load("Materials/grayfloor") as Material;

		floor = new Floor(floorMat, grid, SpaceDimensionX, SpaceDimensionY);

	}

	void SocketOpened(object sender, EventArgs e)
	{
		client.Send ("Derp");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
