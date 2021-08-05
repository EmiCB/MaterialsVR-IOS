using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingBox : MonoBehaviour {
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider) {
        Debug.Log(collider.gameObject.name + " has entered the area");

        // add object to stored materials list
    }

    void OnTriggerExit(Collider collider) {
        Debug.Log(collider.gameObject.name + " has exited the area");

        // reomve object from stored materials list
    }
}
