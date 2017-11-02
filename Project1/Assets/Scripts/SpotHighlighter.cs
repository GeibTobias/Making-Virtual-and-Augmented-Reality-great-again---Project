using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public static class SpotHighlighter {

    public static void activate(GameObject go, Ingredient ing)
    {
        go.SetActive(true);
        (go.transform.Find("Label").GetComponent(typeof(TextMesh)) as TextMesh).text = ing.name;
    }

	public static void setHighlighted(bool highlighted, GameObject go) {
		(go.transform.Find("Spot").GetComponent(typeof(Outline)) as Outline).enabled = highlighted;
	}

    public static void deactive(GameObject go)
    {
        go.SetActive(false);
    }
}
