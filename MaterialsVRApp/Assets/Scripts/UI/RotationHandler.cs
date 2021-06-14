using System.Collections;
using UnityEngine;

using MVR.AssetBundles;

namespace MVR {

/// <summary>
/// This class toggles the rotation of the molecule.
/// </summary>
public class RotationHandler : MonoBehaviour {
    private GameObject[] _molecules;
    private GameObject _molecule;

    public bool isRotating;

    private float _speed = 45.0f;

    void Start() {
        StartCoroutine(Startup());
    }

    private void Update() {
        // find active molecule
        if(_molecules.Length != 0) {
            for (int i = 0; i < _molecules.Length; i++) {
                if (_molecules[i].activeSelf) _molecule = _molecules[i];
            }
            // rotate active molecule
            if (isRotating) _molecule.transform.Rotate(Vector3.up * _speed * Time.deltaTime);
        }
    }

    // toggles rotation mode when pressed
    public void OnClick() {
        if (isRotating) isRotating = false;
        else isRotating = true;
    }

    // coroutine that runs at launch to collect molecule data
    IEnumerator Startup() {
        GameObject programManager = GameObject.FindGameObjectWithTag("GameController");
        yield return new WaitUntil(() => programManager.GetComponent<LoadAssetBundles>().moleculeList.Length != 0);
        _molecules = programManager.GetComponent<LoadAssetBundles>().instantiatedMolecules;
        // is not rotating by default - must be toggled with button first
        isRotating = false;
    }
}

} // namespace MVR
