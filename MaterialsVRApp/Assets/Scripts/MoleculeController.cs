using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This class handles the behavior of each Molecule GameObject it is attached to.
/// </summary>
public class MoleculeController : MonoBehaviour {
    private SphereCollider _sphereCollider;
    private EventTrigger _eventTrigger;

    private Ray _reticleRay;
    private Vector3 _reticleRayPoint;
    private float _distFromPlayer;

    private bool _isSelected;

    void Start() {
        _isSelected = false;
        FetchOrCreateMoleculeComponents();
    }

    void Update() {
        // handle click and drag
        if (_isSelected && Input.GetMouseButton(0)) {
            DragMolecule();
        }
    }

    /// <summary>
    /// Finds the necessary components of the molecule or adds them if they are missing.
    /// </summary>
    private void FetchOrCreateMoleculeComponents() {
        // set sphere collider or create a new one if it does not exist
        if (gameObject.GetComponent<SphereCollider>() == null) {
            _sphereCollider = gameObject.AddComponent<SphereCollider>();
            _sphereCollider.radius = 9.0f;
        } else {
            _sphereCollider = gameObject.GetComponent<SphereCollider>();
        }

        // set event trigger or create a new one if it does not exist
        if (gameObject.GetComponent<EventTrigger>() == null) {
            _eventTrigger = gameObject.AddComponent<EventTrigger>();

            AddEventTriggerEntry(EventTriggerType.PointerEnter);
            AddEventTriggerEntry(EventTriggerType.PointerExit);
            AddEventTriggerEntry(EventTriggerType.PointerClick);
        } else {
            _eventTrigger = gameObject.GetComponent<EventTrigger>();
        }
        
    }

    /// <summary>
    /// Adds an entry of a given type to the EventTrigger.
    /// </summary>
    /// <param name="eventTriggerType">The type of EventTrigger to add</param>
    private void AddEventTriggerEntry(EventTriggerType eventTriggerType) {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventTriggerType;

        // select callback method
        switch (eventTriggerType) {
            case EventTriggerType.PointerEnter:
                entry.callback.AddListener((data) => { OnPointerEnter(); });
                break;
            case EventTriggerType.PointerExit:
                entry.callback.AddListener((data) => { OnPointerExit(); });
                break;
            case EventTriggerType.PointerClick:
                entry.callback.AddListener((data) => { OnPointerClick(); });
                break;
        }

        _eventTrigger.triggers.Add(entry);
    }

    /// <summary>
    /// Runs when the player's pointer enters the molecule's collider.
    /// </summary>
    public void OnPointerEnter() {
        _isSelected = true;
    }

    /// <summary>
    /// Runs when the player's pointer exits the molecule's collider.
    /// </summary>
    public void OnPointerExit() {
        _isSelected = false;
    }

    /// <summary>
    /// Runs when the player clicks while their pointer is over the molecule's collider.
    /// </summary>
    public void OnPointerClick() {
        // ray going from the camera through the mouse position
        _reticleRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        // calculate the distance between the Camera and the GameObject, and go this distance along the ray
        _distFromPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);
    }

    /// <summary>
    /// Drags the molecule to wherever the player is looking.
    /// </summary>
    private void DragMolecule() {
        // ray going from the camera through the mouse position
        _reticleRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        // calculate the distance between the Camera and the GameObject, and go this distance along the ray
        _reticleRayPoint = _reticleRay.GetPoint(_distFromPlayer);
        // move the GameObject when you drag it
        transform.position = _reticleRayPoint;
    }
}
