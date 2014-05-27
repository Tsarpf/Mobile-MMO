using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class JSONEvent
{
    public string eventtype
    {
        get
        {
            return this.GetType().Name.Substring(0, this.GetType().Name.Length - "Event".Length).ToLower();
        }
        set
        { }
    }
}

public class RegisterEvent : JSONEvent
{
    //public static readonly string EventType = "Register";
    public string username;
    public string password;
    public string email;
}

public class LoginEvent : JSONEvent
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
	public Vector2JSON to;
}


public class Vector2JSON
{
	public float x;
	public float y;
    public Vector2JSON(Vector2 vector)
	{
		x = vector.x;
		y = vector.y;
	}
    public Vector2JSON(float x, float y)
	{
		this.x = x;
		this.y = y;
	}
}