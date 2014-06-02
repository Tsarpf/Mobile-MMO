using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player
{
    //GameObject prefab = (GameObject)Resources.Load("RemotePlayer", typeof(GameObject));
    GameObject playerObj;
    Vector2 targetPosition;
    Queue<Vector2JSON> route;
    string username;
    //PlayerMonoBehaviour 
    public Player(GameObject prefab, string username, Vector2 position)
    {
        this.username = username;
		playerObj = prefab;
        playerObj.transform.position = new Vector3(position.x, 0, position.y);
        route = new Queue<Vector2JSON>();
		targetPosition = new Vector2(position.x, position.y);
    }

    public void Update()
    {

        Vector2 pos = new Vector2(playerObj.transform.position.x, playerObj.transform.position.z);
        if (V2Equal(pos, targetPosition))
        {
            playerObj.transform.rigidbody.velocity = new Vector3(0, 0, 0);
            if (route.Count > 0)
            {
                Vector2JSON next = route.Dequeue();
                if (next != null)
                {
                    move(next);
                }
            }
            
        }
    }

    public void Start()
    {
        //ses
    }


    public void Destroy()
	{
		GameObject.Destroy(playerObj);
	}


    public void moveTo(Vector2JSON[] route, Vector2JSON from)
    {
        playerObj.transform.position = new Vector3(from.x, 0, from.y);
        this.route = new Queue<Vector2JSON>();
        foreach (var sinep in route)
        {
            this.route.Enqueue(sinep);
        }
        

    }

    void move(Vector2JSON nexttarget)
    {
        targetPosition = new Vector2(nexttarget.x, nexttarget.y);
        Vector2 pos = new Vector2(playerObj.transform.position.x, playerObj.transform.position.z);
        if (!V2Equal(targetPosition, pos))
        {
            Vector3 direction = new Vector3(targetPosition.x, 0, targetPosition.y) - new Vector3(pos.x, 0, pos.y);
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