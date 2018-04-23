using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : FarmingObject {

	// Use this for initialization
	void Start () {
		base.Start();
	}

	override public void Apply(FieldTile tile)
	{

		if(tile.currentState == FieldTile.State.HOED)
			tile.currentState = FieldTile.State.PLANTED;

		base.collider.enabled = false;
	}
}
