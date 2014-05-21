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
	private bool hilighted;
	bool isEmpty;
    public void Initialize(Material[] materials, Vector3 position, GameObject go)
	{
		//Debug.Log("initialized cell");
		//
		//go = gameObject;
		//go = GameObject.CreatePrimitive(PrimitiveType.Cube); 

		//Debug.Log ("initi");
		this.go = go;
		this.materials = materials;
		this.position = position;
		this.hilighted = false;
		go.transform.position = this.position;
		go.renderer.materials = this.materials;
		go.collider.isTrigger = true;
		isEmpty = true;
	}
	public Vector3 GetCoordinates()
	{
		return position;
	}
	public bool IsEmpty()
	{
		return isEmpty;
	}
	public bool isHilighted()
	{
		return hilighted;
	}	
    void Start()
	{

		//Debug.Log ("start");

		//Debug.Log("Start()ed cell");
    }
    void Update()
	{
        
    }

	public void hilight()
	{
		if(hilighted)
		{
			hilighted = false;
			go.renderer.materials[0].color = Color.black;
			go.renderer.materials[1].color = Color.gray;
		} else 
		{
			hilighted = true;
			go.renderer.materials[0].color = Color.white;
			go.renderer.materials[1].color = Color.green;
		}
	}
    
	// Use this for initialization

}
