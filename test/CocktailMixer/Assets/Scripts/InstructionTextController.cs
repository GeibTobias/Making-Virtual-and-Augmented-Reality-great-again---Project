using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class InstructionTextController : MonoBehaviour {

	public Text textField; 

	void Start () {

		clearText (); 

		addTextLine ("Here will be some instructions"); 
		addText ("Instruction 1"); 
		addText (" -- and more..."); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addTextLine(string text)
	{
		this.textField.text += text + "\n"; 
	}

	public void addText(string text)
	{
		this.textField.text += text; 
	}
		
	public void clearText()
	{
		this.textField.text = ""; 
	}
}
