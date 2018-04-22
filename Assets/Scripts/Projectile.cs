using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed;
	private Vector2 direction;
	public float drag = 0.1f;
	public float damage;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Vector2 v = direction*speed*Time.deltaTime;
		transform.position = transform.position + new Vector3(v.x,v.y,0);
		speed = speed - (drag*Time.deltaTime);

		if(speed <= 0.1)
			Destroy(gameObject);
	}

	public void AddForce(Vector2 direction, float force)
	{
		this.direction = direction;
		speed = force;
	}
}
