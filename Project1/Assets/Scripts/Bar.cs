using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{

	public RecipeCollection collection;
	public GameObject instructionsText;
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

        keyboardInput = new KeyboardInput();
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
        if (keyboardInputEnabled && keyboardInput != null)
        {
            keyboardInput.isKeyPressed();
        }
    }

	private void setupUI ()
	{
		switch (stateManager.State) {
		case State.CocktailSelection:
			instructionsText.GetComponents<TextMesh> () [0].text = "Say a Cocktail number:\n";
			for (int i = 0; i < collection.recipes.Count; i++) {
				string line = (i + 1) + ") " + collection.recipes [i].id + "\n";
				instructionsText.GetComponents<TextMesh> () [0].text += line;
			}

			break;

		case State.BarInitialization:
			// Mapping of ingredients and GameObjects
			initializer.init (stateManager.currentRecipe.ingredients);

			instructionsText.GetComponents<TextMesh> () [0].text = "Place Bottles and\nIngredients on Table\nSay 'DONE' if finished";

			break;

		case State.RecipeExecution:
                stateManager.resetSteps(); 
                // show first step
                recipeNextCommand();

                break;

		case State.RecipeFinished:
                dehighlightAllSpots();
                stateManager.resetSteps();

                instructionsText.GetComponents<TextMesh>()[0].text = "Enjoy the Cocktail.\nSay 'Repeat' to do it again\nSay 'Done' to return to selection";

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

			break;

		case State.BarInitialization:
                this.barinitialization(e);
                break;
		case State.RecipeExecution:
                this.recipeExecution(e);
                break; 

        case State.RecipeFinished:
                this.recipeFinished(e);
                break;
		}
	}


    private void barinitialization(CommandDetectedEventArgs e)
    {
        switch (e.Command)
        {
            case SpeechCommand.Done:
                stateManager.State = State.RecipeExecution;
                dehighlightAllSpots();

                break; 
            case SpeechCommand.Exit:
                break;
        }
    }


    private void recipeFinished(CommandDetectedEventArgs e)
    {
        switch (e.Command)
        {
            case SpeechCommand.Done:
                dehighlightAllSpots();
                stateManager.State = State.CocktailSelection;
                
                break;

            case SpeechCommand.Repeat:
                dehighlightAllSpots();
                stateManager.resetSteps(); 
                stateManager.State = State.BarInitialization;

                break; 

            case SpeechCommand.Exit:
                break;
        }
    }


    private void recipeExecution(CommandDetectedEventArgs e)
    {
        switch (e.Command)
        {
            case SpeechCommand.Next:
                recipeNextCommand();
                break;

            case SpeechCommand.Previous:
                recipePrevCommand();
                break;
                
        }
    }


    private void recipeNextCommand()
    {
        Debug.Log("Process next command. "); 
        if (stateManager.hasNextStep())
        {
            Step current = stateManager.next();
            processStep(current);
        }
        else
        {
            Debug.Log("All steps executed. Recipe execution finished.");
            stateManager.State = State.RecipeFinished;
        }
    }


    private void recipePrevCommand()
    {
        Debug.Log("Process prev command. ");

        Step current = stateManager.previous(); 
        processStep(current);
    }


    private void processStep(Step current)
    {
        Debug.Log("Executing step");

        // deactivate highlight for all spots
        dehighlightAllSpots();

        Dictionary<Ingredient, GameObject> dict = initializer.getIngGoMapping();

        foreach (string item in current.ingredients)
        {
            Dictionary<Ingredient, GameObject> toHighlight = getEntryById(long.Parse( item ), dict);

            foreach (KeyValuePair<Ingredient, GameObject> entry in toHighlight)
            {
                SpotHighlighter.activate(entry.Value, entry.Key); // can this be shifted into the other method?????
            }
        }

        foreach (string instruction in current.instructions)
        {
            instructionsText.GetComponents<TextMesh>()[0].text = instruction + "\n";
        }
    }


    private Dictionary<Ingredient, GameObject> getEntryById(long id, Dictionary<Ingredient, GameObject> dict)
    {
        Dictionary<Ingredient, GameObject> result = new Dictionary<Ingredient, GameObject>(); 

        foreach (KeyValuePair<Ingredient, GameObject> entry in dict)
        {
            if( entry.Key.id == id )
            {
                result.Add(entry.Key, entry.Value); 
            }
        }

        return result; 
    }


    private void dehighlightAllSpots()
    {
        List<GameObject> tmp = initializer.getAllSpots(); 
        foreach(GameObject go in tmp)
        {
            SpotHighlighter.deactive(go);
        }
    }
}
