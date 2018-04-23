using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		Debug.Log(player.name);
		Debug.Log(player.transform.position);
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
		transform.LookAt(transform);
	}

}
