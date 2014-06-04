using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

public class LocalPlayerMonoBehaviour : MonoBehaviour 
{
	Vector3 position;
	Vector3 target;
	Cell lastTouched;
	// Use this for initialization
	void Start ()
	{
		position = transform.position;
		target = position;

		Camera.main.orthographic = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		position = transform.position;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;		
		if (Physics.Raycast (ray, out hit)) {
			Cell hitO = hit.collider.gameObject.GetComponent<Cell>();
			if (hitO != null && !hitO.isHilighted()) {
				hitO.hilight();
				if (lastTouched != null) {
					lastTouched.hilight();
				}
				lastTouched = hitO;
			}
		}

		if (Input.GetMouseButtonDown(0))
		{
			//Camera des = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
			//des.ortho
			//Camera.main.orthographic = true;
            if (Physics.Raycast(ray, out hit))
            {

                //hit.collider.gameObject
                //Debug.Log ("ses: " + hit.collider.gameObject.name);

                Cell hitObject = hit.collider.gameObject.GetComponent<Cell>();
                if (hitObject == null)
                    return;
                if (!hitObject.IsEmpty())
                    return;

                Vector3 targetPos = hitObject.GetCoordinates();

                //hit.transform.SendMessage("Selected");

                if (V3Equal(position, targetPos))
                {
                    return;
                }

                //if(target.x > 
                target = targetPos;
                //transform.rigidbody.velocity = new Vector3(-direction.x, 0, -direction.z);
                //player.GetComponent<PlayerMove>().MoveTo(transform.position);
                MoveRequestEvent request = new MoveRequestEvent();
				//request.to = new Vector2JSON(target.x, target.z);
                request.to = new Vector2JSON
				{
					x = target.x,
					y = target.z
				};

                WriteQueue.Write(request);
                //var test = WriteQueue.Read();
                //Debug.Log("test " + test);
                /*
                var incomingData = ReadQueue.Read();
                if (incomingData != null)
                {
                    Debug.Log(incomingData);
                    JSONEvent readEvent = JsonConvert.DeserializeObject<JSONEvent>(incomingData);
                    Debug.Log(readEvent);
                }
                */
                //moveTo(new Vector2(target.x, target.z)); //for testing
            }
            
        }



    }
		   

    bool V3Equal(Vector3 a, Vector3 b)
    {
        a = new Vector3(a.x, 0, a.z);
        b = new Vector3(b.x, 0, b.z);
        return Vector3.SqrMagnitude(a - b) < 0.001;
    }

	private string chatString = "Oh hi";
    void OnGUI()
	{
		Event e = Event.current;
        if (e.keyCode == KeyCode.Return && Event.current.type == EventType.keyDown)
		{
			if (chatString.Trim() != "")
			{
				AreaChatRequestEvent r = new AreaChatRequestEvent
                {
                    message = chatString.Trim()
                };
				WriteQueue.Write(r);

				chatString = "";
			}
		}
		chatString = GUI.TextField(new Rect(10, Screen.height - 30, 200, 20), chatString, 25);
	}
}
    