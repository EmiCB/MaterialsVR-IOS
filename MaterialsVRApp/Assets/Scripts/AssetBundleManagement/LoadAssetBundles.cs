using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LoadAssetBundles : MonoBehaviour {
    public AssetBundle moleculeAssetBundle;

    public GameObject[] moleculeList;
    public GameObject[] instantiatedMolecules;

    private UnityWebRequest webRequest;

    private string assetBundleServerURL = "";

    void Awake() {
        // set path correctly based on current device platform
        assetBundleServerURL = "https://schleife.web.illinois.edu/vr_phone/MoleculeBundles/" + getAssetBundlePlatformFolder() + "/molecules";
        
        // get and load the assetbundle
        StartCoroutine(GetAssetBundle());
    }

    /// <summary>
    /// Gets the correct AssetBundle URL by checking the current platform.
    /// </summary>
    /// <remarks>Add in a new "if" statement for any additional platforms.</remarks>
    public string getAssetBundlePlatformFolder() {
        RuntimePlatform currentPlatform = Application.platform;
        Debug.Log(currentPlatform);

        if (currentPlatform == RuntimePlatform.IPhonePlayer) return "iOS";
        if (currentPlatform == RuntimePlatform.Android) return "Android";

        // defaults to android
        return "Android";
    }

    /// <summary>
    /// Gets the AssetBundle from the webserver.
    /// </summary>
    IEnumerator GetAssetBundle() {
        Debug.Log("Fetching AssetBundle from: " + assetBundleServerURL);

        // web request to get assetbundle from webserver, skips crc
        webRequest = UnityWebRequestAssetBundle.GetAssetBundle(assetBundleServerURL, 0);
        Debug.Log(webRequest == null ? "Web request does not exist" : "Web request is present");
        yield return webRequest.SendWebRequest();

        // load asset bundle
        moleculeAssetBundle = DownloadHandlerAssetBundle.GetContent(webRequest);
        Debug.Log(moleculeAssetBundle == null ? "Failed to load AssetBundle" : "Successfully Loaded AssetBundle");

        // add all prefab names into array of molecules
        moleculeList = moleculeAssetBundle.LoadAllAssets<GameObject>();

        // instantiate molecules, add them to array of loaded molecules, and set inactive
        instantiatedMolecules = new GameObject[moleculeList.Length];
        for (int i = 0; i < moleculeList.Length; i++) {
            Instantiate(moleculeList[i]);
            instantiatedMolecules[i] = GameObject.Find(moleculeList[i].name + "(Clone)");
            instantiatedMolecules[i].SetActive(false);
        }

        // set a random molecule active
        GameObject randomMolecule = instantiatedMolecules[Random.Range(0, instantiatedMolecules.Length)];
        randomMolecule.SetActive(true);
    }
}