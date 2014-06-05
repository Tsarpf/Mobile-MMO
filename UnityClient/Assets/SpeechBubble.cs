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

	private string message;
    public void ShowMessageBubble(string message)
	{
		this.message = message;
		mat.color = Color.red;
		StartCoroutine("BubbleFade", 0.02f);

	}

    IEnumerator BubbleFade(float alphaDecreasePerFrame)
	{
		while (mat.color.a > 0)
		{
			//Debug.Log(mat.color);
			Color color = mat.color;
			color.a -= alphaDecreasePerFrame;
			mat.color = color;
			yield return null;
		}
	}

	void Awake()
	{
		goTransform = this.GetComponent<Transform>();
		mat.color = Color.red;
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

		Debug.Log("passcount: " + mat.passCount);
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
		GL.PushMatrix();
		GL.LoadOrtho();

		//for (int i = 0; i < mat.passCount; i++)
		//for (int i = 0; i < 1; i++)
		//{
			mat.SetPass(0);
			//mat.SetPass(i);
			GL.Begin(GL.TRIANGLES);
            GL.Color(mat.color);
			Debug.Log(mat.color);


			//Define the triangle vetices
			GL.Vertex3(goViewportPos.x, goViewportPos.y + (offsetY / 3) / Screen.height, 0.1f);
			GL.Vertex3(goViewportPos.x - (bubbleWidth / 3) / (float)Screen.width, goViewportPos.y + offsetY / Screen.height, 0.1f);
			GL.Vertex3(goViewportPos.x - (bubbleWidth / 8) / (float)Screen.width, goViewportPos.y + offsetY / Screen.height, 0.1f);

			GL.End();
		//}
		//pop the orthogonal matrix from the stack
		GL.PopMatrix();
	}
}