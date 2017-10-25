using System;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;


public class BarInitializer
{
	public Dictionary<Ingredient, GameObject> ingredientMapping;

	public GameObject[] bottleUIs;
	public GameObject[] miscUIs;
	public GameObject[] cupUIs;

	private const string bottleGOTag = "BottleHighlightUI";
	private const string cupGOTag = "CupHighlightUI";
	private const string miscGOTag = "MiscHighlightUI";

	private List<Ingredient> bottles;
	private List<Ingredient> miscs;
	private List<Ingredient> cups;

	private List<GameObject> usedGOs;


	public BarInitializer ()
	{
		Debug.Log ("Initializer");
		bottleUIs = GameObject.FindGameObjectsWithTag (bottleGOTag);
		cupUIs = GameObject.FindGameObjectsWithTag (cupGOTag);
		miscUIs = GameObject.FindGameObjectsWithTag (miscGOTag);

		if (bottleUIs == null ||
		          miscUIs == null ||
		          cupUIs == null) {
			throw new Exception ("Some game object for highlighting areas were not found.");
		}

		Debug.Log ("# bottle ui game objects: " + bottleUIs.Length);
		Debug.Log ("# bucket ui game objects: " + cupUIs.Length);
		Debug.Log ("# misc ui game objects: " + miscUIs.Length);

        List<GameObject[]> uiLists = new List<GameObject[]>() { bottleUIs, miscUIs, cupUIs };

        foreach (GameObject[] list in uiLists)
        {
            foreach (GameObject go in list)
            {
                // disable Outline
                (go.transform.Find("Spot").GetComponent(typeof(Outline)) as Outline).enabled = false;
                go.SetActive(false);
            }
        }
    }

	public Dictionary<Ingredient, GameObject> init (List<Ingredient> ingredients)
	{
		if (ingredients == null) {
			throw new ArgumentNullException ("Parameter ingredients is null.");
		}

		bottles = new List<Ingredient> ();
		miscs = new List<Ingredient> ();
		cups = new List<Ingredient> ();

		usedGOs = new List<GameObject> ();
		ingredientMapping = new Dictionary<Ingredient, GameObject> ();

		foreach (Ingredient ing in ingredients) {
			switch (ing.type) {
			case IngredientType.BOTTLE:
				bottles.Add (ing);
				mapIngredients (ing, bottleUIs); 
				break;

			case IngredientType.CUP:
				cups.Add (ing);
				mapIngredients (ing, cupUIs);
				break;

			case IngredientType.MISC:
				miscs.Add (ing);
				mapIngredients (ing, miscUIs); 
				break;
			}
		}

        showUsedGOs();

		return ingredientMapping; 
	}


	private void mapIngredients (Ingredient ing, GameObject[] uiArray)
	{
		GameObject go = getFreeGOs (uiArray);
        usedGOs.Add(go);
		ingredientMapping.Add (ing, go); 
	}


	private GameObject getFreeGOs (GameObject[] uiArray)
	{
		foreach (GameObject go in uiArray) {
			if (usedGOs.Contains (go)) {
				continue; 
			} else {
				return go; 
			}
		}

		throw new Exception ("There are more ingredients given than ui highlights are defined available.");
	}

	private void showUsedGOs() {
		List<GameObject[]> uiLists = new List<GameObject[]>() {bottleUIs, miscUIs, cupUIs};

		foreach (GameObject[] list in uiLists) {
			foreach (GameObject go in list) {
				if (usedGOs.Contains (go)) {
					go.SetActive (true);
                    (go.transform.Find("Spot").GetComponent(typeof(Outline)) as Outline).enabled = true;
                }
			}
		}
	}
}
