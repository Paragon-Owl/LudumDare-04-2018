using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftMachine : MonoBehaviour
{
    public GameObject Receipies;

    public List<GameObject> getPossibleCrafts(Dictionary<GameObject,int> inventory)
    {
        List<GameObject> possibleCrafts = new List<GameObject>();
        for(int i = 1; i<Receipies.GetComponentsInChildren<Transform>().Length; i++)
        {
            GameObject r = Receipies.GetComponentsInChildren<Transform>()[i].gameObject;
            Receipe receipe = r.GetComponent<Receipe>();
            if(receipe.canBeCraft(inventory))
                possibleCrafts.Add(receipe.craftedObject);
        }

        return possibleCrafts;
    }
}
