# MaterialsVR-IOS
## Description
Using: Unity 2018.4.1f1, GVR 1.200

Materials VR uses virtual reality to help students visualize molecules. Molecules can be selected from a list and moved around by the user. The Google Cardboard version (this one currently) includes gaze input which allows the user to gaze at a button for 2 seconds to activate it.

Current platforms: iOS, Android

Supported VR devices: Google Cardboard

# Editing the Project
## Unity Project Setup
1. Clone or download the repository
2. Open with the correct Unity version (2018.4.1f1)

## Asset Bundle Building Instructions
1. Export Blender file as a .fbx
2. Drag and drop file into the "~MoleculeModels" folder in Unity 
3. Drag and drop model into Unity hierarchy from "~MoleculeModels" folder
4. Drag and drop from the hierarchy into the "~AssetBundles/Molecules" folder to create a new prefab
5. Delete object from hierarchy
6. Select and, in the inspector window at the bottom, set the Asset Bundle field to “molecules"
7. Under “Assets” click “Build AssetBundles” (it should build to the desktop)
8. Drag and drop the built asset bundle folder called "MoleculeBundles" into the web server files

## Development for New Platforms
### BuildAssetBundles.cs
Add your new build target into the build target sub directories array
Ex:
```csharp
string[] buildTargetSubDirs = { "", "/iOS", "/Android", "/NewPlatform" };
```
Add a build pipeline line to create the bundles
Ex:
```csharp
BuildPipeline.BuildAssetBundles(assetBundleDirectory + buildTargetSubDirs[3], BuildAssetBundleOptions.None, BuildTarget.NewPlatform);
```

### LoadAssetBundles.cs
Add the new platform into the getAssetBundlePlatformFolder method
Ex:
```csharp
if (currentPlatform == RuntimePlatform.NewPlatform) return "NewPlatform";
```
