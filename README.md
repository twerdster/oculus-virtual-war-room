# Multi-user virtual war-room for Oculus Rift

A shared virtual planning environment for Oculus Rift, with voice communication and synchronized avatar motion.

Students: Noam Peretz, Mor Furshpan.

Supervision: Aaron Wetzler.

## Status

**Original script snapshot recovered. Build and run untested.** `source/scripts/` contains 24 unchanged C# files from `scripts.zip` in the June 2014 final handoff. The companion ZIP contains documents, not a second part of a complete Unity project. This repository is not a runnable game or complete Unity project.

## Running

The scripts use UnityEngine and the historical Unity networking API (`Network`, `NetworkView`/RPC and `MasterServer`), Oculus `OVRPlayerController`, a `CharacterMotor` dependency and `MovieTexture`. No Unity ProjectVersion file, exact Oculus SDK version, complete scene hierarchy or verified build command was present in the script handoff.

`Menu`, `NetworkManager`, `LevelManager` and `UserPlayer` contain the main UI/network/player logic. `NetworkManager` initializes the legacy server on port 42137. The scripts depend on scene-assigned objects, prefabs, input mappings, animation/controllers, tags and missing types; copying them into a new Unity project is not a complete setup procedure. No networking, headset, voice/audio or gameplay behavior was tested.

Scenes, prefabs, models, media, Unity project settings, SDK packages/binaries and unreviewed reports/presentations are not imported. `SkyboxMesh.cs` credits Nora/stereoarts and was withheld because its redistribution terms were not established. No source file was edited or relabeled as newly written code. The original incomplete/commented scripts and historical spelling are preserved.

A runnable recovery would first require the original Unity project/assets, compatible editor/Oculus/camera-controller dependencies and separate license review. No modern editor compatibility or successful build is claimed.

## Links

- [Project entry on Aaron Wetzler's website](https://twerdster.github.io/#multi-user-virtual-war-room-for-oculus-rift)

## Attribution and Updates

Original student credits are preserved. No blanket license has been added; Unity/Oculus and other third-party components retain their own terms. Original authors can open an issue to contribute updates or request corrections or removal of their material.

