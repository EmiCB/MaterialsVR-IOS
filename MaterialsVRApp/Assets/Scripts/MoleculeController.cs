using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoleculeController : MonoBehaviour {
    SphereCollider currentMoleculeCollider;
    EventTrigger currentEventTrigger;

    public bool isSelected;

    // Start is called before the first frame update
    void Start() {
        isSelected = false;
        getMoleculeComponents();
    }

    // Update is called once per frame
    void Update() {
        // handle dragging
        if (isSelected && Input.GetMouseButton(0)) {
            dragMolecule();
        }
    }

    // get componenets on current molecule
    private void getMoleculeComponents() {
        // set sphere collider or create a new one if it does not exist
        if (gameObject.GetComponent<SphereCollider>() == null) {
            currentMoleculeCollider = gameObject.AddComponent<SphereCollider>();
            currentMoleculeCollider.radius = 9.0f;
        } else {
            currentMoleculeCollider = gameObject.GetComponent<SphereCollider>();
        }

        // set event trigger or create a new one if it does not exist
        if (gameObject.GetComponent<EventTrigger>() == null) {
            currentEventTrigger = gameObject.AddComponent<EventTrigger>();

            // set up a pointer down entry
            EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
            pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
            pointerEnterEntry.callback.AddListener((data) => { OnPointerEnter(); });

            // set up a pointer down entry
            EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry();
            pointerExitEntry.eventID = EventTriggerType.PointerExit;
            pointerExitEntry.callback.AddListener((data) => { OnPointerExit(); });

            // add entries
            currentEventTrigger.triggers.Add(pointerEnterEntry);
            currentEventTrigger.triggers.Add(pointerExitEntry);
        } else {
            currentEventTrigger = gameObject.GetComponent<EventTrigger>();
        }
        
    }

    public void OnPointerEnter() {
        isSelected = true;
    }

    public void OnPointerExit() {
        isSelected = false;
    }

    // move molecule to where player drags it
    public void dragMolecule() {
        // create a ray going from the camera through the mouse position
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        // calculate the distance between the Camera and the GameObject, and go this distance along the ray
        Vector3 rayPoint = ray.GetPoint(Vector3.Distance(transform.position, Camera.main.transform.position));
        // move the GameObject when you drag it
        transform.position = rayPoint;
    }
}
