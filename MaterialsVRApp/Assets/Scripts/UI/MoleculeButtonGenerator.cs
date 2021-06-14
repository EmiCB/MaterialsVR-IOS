using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

using MVR.AssetBundles;

namespace MVR {

/// <summary>
/// This class automatically generates UI buttons for each molecule pulled from the AssetBundle webserver.
/// </summary>
public class MoleculeButtonGenerator : MonoBehaviour {
    public RectTransform parentPanel;

    private GameObject[] _molecules;
    private GameObject _buttonPrefab;

    private int _maxButtons;

    void Start() {
        StartCoroutine(Startup());
    }

    /// <summary>
    /// Generates button prefabs from the AssetBundle molecules and puts them into the scrollable list.
    /// </summary>
    void GenerateButtons() {
        // set max number of buttons to length of the molecule array
        _maxButtons = _molecules.Length;

        // create buttons for each molecule
        for (int i = 0; i < _maxButtons; i++) {
            GameObject newButton = Instantiate(_buttonPrefab);
            newButton.transform.SetParent(parentPanel, false);
            newButton.GetComponentInChildren<TMP_Text>().text = _molecules[i].name;
            Debug.Log("Button generated for " + _molecules[i].name);
        }

        // remove original button prefab object after all buttons have been made
        Destroy(_buttonPrefab);
    }

    /// <summary>
    /// Coroutine that runs at program launch to collect molecule data.
    /// </summary>
    IEnumerator Startup() {
        // wait until assetbundle has loaded & then set the array of molecules
        yield return new WaitUntil(() => GetComponent<LoadAssetBundles>().moleculeList.Length != 0);
        _molecules = GetComponent<LoadAssetBundles>().moleculeList;

        // find the button prefab to use as a base
        _buttonPrefab = GameObject.Find("Molecule Button");
        GenerateButtons();
    }
}

} // namespace MVR
