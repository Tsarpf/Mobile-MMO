using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player
{
    //GameObject prefab = (GameObject)Resources.Load("RemotePlayer", typeof(GameObject));
	GameObject prefab;
    GameObject playerObj;
    Vector2 targetPosition;
    string username;
    //PlayerMonoBehaviour 
    public Player(GameObject prefab, string username, Vector2 position)
    {
        this.username = username;
		playerObj = prefab;
        playerObj.transform.position = new Vector3(position.x, 0, position.y);
    }

    public void Update()
    {

        Vector2 pos = new Vector2(playerObj.transform.position.x, playerObj.transform.position.y);
        if (V2Equal(pos, targetPosition))
        {
            playerObj.transform.rigidbody.velocity = new Vector3(0, 0, 0);
        }
        

    }

    public void Start()
    {
        //ses
    }

    public void moveTo(Vector2 targetpos)
    {
        targetPosition = targetpos;
        Vector2 pos = new Vector2(playerObj.transform.position.x, playerObj.transform.position.y);
        if (!V2Equal(targetpos, pos))
        {
            Vector3 direction = targetPosition - pos;
            direction = new Vector3(direction.normalized.x, 0, direction.normalized.z);
            playerObj.transform.rigidbody.velocity = direction * 3;
        }

    }

    bool V2Equal(Vector2 a, Vector2 b)
    {
        //a = new Vector3(a.x, a.y, 0);
        //b = new Vector3(b.x, b.y, 0);
        return Vector2.SqrMagnitude(a - b) < 0.001; 
    }

}