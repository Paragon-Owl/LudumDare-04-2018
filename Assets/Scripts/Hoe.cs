using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoe : FarmingObject {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	override public void Apply(FieldTile tile)
	{
		tile.currentState = FieldTile.State.HOED;
	}
}
