using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace MVR {
namespace AssetBundles {

/// <summary>
/// This class handles the loading of AssetBundles.
/// </summary>
public class LoadAssetBundles : MonoBehaviour {
    private AssetBundle _moleculeAssetBundle;

    public GameObject[] instantiatedMolecules;
    public GameObject[] moleculeList;

    private UnityWebRequest _webRequest;
    private string _assetBundleServerURL = "";

    private void Awake() {
        // set path correctly based on current device platform
        _assetBundleServerURL = "https://schleife.web.illinois.edu/vr_phone/MoleculeBundles/" + getAssetBundlePlatformFolder() + "/molecules";
        
        // get and load the assetbundle
        StartCoroutine(GetAssetBundle());
    }

    /// <summary>
    /// Gets the correct AssetBundle URL by checking the current platform.
    /// </summary>
    /// <returns>The string matching the build platform.</returns>
    /// <remarks>
    /// Add in a new "if" statement for any additional platforms.
    /// </remarks>
    private string getAssetBundlePlatformFolder() {
        RuntimePlatform currentPlatform = Application.platform;

        if (currentPlatform == RuntimePlatform.IPhonePlayer) return "iOS";
        if (currentPlatform == RuntimePlatform.Android) return "Android";

        // defaults to android
        return "Android";
    }

    /// <summary>
    /// Gets the AssetBundle from the webserver.
    /// </summary>
    private IEnumerator GetAssetBundle() {
        Debug.Log("Fetching AssetBundle from: " + _assetBundleServerURL);

        // web request to get assetbundle from webserver, skips crc
        _webRequest = UnityWebRequestAssetBundle.GetAssetBundle(_assetBundleServerURL, 0);
        Debug.Log(_webRequest == null ? "Web request does not exist" : "Web request is present");
        yield return _webRequest.SendWebRequest();

        // load assetbundle
        _moleculeAssetBundle = DownloadHandlerAssetBundle.GetContent(_webRequest);
        Debug.Log(_moleculeAssetBundle == null ? "Failed to load AssetBundle" : "Successfully Loaded AssetBundle");

        // add all prefab names into array of molecules
        moleculeList = _moleculeAssetBundle.LoadAllAssets<GameObject>();

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



} // namespace AssetBundles
} // namespace MVR