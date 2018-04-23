using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FieldTile : MonoBehaviour {

	public enum State {
		EMPTY,
		HOED,
		PLANTED,
		GROWING,
		GROWN
	}

	public Vector3Int tile;
	public State currentState;
	private float lastGrowingTime;
	public float timeToGrowing = 15f;
	public float timeToGrown = 30f;
	public float timeToHarvest = 60f;
	private bool changed = false;



	// Use this for initialization
	void Start () {
		currentState = State.EMPTY;
		lastGrowingTime = Time.time;
		changed = false;
		BoxCollider2D b = gameObject.AddComponent<BoxCollider2D>();
		b.isTrigger = true;
		b.size*=0.9f;
		b.offset+= new Vector2(0.5f,0.5f);
		gameObject.layer = 13;
	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		switch (currentState)
		{
			case State.EMPTY:
			case State.HOED:
				return;
			case State.PLANTED:
				if(Time.time-lastGrowingTime >= timeToGrowing)
				{
					currentState = State.GROWING;
					lastGrowingTime = Time.time;
					changed = true;
				}
				break;
			case State.GROWING:
				if(Time.time-lastGrowingTime >= timeToGrown)
				{
					currentState = State.GROWN;
					lastGrowingTime = Time.time;
					changed = true;
				}
				break;
			case State.GROWN:
				if(Time.time-lastGrowingTime >= timeToHarvest)
				{
					currentState = State.EMPTY;
					changed = true;
				}
				break;
			default:
				return;
		}
	}

	public bool hasChanged()
	{
		if(changed)
		{
			changed = false;
			return true;
		}
		return false;
	}

	public int Harvest()
	{
		currentState = State.EMPTY;
		changed = true;
		return 1;
	}

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		FarmingObject fo = other.gameObject.GetComponent<FarmingObject>();
		if(fo)
		{
			fo.Apply(this);
			changed = true;
		}
	}

}
