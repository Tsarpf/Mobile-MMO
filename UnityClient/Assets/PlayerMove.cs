using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	Vector3 position;
	Vector3 target;
    public void MoveTo(Vector3 target)
	{
		this.target = target; 
	}
	// Use this for initialization
	void Start () {
		position = transform.position;
		target = position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		position = transform.position;

        if(position == target)
		{
			return;
		}

		var direction = target - position;
		transform.rigidbody.velocity = direction;

	}
}
