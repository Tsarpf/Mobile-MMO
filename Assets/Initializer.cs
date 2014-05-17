using UnityEngine;
using System.Collections;

public class Initializer : MonoBehaviour {

	public int SpaceDimensionX;
	public int SpaceDimensionY;
	GameObject[][] gameObjects;
	// Use this for initialization
	void Start () {
		SpaceDimensionX = 30;
		SpaceDimensionY = 20;

		
        Material grid = Resources.Load("Materials/grid") as Material;
        Material[] floorMats = new Material[2];
		floorMats[0] = grid;
		floorMats[1] = Resources.Load("Materials/grayfloor") as Material;

        gameObjects = new GameObject[SpaceDimensionX][];
        for(int x = 0; x < SpaceDimensionX; x++)
		{
            gameObjects[x] = new GameObject[SpaceDimensionY];
            for(int y = 0; y < SpaceDimensionY; y++)
			{
				//gameObjects[x][y] = new GameObject("(" + x + ", " + y + ")");
				gameObjects[x][y] = GameObject.CreatePrimitive(PrimitiveType.Cube);
				gameObjects[x][y].transform.position = new Vector3(x,0,y);
				gameObjects[x][y].renderer.materials = floorMats;

			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
