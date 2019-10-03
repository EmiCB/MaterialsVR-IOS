using UnityEngine;
using UnityEngine.UI;

public class ActionStatusHandler : MonoBehaviour {
    RotationHandler rh;
    MovementHandler mh;

    Text rotationStatusText;
    Text movementStatusText;

    void Awake() {
        rh = FindObjectOfType<RotationHandler>();
        mh = FindObjectOfType<MovementHandler>();

        rotationStatusText = GameObject.Find("RotationStatusText").GetComponent<Text>();
        movementStatusText = GameObject.Find("MovementStatusText").GetComponent<Text>();
    }

    void Update() {
        //update text based on statuses
        rotationStatusText.text = "Rotating: " + rh.isRotating.ToString();
        movementStatusText.text = "Movable: " + mh.canMove.ToString();
    }
}
