﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour {

	public AudioClip FireSound;
	public AudioClip FireEmptySound;
	public AudioClip ReloadSound;

	public GameObject projectile;
	public Transform projectileAnchor;

	public float reloadTime = 10f;
	public float fireRate = 1f;
	public int ammunitionCapacity = 10;
	private float currentAmmunition;
	private float inverseFireRate;

	private bool isReloading;
	private bool isFiring;

	private float lastFireTime;
	private float timeSinceReloadStarted;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		isReloading = false;
		isFiring = false;
		lastFireTime = Time.time;
		inverseFireRate = 1f/fireRate;
		currentAmmunition = ammunitionCapacity;
		audioSource = GetComponent<AudioSource>();
	}

	void Update () {
		//reloading
		if(isReloading)
		{
			//do not reload if already reloading
			if(Time.time-timeSinceReloadStarted >= reloadTime)
			{
				currentAmmunition = ammunitionCapacity;
				audioSource.clip = ReloadSound;
				audioSource.Play();
				timeSinceReloadStarted = Time.time;
			}
			isReloading = false;

		}
		else if(isFiring){

			if(currentAmmunition > 0 && Time.time-lastFireTime >= 1*inverseFireRate)
			{
				lastFireTime = Time.time;
				Instantiate(projectile, projectileAnchor.position, Quaternion.identity);
				currentAmmunition--;
				audioSource.clip = FireSound;
			}
			else
			{
				audioSource.clip = FireEmptySound;
			}

			Debug.Log("Fire");
			audioSource.Play();
		}
		isFiring = false;
	}

	public void Fire() {isFiring = true;}

	public void Reload() {
		if(isReloading)
			return;

		isReloading = true;
	}

}