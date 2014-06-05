using UnityEngine;
using System.Collections;

public class SpeechBubble : MonoBehaviour
{
	private Transform goTransform;
	private Vector3 goScreenPos;
	private Vector3 goViewportPos;

	public int bubbleWidth = 200;
	public int bubbleHeight = 100;

	//an offset, to better position the bubble
	public float offsetX = 0;
	public float offsetY = 150;

	//an offset to center the bubble
	private int centerOffsetX;
	private int centerOffsetY;

	//a material to render the triangular part of the speech balloon
	public Material mat;
	//a guiSkin, to render the round part of the speech balloon
	public GUISkin guiSkin;

	public string message;
	void Awake()
	{
		goTransform = this.GetComponent<Transform>();

        //set triangle color to white, but make it transparent
	}

	void Start()
	{
		if (!mat)
		{
			Debug.LogError("Please assign a material on the Inspector.");
			return;
		}

		if (!guiSkin)
		{
			Debug.LogError("Please assign a GUI Skin on the Inspector.");
			return;
		}

		centerOffsetX = bubbleWidth / 2;
		centerOffsetY = bubbleHeight / 2;
	}

	void LateUpdate()
	{
		//find out the position on the screen of this game object
		goScreenPos = Camera.main.WorldToScreenPoint(goTransform.position);
		goViewportPos = Camera.main.WorldToViewportPoint(goTransform.position);
	}

	//Draw GUIs
	void OnGUI()
	{
		GUI.BeginGroup(new Rect(goScreenPos.x - centerOffsetX - offsetX, Screen.height - goScreenPos.y - centerOffsetY - offsetY, bubbleWidth, bubbleHeight));
		//Render the round part of the bubble
		GUI.Label(new Rect(0, 0, 200, 100), "", guiSkin.customStyles[0]);
		//Render the text
		GUI.Label(new Rect(10, 25, 190, 50), message, guiSkin.label);
		GUI.EndGroup();
	}

	//Called after camera has finished rendering the scene
	void OnRenderObject()
	{
		//push current matrix into the matrix stack
		GL.PushMatrix();
		//set material pass
		mat.SetPass(0);
		//load orthogonal projection matrix (ie. disable 3d)
		GL.LoadOrtho();
		//a triangle primitive is going to be rendered
		GL.Begin(GL.TRIANGLES);

		//Define the triangle vetices
		GL.Vertex3(goViewportPos.x, goViewportPos.y + (offsetY / 3) / Screen.height, 0.1f);
		GL.Vertex3(goViewportPos.x - (bubbleWidth / 3) / (float)Screen.width, goViewportPos.y + offsetY / Screen.height, 0.1f);
		GL.Vertex3(goViewportPos.x - (bubbleWidth / 8) / (float)Screen.width, goViewportPos.y + offsetY / Screen.height, 0.1f);

		GL.End();
		//pop the orthogonal matrix from the stack
		GL.PopMatrix();
	}
}