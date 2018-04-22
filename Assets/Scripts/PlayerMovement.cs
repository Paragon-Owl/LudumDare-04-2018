using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

	public float speed = 1f;
	private Vector3 Axis = Vector3.zero;

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		Axis.x = CrossPlatformInputManager.GetAxis("Horizontal");
		Axis.y = CrossPlatformInputManager.GetAxis("Vertical");

		GetComponent<Rigidbody2D>().MovePosition(Vector2.MoveTowards(transform.position, transform.position + Axis , speed * Time.fixedDeltaTime));
	}

	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	void OnGUI()
	{
		GUI.Label(new Rect(10,40,100,25), "Left " + Axis.x + " : " + Axis.y);
	}

}
