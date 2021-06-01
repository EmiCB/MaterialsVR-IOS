using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MoleculeButtonGenerator : MonoBehaviour {
    public RectTransform parentPanel;

    private GameObject[] molecules;
    private GameObject buttonPrefab;

    private int maxButtons;

    void Start() {
        StartCoroutine(Startup());
    }

    /// <summary>
    /// Generate button prefabs from asset bundle molecules into scrollable list.
    /// </summary>
    void GenerateButtons() {
        // set max number of buttons to length of molecule array
        maxButtons = molecules.Length;

        // create buttons
        for (int i = 0; i < maxButtons; i++) {
            GameObject newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(parentPanel, false);
            newButton.GetComponentInChildren<TMP_Text>().text = molecules[i].name;
            Debug.Log("Button generated for " + molecules[i].name);
        }

        // remove initial button prefab object after making the buttons
        Destroy(buttonPrefab);
    }

    /// <summary>
    /// Coroutine that runs at launch to collect molecule data.
    /// </summary>
    IEnumerator Startup() {
        //wait until assetbundle has loaded & then set the array of molecules
        yield return new WaitUntil(() => GetComponent<LoadAssetBundles>().moleculeList.Length != 0);
        molecules = GetComponent<LoadAssetBundles>().moleculeList;
        //find the button prefab to use as a base
        buttonPrefab = GameObject.Find("Molecule Button");
        GenerateButtons();
    }
}
