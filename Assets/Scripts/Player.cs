using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{

	public GameManager GameManager;
	public List<GameObject> equipableObject;

	private bool isFiring = false;
	public float speed = 1f;
	private Vector3 Axis = Vector3.zero;
	public int equipedObjectIndex = 0;

	private Vector2 aimDir;
	private Vector2 fireDirection = Vector2.right;
	public UsableObject equipedObject;
	public Animator spriteAnimator;
	public PlayerInventory inventory;
	public List<string> animationNames = new List<string>{
		"PlayerEast",
		"PlayerNorthEast",
		"PlayerNorth",
		"PlayerNorthWest",
		"PlayerWest",
		"PlayerSouthWest",
		"PlayerSouth",
		"PlayerSouthEast"
	};


	// Use this for initialization
	void Start () {
		aimDir = Vector2.zero;
		inventory = GetComponent<PlayerInventory>();
	}

	// Update is called once per frame
	void Update () {
		//Firing
		isFiring = CrossPlatformInputManager.GetAxisRaw("Fire1")<=-0.5f || CrossPlatformInputManager.GetButton("Fire1");
		//Reloading
		/*if(CrossPlatformInputManager.GetButton("Reload"))
			equipedObject.UseSecondary();*/

		if(CrossPlatformInputManager.GetButtonDown("Action"))
		{
			if (GetComponentInChildren<Pickup>().isCollidingTruck())
			{
				int quantityNeeded = GameManager.getTruckStockMissing();
				int quantityAvailable = inventory.fill(quantityNeeded);
				GameManager.addInTruckStock(quantityAvailable);	
			}
			int count = GetComponentInChildren<Pickup>().getCrops();
			if(count > 0)
				GetComponent<PlayerInventory>().addItem(GetComponent<PlayerInventory>().getItemFromIndex(3), count);
		}

		if(CrossPlatformInputManager.GetButton("InventoryLeft"))
		{
			equipableObject[equipedObjectIndex%equipableObject.Count].SetActive(false);
			equipedObjectIndex++;
			equipableObject[equipedObjectIndex%equipableObject.Count].SetActive(true);
		}
		else if(CrossPlatformInputManager.GetButton("InventoryRight"))
		{
			equipableObject[equipedObjectIndex%equipableObject.Count].SetActive(false);
			equipedObjectIndex--;
			equipableObject[equipedObjectIndex%equipableObject.Count].SetActive(true);
		}

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

		fireDirection = aimDir!=Vector2.zero? //tern1 start
								aimDir:	//true tern1
								Axis!=Vector3.zero? //false tern1, tern2 start
									new Vector2(Axis.x, Axis.y): //true tern2
									fireDirection; //false tern2

		Animation();

	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		if(isFiring && equipedObject!= null)
		{
			equipedObject.Use(fireDirection);
		}

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

		aimDir = (transform.position - cursorWordPos);
		aimDir.Normalize();
		aimDir.x*=-1;

	}

	private void Animation()
	{
		Vector2 dir = Axis;
		if(aimDir!=Vector2.zero)
		{
			dir = aimDir;
		}
		float angle = Vector3.Angle(Vector3.right, dir);


		if(dir.y < 0)
			angle = 360-angle;

		angle+=Mathf.Rad2Deg*(Mathf.PI/8f);
		spriteAnimator.Play(animationNames[Mathf.FloorToInt(angle/45f)%animationNames.Count]);
	}

}
