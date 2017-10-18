using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

	public InstructionTextController instructions; 
	public StreamVideo videoStreamer;

    // is filled in Unity-Editor
    public GameObject[] bottleSpots; 

	private Receipe currentReceipe; 
	private int currentStep = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startGame(Receipe receipe)
	{
		this.currentReceipe = receipe;

        // assign a bottle to highlight to an bottlespot
        int currentBottleSpot = 0; 
        foreach(Step s in receipe.steps)
        {
            foreach(string name in s.highlight)
            {
                if( receipe.bottles[name] == null )
                {
                    receipe.bottles.Add(name, new Bottle(name, bottleSpots[currentBottleSpot]));
                    currentBottleSpot++; 

                    if( currentBottleSpot >= bottleSpots.Length)
                    {
                        throw new System.Exception("In the receipt are mor bottles to highlight than bottle spots are available.");
                    }
                }
            }
        }
	}


    public void init()
    {
        if( currentReceipe == null )
        {
            throw new System.InvalidOperationException("No receipe is loaded. Please use 'startGame' method first.");
        }

        instructions.addTextLine("Please put the correct bottles to the corropsonding bottle spots."); 

        // init bottles
        // highlight all bottle spots to prepare the bar
        foreach(KeyValuePair<string, Bottle> entry in currentReceipe.bottles)
        {
            entry.Value.highlight(); 
        }
    }

    public void nextStep()
	{
		// check if next step
		// check, what bottles should be highlighted
		Step current = this.currentReceipe.getStep(currentStep); 
		currentStep++;

		if( videoStreamer != null ) 
		{
			//videoStreamer.setVideoClip (...);
		}

        foreach(string highlightBName in current.highlight)
        {
            Bottle b = currentReceipe.bottles[highlightBName];
            b.highlight(); 
        }
	}

	public void prevStep()
	{
        currentStep--;
        nextStep(); 
	}

    private void resetViews()
    {
        // turn off highlighted bottles
        // reset instruction table
        instructions.clearText();
        // reset video
    }
}
