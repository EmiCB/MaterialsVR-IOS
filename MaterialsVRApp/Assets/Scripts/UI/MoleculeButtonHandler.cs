using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This class swaps the displayed molecule based on the player's selection.
/// </summary>
public class MoleculeButtonHandler : MonoBehaviour {
    private GameObject[] _molecules;
    private string[] _moleculeNames;

    private string _moleculeName;
    private string _currentMoleculeName;
    
    void Start() {
        StartCoroutine(Startup());
    }

    /// <summary>
    /// Display selected molecule when button is clicked.
    /// </summary>
    public void OnClick() {
        // find active molecule
        for (int i = 0; i < _molecules.Length; i++) {
            if (_molecules[i].activeSelf) {
                _currentMoleculeName = _molecules[i].name;
                Debug.Log("Currently active molecule: " + _currentMoleculeName);
            }
        }
        
        // get selected molecule name
        _moleculeName = gameObject.GetComponentInChildren<TMP_Text>().text + "(Clone)";
        Debug.Log("Selected molecule name: " + _moleculeName);

        // loop through molecules to find selected one
        for (int i = 0; i < _molecules.Length; i++) {
            if (_moleculeName == _molecules[i].name) {
                GameObject.Find(_currentMoleculeName).SetActive(false);
                _molecules[i].SetActive(true);
            }
        }
    }

    /// <summary>
    /// Coroutine that runs at program launch to collect molecule data.
    /// </summary>
    IEnumerator Startup() {
        GameObject programManager = GameObject.FindGameObjectWithTag("GameController");
        // wait until assetbundle has loaded & then set the array of molecules
        yield return new WaitUntil(() => programManager.GetComponent<LoadAssetBundles>().moleculeList.Length != 0);
        // add names to array
        _molecules = programManager.GetComponent<LoadAssetBundles>().moleculeList;
        _moleculeNames = new string[_molecules.Length];
        for(int i = 0; i < _molecules.Length; i++) {
            _moleculeNames[i] = _molecules[i].name;
        }
        _molecules = programManager.GetComponent<LoadAssetBundles>().instantiatedMolecules;
    }
}
