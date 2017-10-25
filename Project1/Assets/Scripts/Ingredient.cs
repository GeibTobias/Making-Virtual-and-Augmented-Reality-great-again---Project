using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum IngredientType
{
	BOTTLE,
	MISC,
	CUP
}

[Serializable]
public class Ingredient
{
	public long id;
	public string name;
	public IngredientType type;
}
