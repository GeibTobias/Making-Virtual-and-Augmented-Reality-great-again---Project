using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum State
{
	CocktailSelection = 0,
	BarInitialization = 1,
	RecipeExecution = 2,
	RecipeFinished = 3
}

public class StatusChangedEventArgs : EventArgs
{
	public State NewState { get; set; }
}

public class StateManager
{
	private State state;

	public State State {
		get { return this.state; }
		set {
			this.state = value;
			StatusChangedEventArgs args = new StatusChangedEventArgs ();
			args.NewState = state;
			OnStatusChanged (this, args);
		}
	}

	public event EventHandler<StatusChangedEventArgs> StatusChanged;

	public Recipe currentRecipe = null;
	public Step currentStep = null;
    public int currentStepCount = 0; 

	protected virtual void OnStatusChanged (object sender, StatusChangedEventArgs e)
	{
		if (StatusChanged != null)
			StatusChanged (this, e);
	}


    public Boolean hasNextStep()
    {
        if( currentRecipe == null)
        {
            return false; 
        }

        if( currentStepCount < currentRecipe.steps.Count - 1)
        {
            return true; 
        }
        else
        {
            return false; 
        }
    }


	public Step next ()
	{
        if( currentRecipe == null )
        {
            throw new Exception("No Recipe loaded into StateManager"); 
        }

        // init with first step
        if ( currentStep == null )
        {
            currentStepCount = 0;
            currentStep = currentRecipe.steps[0];   
        } 
        else
        {
            currentStepCount++;
            currentStep = currentRecipe.steps[currentStepCount]; 
        }

        return currentStep;
	}


    public Step previous()
    {
        if (currentRecipe == null)
        {
            throw new Exception("No Recipe loaded into StateManager");
        }

        if (currentStep == null || currentStepCount - 1 < 0)
        {
            currentStep = currentRecipe.steps[0];
        }
        else
        {
            currentStepCount--;
            currentStep = currentRecipe.steps[currentStepCount];
        }

        return currentStep; 
    }


    public void resetSteps()
    {
        currentStep = null;
        currentStepCount = 0; 
    }
}
