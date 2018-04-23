using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	private List<FieldTile> fields;

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		FieldTile fieldTile;
		if(fieldTile = other.gameObject.GetComponent<FieldTile>())
		{
			if(fieldTile.currentState == FieldTile.State.)
		}
	}
	 /// <summary>
	 /// Sent when another object leaves a trigger collider attached to
	 /// this object (2D physics only).
	 /// </summary>
	 /// <param name="other">The other Collider2D involved in this collision.</param>
	 void OnTriggerExit2D(Collider2D other)
	 {
		 FieldTile fieldTile;
		if(fieldTile = other.gameObject.GetComponent<FieldTile>())
		{

		}
	 }
}
