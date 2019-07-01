using System.Collections;
using UnityEngine;

public class RotationHandler : MonoBehaviour {
    private GameObject[] molecules;
    private GameObject molecule;

    private bool isRotating;
    [SerializeField]
    private float speed;

    void Start() {
        StartCoroutine(Startup());
    }

    private void Update() {
        //find active molecule
        if(molecules.Length != 0) {
            for (int i = 0; i < molecules.Length; i++) {
                if (molecules[i].activeSelf) molecule = molecules[i];
            }
            //rotate active molecule
            if (isRotating) molecule.transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
    }

    //toggles rotation mode when pressed
    public void OnClick() {
        if (isRotating) isRotating = false;
        else isRotating = true;
    }

    //coroutine that runs at launch to collect molecule data
    IEnumerator Startup() {
        GameObject programManager = GameObject.FindGameObjectWithTag("GameController");
        yield return new WaitUntil(() => programManager.GetComponent<LoadAssetBundles>().moleculeList.Length != 0);
        molecules = programManager.GetComponent<LoadAssetBundles>().instantiatedMolecules;
        //is not rotating by default - must be toggled with button first
        isRotating = false;
    }
}
