// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShadowVolume))]
public class ShadowVolumeEditor : Editor
{
	protected static string shadowMeshTooltip = "Gets or sets the shadow mesh. The shadow mesh is a special mesh that is used for shadow volume extrusion.";
	protected static string isSimpleTooltip = "Gets or sets whether this shadow volume is simple or not. Simple shadow volumes are less resource intensive than non-simple shadow volumes but do not work when the camera is located inside a volume.";
	protected static string layerTooltip = "Gets or sets the layer to which this shadow volume is rendered.";
	
	public override void OnInspectorGUI()
	{
		ShadowVolume source = (ShadowVolume)target;
		
		source.ShadowMesh = (Mesh)EditorGUILayout.ObjectField(new GUIContent("Shadow Mesh", shadowMeshTooltip), source.ShadowMesh, typeof(Mesh), false);
		
		source.IsSimple = EditorGUILayout.Toggle(new GUIContent("Is Simple", isSimpleTooltip), source.IsSimple);
		
		source.Layer = EditorGUILayout.LayerField(new GUIContent("Layer", layerTooltip), source.Layer);
		
		if (GUI.changed)
		{
			EditorUtility.SetDirty(target);
		}
	}
}