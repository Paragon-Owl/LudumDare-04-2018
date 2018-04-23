using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FarmingObject : UsableObject {

	protected Vector2 dir;
	protected new BoxCollider2D collider;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	protected void Start()
	{
		collider = gameObject.AddComponent<BoxCollider2D>();
		collider.enabled = false;
		collider.size*=0.5f;
	}

	public abstract void Apply(FieldTile tile);

	override public void Use(Vector2 direction)
	{
		dir = direction;
		collider.enabled = true;
	}

}
