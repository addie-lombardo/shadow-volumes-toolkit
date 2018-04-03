// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SkinnedShadowVolume))]
public class SkinnedShadowVolumeEditor : Editor
{
	protected static string isSimpleTooltip = "Gets or sets whether this shadow volume is simple or not. Simple shadow volumes are less resource intensive than non-simple shadow volumes but do not work when the camera is located inside a volume.";
	
	public override void OnInspectorGUI()
	{
		SkinnedShadowVolume source = (SkinnedShadowVolume)target;
		
		source.IsSimple = EditorGUILayout.Toggle(new GUIContent("Is Simple", isSimpleTooltip), source.IsSimple);
		
		if (GUI.changed)
		{
			EditorUtility.SetDirty(target);
		}
	}
}