using UnityEditor;
using System.IO;
using UnityEngine;

public class BuildAssetBundles {
    //creates menu option under assets to build assetbundles
    [MenuItem("Assets/Build AssetBundles")]

    //builds assetbundle
    static void BuildAllAssetBundles() {
        //NOTE add new directory to this array when adding build targets
        string[] buildTargetSubDirs = { "", "/iOS", "/Android" };
        //preset paths to build the bundle
        string assetBundleDirectory = "Desktop/MoleculeBundles";

        //creates local directories if they does not exist already
        for (int targets = 0; targets < buildTargetSubDirs.Length; targets++) {
            string newPath = assetBundleDirectory + buildTargetSubDirs[targets];
            if (!Directory.Exists(newPath)) {
                Directory.CreateDirectory(newPath);
                Debug.Log("Created new directory " + newPath);
            }
        }

        //NOTE copy and paste lines below and change target and path for aditional build targets
        //TODO make into loop somehow?
        //builds to directory, uses LZMA compression & LZ4 recompression, builds for iOS
        BuildPipeline.BuildAssetBundles(assetBundleDirectory + buildTargetSubDirs[1], BuildAssetBundleOptions.None, BuildTarget.iOS);
        //builds to directory, uses LZMA compression & LZ4 recompression, builds for Android
        BuildPipeline.BuildAssetBundles(assetBundleDirectory + buildTargetSubDirs[2], BuildAssetBundleOptions.None, BuildTarget.Android);
    }
}