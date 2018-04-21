using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

	public float speed = 1f;
	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		Vector3 Axis = new Vector2();
		Axis.x = CrossPlatformInputManager.GetAxisRaw("Horizontal");
		Axis.y = CrossPlatformInputManager.GetAxisRaw("Vertical");

		GetComponent<Rigidbody2D>().MovePosition(Vector2.MoveTowards(transform.position, transform.position + Axis , speed * Time.fixedDeltaTime));
	}

}
