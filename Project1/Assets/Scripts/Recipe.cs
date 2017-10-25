using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Recipe
{
	public string id;
	public List<string> keywords;
	public List<Step> steps;
	public string picture;
	public List<Ingredient> ingredients;
}
