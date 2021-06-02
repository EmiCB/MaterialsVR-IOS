# Changelog
All notable changes to this project will be documented in this file.
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

See [our roadmap](https://github.com/EmiCB/MoleculesVRApp/projects/2) to see what we're planning to add!

&nbsp;

## [v1.2.0] - 2021-6-1
### Changed
* Updated to Unity 2019.4.26f1 (LTS)
* Now using [Google's Cardboard XR Plugin](https://github.com/googlevr/cardboard-xr-plugin) instead of GVR (deprecated)
* Now using TextMeshPro fonts instead of default Unity fonts
### Fixed
* Camera violently swinging around (caused by old versions of GVR and Unity with newer OS versions)
### Removed
* Molecule movement mode (will be added back in on a future update, did not work due to update to CardbaordXR plugin)

&nbsp;

## [v1.1.1] - 2020-2-17
### Fixed
* App no longer stops running

&nbsp;

## [v1.1.0] "Official Android Release" - 2020-1-22
### Changed
* User moved back to create a more comfortable distance from UI elements and molecules
### Fixed
* Controls screen can be closed with either the Exit button or the Display Controls button without causing any display issues with the controls pop-up

&nbsp;

## [v1.0.1] - 2019-09-15
### Added
* Status indicators for rotation and movement modes
* Display controls button with Google Cardboard controls screen
* Support for Android devices using Google Cardboard

&nbsp;

## [v1.0.0] - 2019-06-30
### Added
* AssetBundles using Unity Web Request (supports multi-platform development)
* Google Cardboard support (GVR)
* Molecules: Alum_structure, CuZnSnS, FeH20S2(NO7)2, K3Fe(CN)6, KP(HO2)2, La2AI2O6
* Scrollable molecule selection menu with auto-generating buttons
* Action menu with working buttons to toggle rotation and movement mode
