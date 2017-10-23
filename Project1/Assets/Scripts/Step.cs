using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StepID
{
	cut,
	crush,
	shake,
	muddle,
	mix,
	add,
	shakeGently,
	pour,
	moveA,
	moveB
}

[Serializable]
public class Step {
	public StepID id;
	public List<string> highlightedObjects;
	public List<string> instructions;
	public bool	isWork;
	public string video;
}
