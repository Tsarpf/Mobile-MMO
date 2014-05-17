using UnityEngine;
using System.Collections;

public class Initializer : MonoBehaviour {

	public int SpaceDimensionX;
	public int SpaceDimensionY;
	Floor floor;
	// Use this for initialization
	void Start () {
		//SpaceDimensionX = 30; SpaceDimensionY = 20;

		
        Material grid = Resources.Load("Materials/grid") as Material;
        Material floorMat = Resources.Load("Materials/grayfloor") as Material;

		floor = new Floor(floorMat, grid, SpaceDimensionX, SpaceDimensionY);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
