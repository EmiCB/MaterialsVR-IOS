using UnityEngine;
using UnityEngine.UI;

public class GazeInput : MonoBehaviour {
    private float timer;
    private float currentGazeTime = 2.0f;

    private bool isGazedAt;

    void Start() {
        //reset timer on application start
        timer = 0.0f;
    }

    void Update() {
        //activate when recticle pointer is on selectable object
        if (isGazedAt) {
            //start timer
            timer += Time.deltaTime;  
            //check for timer end & execute 
            if (timer >= currentGazeTime) {
                PointerGaze();
                timer = 0.0f;
            }
        }
    }

    //activate when recticle pointer goes over object
    public void PointerEnter() {
        isGazedAt = true;
    }
    //activate when recticle pointer leaves object
    public void PointerExit() {
        isGazedAt = false;
        timer = 0.0f;
    }

    //activate when player has gazed at an object for the correct amount of time
    public void PointerGaze() {
        //presses molecule buttons
        if (gameObject.tag == "MoleculeButton") {
            gameObject.GetComponent<MenuButtonHandler>().OnClick();
        }
        //for action buttons
        else if (gameObject.tag == "ActionButton") {
            //presses rotation button
            if (gameObject.GetComponentInChildren<Text>().text == "Toggle Rotation") {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<RotationHandler>().OnClick();
            }
            //presses movement button
            if (gameObject.GetComponentInChildren<Text>().text == "Toggle Movement") {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<MovementHandler>().OnClick();
            }
        }
    }
}
