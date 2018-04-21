using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receipe : MonoBehaviour
{
    [System.Serializable]
    public class ReceipeContent
    {
        public GameObject item;
        public int quantity;
    }
    public List<ReceipeContent> neededObjects;
    public GameObject craftedObject;

    public bool canBeCraft(Dictionary<GameObject, int> inventory)
    {
        foreach (ReceipeContent r in neededObjects)
        {
            if (!(inventory.ContainsKey(r.item) && inventory[r.item] > r.quantity))
            {
                return false;
            }
        }

        return true;
    }
}