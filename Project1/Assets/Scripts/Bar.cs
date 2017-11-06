using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Bar : MonoBehaviour
{

	public RecipeCollection collection;
	public GameObject instructionsText;
	public GameObject videoArea;
	public GameObject next;
	public GameObject previous;
	public GameObject exit;
	public GameObject redo;
	public GameObject done;
	public GameObject cocktailImage;
	public GameObject cocktailSelectionGO;
	public List<GameObject> cocktailSelectionCocktails;
	private BarInitializer initializer;
	private StateManager stateManager;
	private KeywordDetector keywordDetector;
	private KeyboardInput keyboardInput;

	private bool keyboardInputEnabled = true;

	// Use this for initialization
	void Start ()
	{
		stateManager = new StateManager ();
		stateManager.StatusChanged += sm_OnStatusChanged;

		keyboardInput = new KeyboardInput ();
		keyboardInput.CommandDetected += kd_OnCommandDetected;

		keywordDetector = this.GetComponent<KeywordDetector> ();
		keywordDetector.CommandDetected += kd_OnCommandDetected;

		collection = LoadRecipes ();

		initializer = new BarInitializer ();

		setupUI ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (keyboardInputEnabled && keyboardInput != null) {
			keyboardInput.isKeyPressed ();
		}
	}

	private void setupUI ()
	{
		videoArea.GetComponent<VideoPlayer> ().clip = null;

		cocktailSelectionGO.SetActive(stateManager.State == State.CocktailSelection);
		instructionsText.SetActive (stateManager.State != State.CocktailSelection);

		switch (stateManager.State) {
		case State.CocktailSelection:
			//instructionsText.GetComponents<TextMesh> () [0].text = "Say a Cocktail number:\n";
			for (int i = 0; i < collection.recipes.Count; i++) {
				string line = (i + 1) + ") " + collection.recipes [i].id + "\n";
				//instructionsText.GetComponents<TextMesh> () [0].text += line;
				cocktailSelectionCocktails [i].GetComponentInChildren<TextMesh>().text = line;
				Texture2D texture = Resources.Load ("Images/" + collection.recipes[i].id) as Texture2D;
				Renderer[] renderers = cocktailSelectionCocktails [i].GetComponentsInChildren<Renderer> ();
				foreach (var renderer in renderers) {
					if (renderer.CompareTag("CocktailSelectionImage")) {
						renderer.material.mainTexture = texture;
					}
				}
			}

			break;

		case State.BarInitialization:
			// Mapping of ingredients and GameObjects
			initializer.init (stateManager.currentRecipe.ingredients);

			instructionsText.GetComponents<TextMesh> () [0].text = "Place Bottles and\nIngredients on Table\nSay 'Continue' if finished";

			break;

		case State.RecipeExecution:
			stateManager.resetSteps ();
            // show first step
			recipeNextCommand ();

			break;

		case State.RecipeFinished:
			stateManager.resetSteps ();

			instructionsText.GetComponents<TextMesh> () [0].text = "Enjoy the Cocktail.\nSay 'Redo' to do it again\nSay 'Exit' to return to selection";

			break;
		}

		updateLabels (stateManager.State);
	}

	private void updateLabels (State state)
	{
		dehighlightAllSpots ();
		deactivateAllSpots ();

		switch (state) {
		case State.CocktailSelection:
			next.SetActive (false);
			previous.SetActive (false);
			exit.SetActive (false);
			redo.SetActive (false);
			done.SetActive (false);
			cocktailImage.SetActive (false);
			break;

		case State.BarInitialization:
			next.SetActive (false);
			previous.SetActive (false);
			exit.SetActive (true);
			redo.SetActive (false);
			done.SetActive (true);
			cocktailImage.SetActive (true);
			Texture2D texture = Resources.Load ("Images/" + stateManager.currentRecipe.picture) as Texture2D;
			cocktailImage.GetComponent<Renderer> ().material.mainTexture = texture;
			activateSpots ();
			break;

		case State.RecipeExecution:
			next.SetActive (true);
			previous.SetActive (stateManager.currentStepCount > 0);
			exit.SetActive (true);
			redo.SetActive (true);
			done.SetActive (false);
			cocktailImage.SetActive (true);
			activateSpots ();
			highlightSpots ();
			break;

		case State.RecipeFinished:
			next.SetActive (false);
			previous.SetActive (false);
			exit.SetActive (true);
			redo.SetActive (true);
			done.SetActive (true);
			cocktailImage.SetActive (true);
			break;
		}
	}

	private RecipeCollection LoadRecipes ()
	{
		TextAsset json = Resources.Load ("recipes") as TextAsset;
		return JsonUtility.FromJson<RecipeCollection> (json.text);
	}

	private void sm_OnStatusChanged (object sender, StatusChangedEventArgs e)
	{
		Debug.Log ("sm_OnStatusChanged: " + e.NewState);
		setupUI ();
	}

	private void kd_OnCommandDetected (object sender, CommandDetectedEventArgs e)
	{
		Debug.Log ("kd_OnCommandDetected: " + e.Command);

		switch (stateManager.State) {
		case State.CocktailSelection:
			this.cocktailSelection (e);
			break;

		case State.BarInitialization:
			this.barinitialization (e);
			break;

		case State.RecipeExecution:
			this.recipeExecution (e);
			break;

		case State.RecipeFinished:
			this.recipeFinished (e);
			break;
		}

		updateLabels (stateManager.State);
	}

	private void cocktailSelection (CommandDetectedEventArgs e)
	{
		switch (e.Command) {
		case SpeechCommand.One:
			if (collection.recipes [0] != null) {
				stateManager.currentRecipe = collection.recipes [0];
				stateManager.State = State.BarInitialization;
			}
			break;
		case SpeechCommand.Two:
			if (collection.recipes [1] != null) {
				stateManager.currentRecipe = collection.recipes [1];
				stateManager.State = State.BarInitialization;
			}
			break;
		case SpeechCommand.Three:
			if (collection.recipes [2] != null) {
				stateManager.currentRecipe = collection.recipes [2];
				stateManager.State = State.BarInitialization;
			}
			break;
		case SpeechCommand.Four:
			if (collection.recipes [3] != null) {
				stateManager.currentRecipe = collection.recipes [3];
				stateManager.State = State.BarInitialization;
			}
			break;
		case SpeechCommand.Five:
			if (collection.recipes [4] != null) {
				stateManager.currentRecipe = collection.recipes [4];
				stateManager.State = State.BarInitialization;
			}
			break;
		case SpeechCommand.Exit:
			break;
		}
	}


	private void barinitialization (CommandDetectedEventArgs e)
	{
		switch (e.Command) {
		case SpeechCommand.Done:
			stateManager.State = State.RecipeExecution;
			processStep (stateManager.currentStep);
			break;

		case SpeechCommand.Exit:
			stateManager.State = State.CocktailSelection;
			break;
		}
	}


	private void recipeFinished (CommandDetectedEventArgs e)
	{
		switch (e.Command) {
		case SpeechCommand.Done:
		case SpeechCommand.Exit:
			stateManager.State = State.CocktailSelection;

			break;

		case SpeechCommand.Repeat:
			stateManager.resetSteps ();
			stateManager.State = State.BarInitialization;

			break;
		}
	}


	private void recipeExecution (CommandDetectedEventArgs e)
	{
		switch (e.Command) {
		case SpeechCommand.Next:
			recipeNextCommand ();
			break;

		case SpeechCommand.Previous:
			recipePrevCommand ();
			break;

		case SpeechCommand.Repeat:
			stateManager.resetSteps ();
			stateManager.State = State.BarInitialization;
			break;

		case SpeechCommand.Exit:
			stateManager.State = State.CocktailSelection;
			break;
		}
	}


	private void recipeNextCommand ()
	{
		Debug.Log ("Process next command. ");
		if (stateManager.hasNextStep ()) {
			Step current = stateManager.next ();
			processStep (current);
		} else {
			Debug.Log ("All steps executed. Recipe execution finished.");
			stateManager.State = State.RecipeFinished;
		}
	}


	private void recipePrevCommand ()
	{
		Debug.Log ("Process prev command. ");

		Step current = stateManager.previous ();
		processStep (current);
	}


	private void processStep (Step current)
	{
		Debug.Log ("Executing step");



		string text = "";
		for (int i = 0; i < current.instructions.Count; i++) {
			text += (i + 1) + ") " + current.instructions [i] + "\n";
		}
		fillTextField (instructionsText.GetComponents<TextMesh> () [0], text);

		videoArea.GetComponent<VideoPlayer> ().clip = null;
		videoArea.GetComponent<VideoPlayer> ().clip = Resources.Load ("Movies/" + current.video, typeof(VideoClip)) as VideoClip;
	}

	protected void fillTextField (TextMesh txt, string val)
	{
		float rowLimit = 3f; //find the sweet spot

		string[] parts = val.Split (' ');
		string tmp = "";
		txt.text = "";
		for (int i = 0; i < parts.Length; i++) {
			tmp = txt.text;

			txt.text += parts [i] + " ";
			if (txt.GetComponent<Renderer> ().bounds.extents.x > rowLimit) {
				tmp += Environment.NewLine;
				tmp += parts [i] + " ";
				txt.text = tmp;
			}
		}
	}


	private Dictionary<Ingredient, GameObject> getEntryById (long id, Dictionary<Ingredient, GameObject> dict)
	{
		Dictionary<Ingredient, GameObject> result = new Dictionary<Ingredient, GameObject> ();

		foreach (KeyValuePair<Ingredient, GameObject> entry in dict) {
			if (entry.Key.id == id) {
				result.Add (entry.Key, entry.Value);
			}
		}

		return result;
	}


	private void dehighlightAllSpots ()
	{
		List<GameObject> tmp = initializer.getAllSpots ();
		foreach (GameObject go in tmp) {
			SpotHighlighter.setHighlighted(false, go);
		}
	}

	private void deactivateAllSpots ()
	{
		List<GameObject> tmp = initializer.getAllSpots ();
		foreach (GameObject go in tmp) {
			SpotHighlighter.deactive(go);
		}
	}

	private void activateSpots() {
		Dictionary<Ingredient, GameObject> dict = initializer.getIngGoMapping ();

		foreach (Ingredient ing in stateManager.currentRecipe.ingredients) {
			SpotHighlighter.activate (dict [ing], ing);
		}
	}

	private void highlightSpots() {
		Dictionary<Ingredient, GameObject> dict = initializer.getIngGoMapping ();

		foreach (KeyValuePair<Ingredient, GameObject> entry in dict) {
			if (stateManager.currentStep.ingredients.Contains(entry.Key.id)) {
				SpotHighlighter.setHighlighted(true, entry.Value);
			}
		}
	}
}