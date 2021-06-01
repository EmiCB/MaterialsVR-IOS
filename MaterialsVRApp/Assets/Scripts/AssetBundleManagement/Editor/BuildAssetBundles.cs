using UnityEditor;
using System.IO;
using UnityEngine;

public class BuildAssetBundles {
    // create menu option under "assets" to build assetbundles
    [MenuItem("Assets/Build AssetBundles")]

    /// <summary>
    /// Build all AssetBundles.
    /// </summary>
    /// <remarks>Add a new directory to the "buildTargetSubDirs" array when adding a new build target.</remarks>
    static void BuildAllAssetBundles() {
        // set up all of the build target sub-directories
        string[] buildTargetSubDirs = { "", "/iOS", "/Android" };
        // preset path to build the bundle in
        string assetBundleDirectory = "Desktop/MoleculeBundles";

        // Create local directories if they does not exist already
        for (int targets = 0; targets < buildTargetSubDirs.Length; targets++) {
            string newPath = assetBundleDirectory + buildTargetSubDirs[targets];
            if (!Directory.Exists(newPath)) {
                Directory.CreateDirectory(newPath);
                Debug.Log("Created new directory: " + newPath);
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