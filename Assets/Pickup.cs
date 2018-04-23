using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pickup : MonoBehaviour
{
    private List<FieldTile> fields = new List<FieldTile>();
    private bool collideTruck;

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("ColliderTruck"))
        {
            collideTruck = true;
        }
        else
        {
            FieldTile fieldTile = other.gameObject.GetComponent<FieldTile>();
            if (fieldTile)
            {
                if (fieldTile.currentState == FieldTile.State.GROWN)
                    fields.Add(fieldTile);
            }
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("ColliderTruck"))
        {
            collideTruck = false;
        }
        else
        {
            FieldTile fieldTile = other.gameObject.GetComponent<FieldTile>();
            if (fieldTile)
            {
                if (fields.Contains(fieldTile))
                    fields.Remove(fieldTile);
            }
        }
    }

    public int getCrops()
    {
        int count = 0;
        foreach (FieldTile field in fields)
        {
            count += field.Harvest();
        }

        fields.Clear();
        return count;
    }

    public bool isCollidingTruck()
    {
        return collideTruck;
    }
}