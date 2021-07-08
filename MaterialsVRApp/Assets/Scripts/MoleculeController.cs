using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This class handles the behavior of each Molecule GameObject it is attached to.
/// </summary>
public class MoleculeController : MonoBehaviour {
    private SphereCollider currentMoleculeCollider;
    private EventTrigger currentEventTrigger;

    private Ray ray;
    private Vector3 rayPoint;
    private float distFromPlayer;

    private bool isSelected;

    void Start() {
        isSelected = false;
        GetMoleculeComponents();
    }

    void Update() {
        // handle click and drag
        if (isSelected && Input.GetMouseButton(0)) {
            DragMolecule();
        }
    }

    /// <summary>
    /// Gets the necessary components of the molecule and adds them if they are missing.
    /// </summary>
    // TODO: rename this method as it is not a getter
    // TODO: split up getting each component into its own method?
    private void GetMoleculeComponents() {
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

            // TODO: make less repetitive if possible
            // set up a pointer enter entry
            EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
            pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
            pointerEnterEntry.callback.AddListener((data) => { OnPointerEnter(); });

            // set up a pointer exit entry
            EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry();
            pointerExitEntry.eventID = EventTriggerType.PointerExit;
            pointerExitEntry.callback.AddListener((data) => { OnPointerExit(); });

            // set up a pointer click entry
            EventTrigger.Entry pointerClickEntry = new EventTrigger.Entry();
            pointerClickEntry.eventID = EventTriggerType.PointerClick;
            pointerClickEntry.callback.AddListener((data) => { OnPointerClick(); });

            // add entries
            currentEventTrigger.triggers.Add(pointerEnterEntry);
            currentEventTrigger.triggers.Add(pointerExitEntry);
            currentEventTrigger.triggers.Add(pointerClickEntry);
        } else {
            currentEventTrigger = gameObject.GetComponent<EventTrigger>();
        }
        
    }

    /// <summary>
    /// Runs when the player's pointer enters the molecule's collider.
    /// </summary>
    public void OnPointerEnter() {
        isSelected = true;
    }

    /// <summary>
    /// Runs when the player's pointer exits the molecule's collider.
    /// </summary>
    public void OnPointerExit() {
        isSelected = false;
    }

    /// <summary>
    /// Runs when the player clicks while their pointer is over the molecule's collider.
    /// </summary>
    public void OnPointerClick() {
        // ray going from the camera through the mouse position
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        // calculate the distance between the Camera and the GameObject, and go this distance along the ray
        distFromPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);
    }

    /// <summary>
    /// Drags the molecule to wherever the player is looking.
    /// </summary>
    private void DragMolecule() {
        // ray going from the camera through the mouse position
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        // calculate the distance between the Camera and the GameObject, and go this distance along the ray
        rayPoint = ray.GetPoint(distFromPlayer);
        // move the GameObject when you drag it
        transform.position = rayPoint;
    }
}
