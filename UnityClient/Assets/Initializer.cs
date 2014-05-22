using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Initializer : MonoBehaviour {

	public int SpaceDimensionX;
	public int SpaceDimensionY;
	Floor floor;
	Dictionary<string, RunEvent> events;
	// Use this for initialization
	void Start () {
		SpaceDimensionX = 30; SpaceDimensionY = 20;
		
        Material grid = Resources.Load("Materials/grid") as Material;
        Material floorMat = Resources.Load("Materials/grayfloor") as Material;

		floor = new Floor(floorMat, grid, SpaceDimensionX, SpaceDimensionY);


        //Construct data handling stuff
		events = new Dictionary<string, RunEvent>();
		events["move"] = MoveEventHandler;
	}

    public void MoveEventHandler(object data)
	{
        //Do something with move data
	}

	// Update is called once per frame
	void Update ()
	{
        string json;
        while((json = ReadQueue.Read()) != null)
	    {
			jokuHandleHomma(json);
        }
	}

	public delegate void RunEvent(object someData);
    void jokuHandleHomma(string json)
	{
        //Convert json to object n' stuff
		object placeHolder = new object();
		string eventType = "move"; //placeholder, should be determined from json

		events[eventType](placeHolder);

	}
}
