using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoleculeButtonHandler : MonoBehaviour {
    private GameObject[] molecules;
    private string[] moleculeNames;

    private string moleculeName;
    private string currentMoleculeName;
    
    void Start() {
        StartCoroutine(Startup());
    }

    /// <summary>
    /// Display selected molecule when button is clicked.
    /// </summary>
    public void OnClick() {
        //find active molecule
        for (int i = 0; i < molecules.Length; i++) {
            if (molecules[i].activeSelf) {
                currentMoleculeName = molecules[i].name;
                Debug.Log("Currently active molecule: " + currentMoleculeName);
            }
        }
        
        // get selected molecule name
        moleculeName = gameObject.GetComponentInChildren<TMP_Text>().text + "(Clone)";
        Debug.Log("Selected molecule name: " + moleculeName);

        //loop through molecules to find selected one
        for (int i = 0; i < molecules.Length; i++) {
            if (moleculeName == molecules[i].name) {
                GameObject.Find(currentMoleculeName).SetActive(false);
                molecules[i].SetActive(true);
            }
        }
    }

    /// <summary>
    /// Coroutine that runs at launch to collect molecule data.
    /// </summary>
    IEnumerator Startup() {
        GameObject programManager = GameObject.FindGameObjectWithTag("GameController");
        //wait until assetbundle has loaded & then set the array of molecules
        yield return new WaitUntil(() => programManager.GetComponent<LoadAssetBundles>().moleculeList.Length != 0);
        //add names to array
        molecules = programManager.GetComponent<LoadAssetBundles>().moleculeList;
        moleculeNames = new string[molecules.Length];
        for(int i = 0; i < molecules.Length; i++) {
            moleculeNames[i] = molecules[i].name;
        }
        molecules = programManager.GetComponent<LoadAssetBundles>().instantiatedMolecules;
    }
}
