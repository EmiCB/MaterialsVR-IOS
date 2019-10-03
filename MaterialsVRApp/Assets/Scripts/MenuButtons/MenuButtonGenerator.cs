using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuButtonGenerator : MonoBehaviour {
    public RectTransform parentPanel;

    private GameObject[] molecules;
    private GameObject buttonPrefab;

    private int maxButtons;

    void Start() {
        StartCoroutine(Startup());
    }

    //generate list of buttons from asset bundle molecules
    void GenerateButtons() {
        //set max amount of buttons to length of molecule array
        maxButtons = molecules.Length;
        //create buttons
        for (int i = 0; i < maxButtons; i++) {
            GameObject newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(parentPanel, false);
            newButton.GetComponentInChildren<Text>().text = molecules[i].name;
            Debug.Log(molecules[i].name);
        }
        //remove button prefab object after making the buttons
        Destroy(buttonPrefab);
    }

    //coroutine that runs at launch to collect molecule data
    IEnumerator Startup() {
        //wait until assetbundle has loaded & then set the array of molecules
        yield return new WaitUntil(() => GetComponent<LoadAssetBundles>().moleculeList.Length != 0);
        molecules = GetComponent<LoadAssetBundles>().moleculeList;
        //find the button prefab to use as a base
        buttonPrefab = GameObject.Find("ButtonPrefab");
        GenerateButtons();
    }
}
