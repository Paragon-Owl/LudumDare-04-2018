using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

	public List<GameObject> actifItem;
	public List<GameObject> seeds;
	
	private Dictionary<GameObject, int> passifInventory = new Dictionary<GameObject, int>();
	
	public void addItem(GameObject item, int quantity)
	{
		if (passifInventory.ContainsKey(item))
		{
			passifInventory[item] += quantity;
		}
		else
		{
			passifInventory.Add(item,quantity);
		}
	}

	public Dictionary<GameObject, int> getInventory()
	{
		return passifInventory;
	}

	public void removeItem(GameObject item, int quantity)
	{
		if (passifInventory.ContainsKey(item))
		{
			passifInventory[item] -= quantity;
			if (passifInventory[item] <= 0)
			{
				passifInventory.Remove(item);
			}
		}
	}

	public GameObject getItemFromIndex(int index)
	{
		if (index > 2)
		{
			return seeds[index - 2];
		}
		else
		{
			return actifItem[index];
		}
	}
}
