using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

public class PlayerMove : MonoBehaviour {

	Vector3 position;
	Vector3 target;
	Cell lastTouched;
	/*
    public void MoveTo(Vector3 target)
	{
		this.target = target; 
	}
	*/
	// Use this for initialization
	void Start () {
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
			Debug.Log ("mouse down");
 

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
                request.to = new Vector2JSON(target.x, target.z);
                WriteQueue.Write(request);
                Debug.Log("written: " + request);
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
                moveTo(new Vector2(target.x, target.z)); //for testing
            }
            
		}   

		

	}

    public void moveTo(Vector2 targetpos)
    {
        Vector3 tgt = new Vector3(targetpos.x, 0, targetpos.y);
        if (!V3Equal(tgt, position))
        {
            Vector3 direction = tgt - position;
            direction = new Vector3(direction.normalized.x, 0, direction.normalized.z);
            transform.rigidbody.velocity = direction * 3;
        }

    }


	bool V3Equal(Vector3 a, Vector3 b){
		a = new Vector3 (a.x, 0, a.z);
		b = new Vector3 (b.x, 0, b.z);
		return Vector3.SqrMagnitude(a - b) < 0.001;
	}

}
