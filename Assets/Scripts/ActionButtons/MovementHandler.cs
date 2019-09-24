using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementHandler : MonoBehaviour {
    private GameObject[] molecules;
    private GameObject molecule;

    public bool canMove;

    private bool isDragging;

    SphereCollider sc;
    EventTrigger et;
    GvrPointerPhysicsRaycaster gvrPhysicsRaycaster;

    DisplayControlsHandler dch;

    void Start() {
        StartCoroutine(Startup());
        dch = FindObjectOfType<DisplayControlsHandler>();
    }
    void Update() {
        if(molecules.Length != 0 && dch.isDisplaying == false) {
            //find active molecule
            for (int i = 0; i < molecules.Length; i++) {
                if (molecules[i].activeSelf) molecule = molecules[i];
            }

            //get current componenets on molecule
            getMoleculeComponents();

            //add components if they do not exist
            if (sc == null) {
                sc = molecule.AddComponent<SphereCollider>();

                //get current componenets on molecule
                //getMoleculeComponents();

                //set radius for sphere collider
                sc.radius = 9.0f;
            }
            if (et == null) {
                et = molecule.AddComponent<EventTrigger>();

                //get current componenets on molecule
                //getMoleculeComponents();

                //set up event trigger
                EventTrigger.Entry entry1 = new EventTrigger.Entry {
                    eventID = EventTriggerType.PointerDown
                };
                entry1.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
                et.triggers.Add(entry1);

                EventTrigger.Entry entry2 = new EventTrigger.Entry {
                    eventID = EventTriggerType.PointerUp
                };
                entry2.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
                et.triggers.Add(entry2);
            }
            if (gvrPhysicsRaycaster == null) {
                molecule.AddComponent<GvrPointerPhysicsRaycaster>();
            }

            if(canMove && isDragging) dragMolecule();
        }
    }

    public void OnPointerDown(PointerEventData data) {
        //Debug.Log("Pointer down");
        isDragging = true;
    }
    public void OnPointerUp(PointerEventData data) {
        //Debug.Log("Pointer up");
        isDragging = false;
    }

    //move molecule to where player drags it
    public void dragMolecule() {
        //Create a ray going from the camera through the mouse position
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        //Calculate the distance between the Camera and the GameObject, and go this distance along the ray
        Vector3 rayPoint = ray.GetPoint(Vector3.Distance(transform.position, Camera.main.transform.position));
        //Move the GameObject when you drag it
        molecule.transform.position = rayPoint;
    }

    //get current componenets on molecule
    private void getMoleculeComponents() {
        sc = molecule.GetComponent<SphereCollider>();
        et = molecule.GetComponent<EventTrigger>();
        gvrPhysicsRaycaster = molecule.GetComponent<GvrPointerPhysicsRaycaster>();
    }

    //toggle movement mode when button is clicked
    public void OnClick() {
        if (canMove) canMove = false;
        else canMove = true;
    }

    //coroutine that runs at launch to collect molecule data
    IEnumerator Startup() {
        GameObject programManager = GameObject.FindGameObjectWithTag("GameController");
        yield return new WaitUntil(() => programManager.GetComponent<LoadAssetBundles>().moleculeList.Length != 0);
        molecules = programManager.GetComponent<LoadAssetBundles>().instantiatedMolecules;
        //cannot move by default - must be toggled with button first
        canMove = false;
    }
}
