using System;
using System.Collections.Generic;
using UnityEngine;

public class Bottle
{
	public string nameId; 
	public GameObject position; 

	public Bottle (string name, GameObject position)
	{
        this.nameId = name; 
        this.position = position; 
	}

    public void highlight()
    {
        // implement highlight of gameobject
    }
}

