using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ExtensionMethods {

    public static T[] GetComponentsInDirectChildren<T>(this GameObject gameObject) {
        int indexer = gameObject.transform.Cast<Transform>().Count(transform => transform.GetComponent<T>() != null);
        var returnArray = new T[indexer];
        indexer = 0;

        foreach (Transform transform in gameObject.transform) {
            if (transform.GetComponent<T>() != null) {
                returnArray[indexer++] = transform.GetComponent<T>();
            }
        }
        return returnArray;
    }
}
