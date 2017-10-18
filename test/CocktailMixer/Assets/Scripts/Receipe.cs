using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receipe {

	public string name; 
	public List<string> keywords; 
	public List<Step> steps;

    public Dictionary<string, Bottle> bottles;

    // picture is left here

    public Receipe()
    {
        bottles = new Dictionary<string, Bottle>(); 
	}

	public Receipe(List<string> keywords) {
		this.keywords = keywords; 
	}

	public void addStep(Step step)
	{
		if (this.steps == null) {
			this.steps = new List<Step>(); 
		}

		this.steps.Add (step); 
	}

	public Step getStep(int number)
	{
		return null;
	}
}
