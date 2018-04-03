// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
using UnityEngine;
using UnityEditor;

public abstract class AbstractShadowVolumeEditor : Editor
{
	private static string compatibilityModeTooltip = "Gets or sets the compatibility mode. Use NoBlendOp for platforms that don't reliably support the BlendOp shader function, for example Android. The Standard mode uses less full screen passes and has better performance than the NoBlendOp mode. Always set the same compatibility mode for the ShadowVolumeRenderer, ShadowVolume and SkinnedShadowVolume scripts.";
	private static string isSimpleTooltip = "Gets or sets whether this shadow volume is simple or not. Simple shadow volumes have better performance than non-simple shadow volumes but do not support the scenario where the camera is inside a volume.";
	
	public override void OnInspectorGUI()
	{
		AbstractShadowVolume source = (AbstractShadowVolume)target;
		
		// CompatibilityMode
		source.CompatibilityMode = (ShadowCompatibilityMode)EditorGUILayout.EnumPopup(new GUIContent("Compatibility Mode", compatibilityModeTooltip), source.CompatibilityMode);
		
		// IsSimple
		source.IsSimple = EditorGUILayout.Toggle(new GUIContent("Is Simple", isSimpleTooltip), source.IsSimple);
		
		// Handle change
		if (GUI.changed)
		{
			EditorUtility.SetDirty(target);
		}
	}
}