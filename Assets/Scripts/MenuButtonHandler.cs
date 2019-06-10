using UnityEngine;
using UnityEngine.UI;

public class MenuButtonHandler : MonoBehaviour {
    public RectTransform parentPanel;

    private GameObject buttonPrefab;
    private int maxButtons;
    private Object[] molecules;

    void Start() {
        molecules = GetComponent<LoadAssetBundles>().molecules;
        GenerateButtons();
    }

    //generate list of buttons from asset bundle molecules
    void GenerateButtons() {
        //find the button prefab to use as a base
        buttonPrefab = GameObject.Find("ButtonPrefab");
        //set max amount of buttons to length of molecule array
        maxButtons = molecules.Length;
        //create button
        for (int i = 0; i < maxButtons; i++) {
            GameObject newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(parentPanel, false);
            newButton.transform.localScale = new Vector3(1, 1, 1);
            newButton.GetComponentInChildren<Text>().text = molecules[i].name;
        }
        //remove button prefab object
        Destroy(buttonPrefab);
    }
}
