![](https://img.shields.io/badge/release-v2.1.0-blue)
![](https://img.shields.io/badge/maintained-yes-green)

# MaterialsVR App
Materials VR uses virtual reality to help students visualize molecular structures. Molecules can be selected from a list and moved around by the user. The Google Cardboard version includes gaze input which allows the user to gaze at a button for 2 seconds to interact with it.

Please read our [Privacy Policy](https://www.uillinois.edu/cms/One.aspx?portalId=1324&pageId=1465359) and [Terms of Service](https://www.uillinois.edu/cms/One.aspx?portalId=1324&pageId=1465360).

Check out `CHANGELOG.md` to see what's changed and [our roadmap](https://github.com/EmiCB/MoleculesVRApp/projects/2) to see what we're planning to add!

Uses [Unity 2019.4.26f1 (LTS)](https://unity.com/) and [Joaoborks' Patch to the CardboardXR Plugin](https://github.com/joaoborks/cardboard-xr-plugin/tree/feature/xr_interaction).

This code follows [Google's C# Style Guide](https://google.github.io/styleguide/csharp-style.html) and the [Ramen Unity Style Guide](https://github.com/stillwwater/UnityStyleGuide).

&nbsp;

**Current platforms:** iOS, Android

**Supported VR devices:** Google Cardboard

&nbsp;

[![](https://img.shields.io/badge/-Download_On_PlayStore-default)](https://play.google.com/store/apps/details?id=com.unity3d.MoleculesVRAndroidTest)
[![](https://img.shields.io/badge/-Download_On_AppStore-blue)](https://apps.apple.com/us/app/materials-vr/id1533090685)
[![](https://img.shields.io/badge/-PC_Version_GitHub-orange)](https://github.com/aschleife/MaterialsVR)

&nbsp;

## Features
- Click and drag molecules around the space
- Gaze at a button for 2 seconds to activate it

&nbsp;

# Building The Project
## Android
1. Set the current platform in Unity's build settings to Android and press build

## iOS
1. Set the current platform in Unity's build settings to iOS and press build
2. Open the `.xcodeproj` file in Xcode
3. Click on the Unity-iPhone project in Xcode and go to Signing & Capabilities
4. Set the correct bundle identifier and team

&nbsp;

# Editing The Project
## Unity Project Setup
1. Clone or download the repository
2. Open with the correct Unity version (2019.4.26f1)

## Asset Bundle Building Instructions
[Video Tutorial](https://youtu.be/Dzn0RGZ-Cbs)

1. Export Blender file as a `.fbx`
2. Drag and drop file into the `~MoleculeModels` folder in Unity 
3. Drag and drop model into Unity hierarchy from `~MoleculeModels` folder
4. Drag and drop from the hierarchy into the `~AssetBundles/Molecules` folder to create a new prefab
5. Delete object from hierarchy
6. Select and, in the inspector window at the bottom, set the Asset Bundle field to `molecules`
7. Under `Assets` in the menu bar, click `Build AssetBundles` (it should build to the desktop)
8. Drag and drop the built `MoleculeBundles` asset bundle folder into wherever you will be hosting the bundle

## Development for New Platforms
### BuildAssetBundles.cs
Add your new build target into the build target sub directories array

**Example:**
```csharp
string[] buildTargetSubDirs = { "", "/iOS", "/Android", "/NewPlatform" };
```

Add a build pipeline line to create the bundles

**Example:**
```csharp
BuildPipeline.BuildAssetBundles(assetBundleDirectory + buildTargetSubDirs[3], BuildAssetBundleOptions.None, BuildTarget.NewPlatform);
```

### LoadAssetBundles.cs
Add the new platform into the getAssetBundlePlatformFolder method

**Example:**
```csharp
if (currentPlatform == RuntimePlatform.NewPlatform) return "NewPlatform";
```

&nbsp;

# Acknowledgement
**Author & Maintainer:** [Emi Brown](https://emicb.com/) @EmiCB

[Professor Schleife's Website](http://schleife.matse.illinois.edu/)

We acknowledge contributions by (in alphabetical order) Siddharth Ahuja, Andong Jing, Sean Lin, Qiaoqian Lin,  Zhili Luo, Noah Rebei, Sujay Shah, Zekun Wei, Jinlin Xu, Zhongshen Zeng, and Xusheng Zhang and support from the NCSA SPIN program, the NSF REU INCLUSION (grant No. OAC-1659702), and the NSF CAREER grant No. DMR-1555153 for support.
