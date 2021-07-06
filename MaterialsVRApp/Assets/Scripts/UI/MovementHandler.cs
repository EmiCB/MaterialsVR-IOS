using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using MVR.AssetBundles;

namespace MVR {

/// <summary>
/// This class toggles the the ability for the player to move the molecule.
/// </summary>
public class MovementHandler : MonoBehaviour {
    private GameObject[] molecules;
    private GameObject molecule;

    public bool isMovable;
    public bool isDragging;

    SphereCollider currentMoleculeCollider;
    EventTrigger currentEventTrigger;

    DisplayControlsHandler displayControlsHandler;


    void Start() {
        StartCoroutine(Startup());
        displayControlsHandler = FindObjectOfType<DisplayControlsHandler>();
    }

    void Update() {
        if (molecules.Length != 0 && displayControlsHandler.isDisplaying == false) {
            // find active molecule
            for (int i = 0; i < molecules.Length; i++) {
                if (molecules[i].activeSelf) molecule = molecules[i];
            }

            // get current componenets on molecule
            getMoleculeComponents();

            if (isMovable && isDragging) dragMolecule();
        }
    }

    public void OnPointerDown(PointerEventData data) {
        Debug.Log("Pointer down");
        isDragging = true;
    }
    public void OnPointerUp(PointerEventData data) {
        Debug.Log("Pointer up");
        isDragging = false;
    }

    // move molecule to where player drags it
    public void dragMolecule() {
        // create a ray going from the camera through the mouse position
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        // calculate the distance between the Camera and the GameObject, and go this distance along the ray
        Vector3 rayPoint = ray.GetPoint(Vector3.Distance(transform.position, Camera.main.transform.position));
        // move the GameObject when you drag it
        molecule.transform.position = rayPoint;
    }

    // get componenets on current molecule
    private void getMoleculeComponents() {
        // set sphere collider or create a new one if it does not exist
        if (molecule.GetComponent<SphereCollider>() == null) {
            currentMoleculeCollider = molecule.AddComponent<SphereCollider>();
            currentMoleculeCollider.radius = 9.0f;
        } else {
            currentMoleculeCollider = molecule.GetComponent<SphereCollider>();
        }
        // set event trigger or create a new one if it does not exist
        /*
        if (molecule.GetComponent<EventTrigger>() == null) {
            currentEventTrigger = molecule.AddComponent<EventTrigger>();

            //set up event trigger
            EventTrigger.Entry entry1 = new EventTrigger.Entry {
                eventID = EventTriggerType.PointerDown
            };
            entry1.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
            currentEventTrigger.triggers.Add(entry1);

            EventTrigger.Entry entry2 = new EventTrigger.Entry {
                eventID = EventTriggerType.PointerUp
            };
            entry2.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
            currentEventTrigger.triggers.Add(entry2);
        }
        */

        /*
        if (molecule.GetComponent<EventTrigger>() == null) {
            currentEventTrigger = molecule.AddComponent<EventTrigger>();

            // set up a pointer down entry
            EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
            pointerDownEntry.eventID = EventTriggerType.PointerDown;
            pointerDownEntry.callback.AddListener((data) => {isDragging = true; });

            // set up a pointer down entry
            EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
            pointerUpEntry.eventID = EventTriggerType.PointerUp;
            pointerUpEntry.callback.AddListener((data) => {isDragging = false; });

            // add entries
            currentEventTrigger.triggers.Add(pointerDownEntry);
            currentEventTrigger.triggers.Add(pointerUpEntry);
        } else {
            currentEventTrigger = molecule.GetComponent<EventTrigger>();
        }
        */

        if (molecule.GetComponent<EventTrigger>() == null) {
            currentEventTrigger = molecule.AddComponent<EventTrigger>();
        } else {
            currentEventTrigger = molecule.GetComponent<EventTrigger>();
        }
        
    }

    // toggle movement mode when button is clicked
    public void OnClick() {
        isMovable = !isMovable;
    }

    // coroutine that runs at launch to collect molecule data
    IEnumerator Startup() {
        GameObject programManager = GameObject.FindGameObjectWithTag("GameController");
        yield return new WaitUntil(() => programManager.GetComponent<LoadAssetBundles>().moleculeList.Length != 0);
        molecules = programManager.GetComponent<LoadAssetBundles>().instantiatedMolecules;
        // controls not displayed by default - must be toggled with button first
        isMovable = false;
    }
}

} // namespace MVR