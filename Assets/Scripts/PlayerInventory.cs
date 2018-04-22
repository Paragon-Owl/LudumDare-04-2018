using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

	public GameObject itemLibrary;

	private Dictionary<GameObject, int> inventory;
	
	public void addItem(GameObject item, int quantity)
	{
		if (inventory.ContainsKey(item))
		{
			inventory[item] += quantity;
		}
		else
		{
			inventory.Add(item,quantity);
		}
	}

	public void removeItem(GameObject item, int quantity)
	{
		if (inventory.ContainsKey(item))
		{
			inventory[item] -= quantity;
			if (inventory[item] <= 0)
			{
				inventory.Remove(item);
			}
		}
	}
}
