using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LoadAssetBundles : MonoBehaviour {
    public AssetBundle moleculeAssetBundle;
    public Object[] molecules;

    private string assetBundleServerURL = "http://web.engr.illinois.edu/~schleife/vr_app/AssetBundles/Android/molecules";
    private UnityWebRequest webRequest;

    void Start() {
        StartCoroutine(GetAssetBundle());
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
        molecules = moleculeAssetBundle.LoadAllAssets();
        //instantiate a random molecule
        Instantiate(molecules[Random.Range(0, molecules.Length)]);
    }
}
