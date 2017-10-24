using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Initializer
{
    public Dictionary<Ingredient, GameObject> ingredientMapping;

    public GameObject[] bottleUIs;
    public GameObject[] miscUIs;
    public GameObject[] bucketUIs;

    private const string bottleGOTag = "BottleHighlightUI";
    private const string bucketGOTag = "BucketHighlightUI";
    private const string miscGOTag = "MiscHighlightUI";

    private List<Ingredient> bottles;
    private List<Ingredient> miscs;
    private List<Ingredient> buckets;

    private List<GameObject> usedGOs; 


    public Initializer()
    {
        Debug.Log("Initializer");
        bottleUIs = GameObject.FindGameObjectsWithTag(bottleGOTag);
        bucketUIs = GameObject.FindGameObjectsWithTag(bucketGOTag);
        miscUIs = GameObject.FindGameObjectsWithTag(miscGOTag);

        if (bottleUIs == null ||
            miscUIs == null ||
            bucketUIs == null)
        {
            throw new Exception("Some game object for highlighting areas were not found.");
        }

        Debug.Log("# bottle ui game objects: " + bottleUIs.Length);
        Debug.Log("# bucket ui game objects: " + bucketUIs.Length);
        Debug.Log("# misc ui game objects: " + miscUIs.Length);
    }

    public Dictionary<Ingredient, GameObject> init(List<Ingredient> ingredients)
    {
        if (ingredients == null)
        {
            throw new ArgumentNullException("Parameter ingredients is null.");
        }

        bottles = new List<Ingredient>();
        miscs = new List<Ingredient>();
        buckets = new List<Ingredient>();

        usedGOs = new List<GameObject>();
        ingredientMapping = new Dictionary<Ingredient, GameObject>();

        foreach (Ingredient ing in ingredients)
        {
            switch(ing.type)
            {
                case IngredientType.BOTTLE:
                    bottles.Add(ing);
                    mapIngredients(ing, bottleUIs); 
                    break;

                case IngredientType.BUCKET:
                    buckets.Add(ing);
                    mapIngredients(ing, bucketUIs);
                    break;

                case IngredientType.MISC:
                    miscs.Add(ing);
                    mapIngredients(ing, miscUIs); 
                    break;
            }
        }

        return ingredientMapping; 
    }


    private void mapIngredients(Ingredient ing, GameObject[] uiArray)
    {
        GameObject go = getFreeBottleGO(uiArray);
        ingredientMapping.Add(ing, go); 
    }


    private GameObject getFreeBottleGO(GameObject[] uiArray)
    {
        foreach(GameObject go in uiArray)
        {
            if( usedGOs.Contains(go) )
            {
                continue; 
            }
            else
            {
                return go; 
            }
        }

        throw new Exception("There are more ingredients given than ui highlights are defined available.");
    }
}
