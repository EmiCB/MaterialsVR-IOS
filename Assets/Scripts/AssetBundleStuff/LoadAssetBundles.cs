using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LoadAssetBundles : MonoBehaviour {
    public AssetBundle moleculeAssetBundle;

    public GameObject[] moleculeList;
    public GameObject[] instantiatedMolecules;

    private UnityWebRequest webRequest;

    private string assetBundleServerURL = "";

    void Start() {
        //change to correct path after assetbundles have been uploaded
        assetBundleServerURL = "https://emicb.github.io/MoleculeBundles/" + getAssetBundlePlatformFolder() + "/molecules";

        #if UNITY_EDITOR
        //URLs for testing in the editor
        assetBundleServerURL = "https://emicb.github.io/MoleculeBundles/iOS/molecules";
        //assetBundleServerURL = "http://web.engr.illinois.edu/~schleife/vr_app/AssetBundles/Android/molecules";
        #endif

        //get anf load the assetbundle
        StartCoroutine(GetAssetBundle());
    }

    //NOTE add new if statement for any additional platforms
    //function to get correct path by checking the current platform
    public string getAssetBundlePlatformFolder() {
        RuntimePlatform currentPlatform = Application.platform;

        if (currentPlatform == RuntimePlatform.IPhonePlayer) return "iOS";
        if (currentPlatform == RuntimePlatform.Android) return "Android";
        return "";
    }

    //coroutine to get assetbundle from webserver
    IEnumerator GetAssetBundle() {
        //web request to get assetbundle from webserver, skips crc
        webRequest = UnityWebRequestAssetBundle.GetAssetBundle(assetBundleServerURL, 0);
        Debug.Log(webRequest == null ? "Web request does not exist" : "Web request is present");
        yield return webRequest.SendWebRequest();

        //loads asset bundle
        moleculeAssetBundle = DownloadHandlerAssetBundle.GetContent(webRequest);
        Debug.Log(moleculeAssetBundle == null ? "Failed to load AssetBundle" : "Successfully Loaded AssetBundle");
        //put prefab names into array
        moleculeList = moleculeAssetBundle.LoadAllAssets<GameObject>();

        //instantiate molecules, add them to array of loaded molecules, and set inactive
        instantiatedMolecules = new GameObject[moleculeList.Length];
        for (int i = 0; i < moleculeList.Length; i++) {
            Instantiate(moleculeList[i]);
            instantiatedMolecules[i] = GameObject.Find(moleculeList[i].name + "(Clone)");
            instantiatedMolecules[i].SetActive(false);
        }

        //set a random molecule active
        GameObject randomMolecule = instantiatedMolecules[Random.Range(0, instantiatedMolecules.Length)];
        randomMolecule.SetActive(true);
    }
}
