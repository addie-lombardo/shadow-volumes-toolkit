// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShadowVolumeRenderer))]
public class ShadowVolumeRendererEditor : Editor
{
	private static string compatibilityModeTooltip = "Gets or sets the compatibility mode. Use NoBlendOp for platforms that don't reliably support the BlendOp shader function, for example Android. The Standard mode uses less full screen passes and has better performance than the NoBlendOp mode. Always set the same compatibility mode for the ShadowVolumeRenderer, ShadowVolume and SkinnedShadowVolume scripts.";
	
	public override void OnInspectorGUI()
	{
		ShadowVolumeRenderer source = (ShadowVolumeRenderer)target;
		
		// CompatibilityMode
		source.CompatibilityMode = (ShadowCompatibilityMode)EditorGUILayout.EnumPopup(new GUIContent("Compatibility Mode", compatibilityModeTooltip), source.CompatibilityMode);
		
		// Handle change
		if (GUI.changed)
		{
			EditorUtility.SetDirty(target);
		}
	}
}