using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Cell : MonoBehaviour
{

	//static Material grid = Resources.Load("Materials/grid") as Material;
	private GameObject go;
	private Vector3 position;
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
	}
    void Start()
	{
		//Debug.Log("Start()ed cell");
    }
    void Update()
	{
        
    }
	// Use this for initialization

}
