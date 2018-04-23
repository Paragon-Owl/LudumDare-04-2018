using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed;
	private Vector2 direction;
	public float drag = 0.1f;
	public float damage;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
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
		this.direction = direction.normalized;
		speed = force;
	}

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		EnemyMovement en = other.gameObject.GetComponent<EnemyMovement>();
		if(en)
		{
			other.gameObject.GetComponent<EnemyMovement>().Die();
		}
		Destroy(gameObject);
	}
}
