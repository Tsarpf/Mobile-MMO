using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Cell : MonoBehaviour
{

	private GameObject go;
	private Vector3 position;
	private GameObject player;
	//private List<Material> materials; 
	private Material[] materials;
    public void Initialize(Material[] materials, Vector3 position)
	{
		//Debug.Log("initialized cell");
		go = GameObject.CreatePrimitive(PrimitiveType.Cube); 
		this.materials = materials;
		this.position = position;

		go.transform.position = this.position;
		go.renderer.materials = this.materials;
		go.collider.isTrigger = true;
	}
    void Start()
	{
		//Debug.Log("Start()ed cell");
    }
    void Update()
	{
        
    }
    void OnMouseDown()
	{
		//Debug.Logaöslkjfgdklrjtwqlörak§j
		//Debug.Log(transform.position);
		player.GetComponent<PlayerMove>().MoveTo(transform.position);
	}
	// Use this for initialization

}
