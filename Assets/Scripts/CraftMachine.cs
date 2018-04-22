using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftMachine : MonoBehaviour
{
    public GameObject Receipies;

    public List<GameObject> getPossibleCrafts(Dictionary<GameObject, int> inventory)
    {
        List<GameObject> possibleCrafts = new List<GameObject>();
        for (int i = 1; i < Receipies.GetComponentsInChildren<Transform>().Length; i++)
        {
            GameObject r = Receipies.GetComponentsInChildren<Transform>()[i].gameObject;
            Receipe receipe = r.GetComponent<Receipe>();
            if (receipe.canBeCraft(inventory))
                possibleCrafts.Add(receipe.craftedObject);
        }

        return possibleCrafts;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            List<GameObject> items = getPossibleCrafts(other.gameObject.GetComponent<PlayerInventory>().getInventory());
            showNameGameObjectInList(items);
            //Ici mettre en place l'affichage des item disponible dans la craft machine;
        }
    }

    private void showNameGameObjectInList(List<GameObject> list)
    {
        String debugLine = "[";
        foreach (GameObject go in list)
        {
            debugLine += go.name + ", ";
        }

        debugLine += "]";
        Debug.Log(debugLine);
    }
}