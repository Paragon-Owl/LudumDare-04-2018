using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ManagerTest : MonoBehaviour
{
	public EnemySpawner spawn;
	public List<GameObject> items;
	public CraftMachine craftMachine;
	public PlayerInventory pInv;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
			spawn.startSpawner(1);
		if(Input.GetKeyDown(KeyCode.Z))
			spawn.stopSpawner();
		if (Input.GetKeyDown(KeyCode.E))
		{
			pInv.addItem(items[0],100);
			pInv.addItem(items[1],100);
			pInv.addItem(items[2],1);	
		}
	}
}
