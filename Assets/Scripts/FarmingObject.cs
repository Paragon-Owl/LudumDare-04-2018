using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FarmingObject : MonoBehaviour {
	abstract public void Apply(FieldTile tile);
}
