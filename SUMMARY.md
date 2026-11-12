# oculus-virtual-war-room: maintenance context

Read this file first, then SKILL.md and README.md. This context describes the repository as of 2026-11-12.

## Purpose and current source

June 2014 Unity scripts for host/join, shared avatars, scene interactions and text chat in an Oculus planning-room project.

24 unchanged scripts in source/scripts/, excluding the separately withheld SkyboxMesh.cs. NetworkManager uses legacy Unity Network/NetworkView RPC/MasterServer, port 42137; Chat sends text RPCs and player classes serialize state. No recovered microphone/audio implementation supports the historical voice-communication goal.

## Use and validation

No complete Unity project or verified build command. First recover original scenes/assets/settings and compatible Unity/Oculus/controller dependencies. Read README.md before source inspection.

Script inventory/provenance inspection only; no Unity import/build/headset/network/audio runtime or performance test. The November 2026 SVG is a labeled explanatory schematic, not an app screenshot or result; labels now describe motion and text chat.

ProjectVersion/SDK versions, scene hierarchy, prefabs, tags, mappings, models/media and types are missing. Legacy OVRPlayerController, CharacterMotor and MovieTexture dependencies need restoration. SkyboxMesh.cs was withheld pending terms review.

## Documentation maintenance

The 2026-11-12 documentation revision organizes the README around implementation, available source, setup, recorded checks and limits. It preserves project links and clearly labels its November 2026 schematic; no original project screenshot was recovered. No runnable code, source provenance, pinned revisions, licenses or validation results changed in this revision. Recorded earlier tests were not rerun for the prose-only edits.

Preserve original source and authorship. New ports belong under updated/ and require their own scope/validation. Do not infer a blanket license from recovery. Review this file and SKILL.md before every commit and synchronize changes to workflow, commands, validation or status. Check relative links and figure rendering; never describe inventory counts or component tests as full-application validation.

Two actual Claude editorial review rounds on 2026-11-12 covered the README and related changed documentation, including context files, component guides and applicable algorithm/build notes or schematic text. Useful qualifications were checked against source, preserved project records and saved validation logs, then applied. Editorial review adds no runtime coverage.
