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

	private string chatString = "Click here or press Enter to focus here. After typing press enter to chat to others in the area";
	private bool typing = false;
    void OnGUI()
	{
        //Remember the event stuff (next 3 lines) must be handled before drawing text fields etc. otherwise they'll eat up the events
		Event e = Event.current;
		typing = GUI.GetNameOfFocusedControl() == "chat";
		bool hitEnterThisFrame = e.keyCode == KeyCode.Return && Event.current.type == EventType.keyDown;




		GUI.SetNextControlName("chat");
		chatString = GUI.TextField(new Rect(10, Screen.height - 40, 500, 30), chatString, 100);



        if(!typing && hitEnterThisFrame)
		{
			//Debug.Log("perkele");
			GUI.FocusControl("chat");
			typing = true;
			return;
		}
		//if (e.keyCode == KeyCode.Return && Event.current.type == EventType.keyDown && GUI.GetNameOfFocusedControl() == "chat")
        if(typing && hitEnterThisFrame)
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
	}
}
    