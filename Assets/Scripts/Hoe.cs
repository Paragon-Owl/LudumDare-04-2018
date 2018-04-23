using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoe : FarmingObject {

	// Use this for initialization
	void Start () {
		base.Start();
	}

	override public void Apply(FieldTile tile)
	{
		Debug.Log("Apply");

		if(tile.currentState == FieldTile.State.EMPTY)
			tile.currentState = FieldTile.State.HOED;

		base.collider.enabled = false;
	}
}
