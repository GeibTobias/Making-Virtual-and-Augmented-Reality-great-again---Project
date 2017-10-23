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

public class StateManager {

    private State state;
	public State State {
		get { return this.state; }
		set {
			this.state = value;
			StatusChangedEventArgs args = new StatusChangedEventArgs();
			args.NewState = state;
			OnStatusChanged(this, args);
		}
	}
	public event EventHandler<StatusChangedEventArgs> StatusChanged;
	public Recipe currentRecipe = null;
	public Step currentStep = null;

	protected virtual void OnStatusChanged(object sender, StatusChangedEventArgs e)
	{
		if (StatusChanged != null)
			StatusChanged(this, e);
	}

	public void next() {
		Debug.Log ("next()");
	}
}
