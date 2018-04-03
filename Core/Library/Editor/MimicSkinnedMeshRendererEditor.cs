// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MimicSkinnedMeshRenderer))]
public class MimicSkinnedMeshRendererEditor : Editor
{
	private static string referenceTooltip = "Gets or sets the reference renderer. The renderer that is attached to this game object will mimic the reference renderer.";
	
	public override void OnInspectorGUI()
	{
		MimicSkinnedMeshRenderer source = (MimicSkinnedMeshRenderer)target;
		
		// Reference
		source.Reference = (SkinnedMeshRenderer)EditorGUILayout.ObjectField(new GUIContent("Reference", referenceTooltip), source.Reference, typeof(SkinnedMeshRenderer), true);
		
		// Handle change
		if (GUI.changed)
		{
			EditorUtility.SetDirty(target);
		}
	}
}