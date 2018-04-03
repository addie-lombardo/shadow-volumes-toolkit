# Shadow Volumes Toolkit
# Copyright 2012 Gustav Olsson
# http://gustavolsson.squarespace.com/shadow-volumes-toolkit/

Please visits the website for video tutorials and answers to frequently asked questions.

#
# Important
#

- All components have inspector tooltips

- If you're targeting mobile platforms, make sure to enable a 32bit display buffer in the build settings

- If you're targeting platforms that don't reliably support the BlendOp shader function (such as the Android platform), use the NoBlendOp compatibility mode for all ShadowVolume, SkinnedShadowVolume and ShadowVolumeRenderer components

#
# For optimal performance
#

- Enable the IsSimple property on the ShadowVolume and SkinnedShadowVolume components. Simple shadow volumes will look identical to non-simple ones as long as the camera is located outside all volumes. Simple shadow volumes should be used in games where it is known that the camera is outside all volumes at all times. (For example games with a top-down/side view or a constrained camera)

- Use the Standard compatibility mode for all ShadowVolume, SkinnedShadowVolume and ShadowVolumeRenderer components

#
# Quick scene setup
#

1. Place the "Shadow Renderer" prefab in the scene

2. Place the "Light" prefab in the scene

3. Click on "Window->Shadow Volumes Toolkit->Quick Shadow Setup" to open up the quick setup dialog. Dock it somewhere in the editor for easy access.

4. Select a game object and then click the "Setup shadow" button in the quick setup dialog

#
# Manual scene setup
#

1. Click on "Window->Shadow Volumes Toolkit->Shadow Mesh Creator" to open up the mesh creator dialog. Dock it somewhere in the editor for easy access. Select a reference mesh and click on the "Create Shadow Mesh" button to create a shadow mesh asset.

2. Drag & drop the ShadowVolumeRenderer script onto an empty game object (preferably called "Shadow Renderer")

3. Drag & drop the ShadowVolumeSource script onto a light game object

4. For each shadow caster - create an empty game object (preferably called "Shadow") and put it as a child to the shadow caster. Drag & drop the ShadowVolume script onto the child object. Choose which shadow mesh the child object's MeshFilter should use.

See the Basic example scene

5. For each skinned shadow caster - create an empty game object and put the shadow caster object as a child to it. Duplicate the shadow caster (so that it's also a child of the empty game object) and rename it to something useful (preferably "Shadow"). Go through the object tree of the duplicate and do the following:

a) Remove the Animation component
b) Drag & drop the SkinnedShadowVolume script onto the game object with the SkinnedMeshRenderer component
c) Choose which shadow mesh the SkinnedMeshRenderer should use
d) Drag & drop the MimicSkinnedMeshRenderer script onto the game object with the SkinnedMeshRenderer and select the shadow caster's SkinnedMeshRenderer as Reference

See the Animation example scene