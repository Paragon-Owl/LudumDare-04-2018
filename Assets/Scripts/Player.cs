using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour {

	private bool isFiring = false;
	public float speed = 1f;
	private Vector3 Axis = Vector3.zero;

	private Vector2 aimDir;
	public Weapon equipedWeapon;
	public Animator spriteAnimator;

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

		// Player Movement (directionVector)
		Axis.x = CrossPlatformInputManager.GetAxisRaw("Horizontal");
		Axis.y = CrossPlatformInputManager.GetAxisRaw("Vertical");

		// Player Aim (directionVector)
		if(Input.GetJoystickNames().Length>0)
		{
			aimDir.y = CrossPlatformInputManager.GetAxisRaw("AimVertical");
			aimDir.x = CrossPlatformInputManager.GetAxisRaw("AimHorizontal");
		}
		else
			AimMouse();


		Animation();

	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		if(isFiring && equipedWeapon!= null)
			equipedWeapon.Fire(aimDir);

		GetComponent<Rigidbody2D>().MovePosition(Vector2.MoveTowards(transform.position, transform.position + Axis , speed * Time.fixedDeltaTime));

	}

	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	void OnGUI()
	{
		GUI.Label(new Rect(10,10,500,50), "Aim " + aimDir);
	}

	void AimMouse()
	{

        Camera  c = Camera.main;
        Vector2 mousePos = Input.mousePosition;

        mousePos.y = c.pixelHeight - mousePos.y;
		Vector3 cursorWordPos = c.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, c.nearClipPlane));

		aimDir = 10*(transform.position - cursorWordPos);
		aimDir.x*=-1;

	}

	private void Animation()
	{

	}
}
