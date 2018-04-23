using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
public class TopBarScript : MonoBehaviour {

	public GameManager gameManager;
	public TMP_Text truck;
	private string truck_text_base;
	public TMP_Text inventory;
	private string inven_text_base;
	public TMP_Text time;
	private string time_text_base;

	// Use this for initialization
	void Start () {
		inven_text_base = inventory.text;
		time_text_base = time.text;
		truck_text_base = truck.text;
	}

	// Update is called once per frame
	void Update () {
		inventory.text = inven_text_base + gameManager.player.inventory.getQuantityForAll().ToString();
		time.text = time_text_base + Mathf.FloorToInt(gameManager.remainingTimeBeforeDelivery());
		string tmp = truck_text_base;
		truck.text = tmp.Replace("x", gameManager.amountDelivery.ToString());
	}
}
