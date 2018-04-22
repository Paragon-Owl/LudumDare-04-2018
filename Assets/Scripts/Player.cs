using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour {

	private bool isFiring = false;

	private Vector2 aimDir;
	public Weapon equipedWeapon;

	// Use this for initialization
	void Start () {
		aimDir = Vector2.zero;
	}

	// Update is called once per frame
	void Update () {
		//Firing
		isFiring = CrossPlatformInputManager.GetAxisRaw("Fire1")<=-0.5f || CrossPlatformInputManager.GetButton("Fire1");
		//Reloading
		if(CrossPlatformInputManager.GetButton("Reload"))
			equipedWeapon.Reload();

		//Manette
		aimDir.y = CrossPlatformInputManager.GetAxisRaw("AimVertical");
		aimDir.x = CrossPlatformInputManager.GetAxisRaw("AimHorizontal");

	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		if(isFiring && equipedWeapon!= null)
			equipedWeapon.Fire(aimDir);
	}

	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	void OnGUI()
	{
		GUI.Label(new Rect(10,10,100,25), "Right " + aimDir.x + " : " + aimDir.y);
	}
}
