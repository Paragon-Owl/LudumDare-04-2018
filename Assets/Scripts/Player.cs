using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour {

	private bool isFiring = false;

	public Weapon equipedWeapon;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//Firing
		isFiring = CrossPlatformInputManager.GetAxisRaw("Fire1")<=-0.5f || CrossPlatformInputManager.GetButton("Fire1");
		//Reloading
		if(CrossPlatformInputManager.GetButton("Reload"))
			equipedWeapon.Reload();

	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		if(isFiring && equipedWeapon!= null)
			equipedWeapon.Fire();
	}
}
