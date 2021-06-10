using System.Collections;
using UnityEngine;

/// <summary>
/// This class handles the toggling of the controls screen when the corresponding UI button is pressed.
/// </summary>
public class DisplayControlsHandler : MonoBehaviour {
    public GameObject[] molecules;
    public GameObject molecule;

    public GameObject cardboardControls;

    public bool isDisplaying;

    void Start() {
        cardboardControls = GameObject.Find("Controls Screen");
        StartCoroutine(Startup());
    }

    void Update() {
        // find active molecule
        if (molecules.Length != 0) {
            for (int i = 0; i < molecules.Length; i++) {
                if (molecules[i].activeSelf) molecule = molecules[i];
            }

            // toggle display and molecule accordingly
            if (isDisplaying) {
                cardboardControls.SetActive(true);
                molecule.SetActive(false);
            }
            else if (!isDisplaying) {
                cardboardControls.SetActive(false);
                molecule.SetActive(true);
            }
        }
        
    }

    /// <summary>
    /// Toggles control display visibility when UI button is triggered.
    /// </summary>
    public void OnClick() {
        if (isDisplaying) isDisplaying = false;
        else isDisplaying = true;
    }


    // coroutine that runs at launch to collect molecule data
    IEnumerator Startup() {
        GameObject programManager = GameObject.FindGameObjectWithTag("GameController");
        yield return new WaitUntil(() => programManager.GetComponent<LoadAssetBundles>().moleculeList.Length != 0);
        molecules = programManager.GetComponent<LoadAssetBundles>().instantiatedMolecules;
        //controls not displayed by default - must be toggled with button first
        isDisplaying = true;
    }
}
