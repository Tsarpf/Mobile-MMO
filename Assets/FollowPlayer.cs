using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	Transform player;
	void Start () {
		player = gameObject.transform.parent.transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(player);	
	}
}
