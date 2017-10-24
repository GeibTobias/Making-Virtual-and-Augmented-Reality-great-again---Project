using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {

	public RecipeCollection collection;
	public GameObject instructionsText;
    public Initializer initializer;
    private StateManager stateManager;
	private KeywordDetector keywordDetector;

    

    // Use this for initialization
    void Start () {
		stateManager = new StateManager();
		stateManager.StatusChanged += sm_OnStatusChanged;

		keywordDetector = this.GetComponent<KeywordDetector> ();
		keywordDetector.CommandDetected += kd_OnCommandDetected;

		LoadRecipes();

        // Mapping of ingredients and GameObjects
        initializer = new Initializer();
        //initializer.init(ingredients);

        setupUI ();
	}

	// Update is called once per frame
	void Update () {
				
	}

	private void setupUI () {
		switch (stateManager.State) {
		case State.CocktailSelection:
			instructionsText.GetComponents<TextMesh> () [0].text = "";
			for (int i = 0; i < collection.recipes.Count; i++) {
				string line = (i+1) + ") " + collection.recipes [i].id + "\n";
				instructionsText.GetComponents<TextMesh> () [0].text += line;
			}

			break;

		case State.BarInitialization:
			instructionsText.GetComponents<TextMesh> () [0].text = "Place Bottles and\nIngredients on Table";

			break;

		case State.RecipeExecution:
			break;

		case State.RecipeFinished:
			break;
		}	
	}

	private void LoadRecipes() {
		TextAsset json = Resources.Load("recipes") as TextAsset;
		collection = JsonUtility.FromJson<RecipeCollection> (json.text);
	}

	private void sm_OnStatusChanged(object sender, StatusChangedEventArgs e) {
		Debug.Log("sm_OnStatusChanged: " + e.NewState);
		setupUI ();
	}

	private void kd_OnCommandDetected(object sender, CommandDetectedEventArgs e) {
		Debug.Log("kd_OnCommandDetected: " + e.Command);

		switch (stateManager.State) {
		case State.CocktailSelection:
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
			case SpeechCommand.None:
			case SpeechCommand.Next:
			case SpeechCommand.Previous:
			case SpeechCommand.Done:
			case SpeechCommand.Exit:
				break;
			}

			break;

		case State.BarInitialization: case State.RecipeExecution: case State.RecipeFinished:
			break;
		}
	}
}
