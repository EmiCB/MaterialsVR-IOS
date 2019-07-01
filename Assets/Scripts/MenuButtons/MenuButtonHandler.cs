using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonHandler : MonoBehaviour {
    private GameObject[] molecules;
    private string[] moleculeNames;

    private string moleculeName;
    private string currentMoleculeName;
    
    void Start() {
        StartCoroutine(Startup());
    }

    //sets selected molecule active when button is clicked
    public void OnClick() {
        //find active molecule
        for(int i = 0; i < molecules.Length; i++) {
            if (molecules[i].activeSelf) currentMoleculeName = molecules[i].name;
        }
        moleculeName = gameObject.GetComponentInChildren<Text>().text + "(Clone)";
        //loop through molecules to find selected one
        for(int i = 0; i < molecules.Length; i++) {
            if (moleculeName == molecules[i].name) {
                GameObject.Find(currentMoleculeName).SetActive(false);
                molecules[i].SetActive(true);
            }
        }
    }

    //coroutine that runs at launch to collect molecule data
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
