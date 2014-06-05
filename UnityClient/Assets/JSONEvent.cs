using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class JSONEvent
{
    public string eventType
    {
        get
        {
            string eventName = this.GetType().Name.Substring(0, this.GetType().Name.Length - "Event".Length);
            eventName = eventName[0].ToString().ToLower().ToCharArray()[0] + eventName.Substring(1);
			return eventName;
        }
        set
        { }
    }
}


public class JoinAreaRequestEvent : JSONEvent
{
	public string areaId;
	public string password;
}

public class JoinAreaEvent : JSONEvent
{
	public Vector2JSON startingPosition;
	public Vector2JSON dimensions;
	public List<RemotePlayerData> playersData;
}


public class RemotePlayerData
{
	public string username;
	public Vector2JSON position; 
}

public class AreaChatRequestEvent : JSONEvent
{
	public string message;
}

public class AreaChatEvent : JSONEvent
{
	public string username;
	public string message;
}
public class RegisterRequestEvent : JSONEvent
{
    //public static readonly string EventType = "Register";
    public string username;
    public string password;
    public string email;
}

public class LoginRequestEvent : JSONEvent
{
    public string username;
    public string password;
}

public class MoveRequestEvent : JSONEvent
{
	public Vector2JSON to;
}

public class MoveEvent : JSONEvent
{
    public string username;
	public Vector2JSON from;
	public Vector2JSON[] to;
}


public class Vector2JSON
{
	public float x;
	public float y;
}