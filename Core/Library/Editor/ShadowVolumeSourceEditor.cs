// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShadowVolumeSource))]
public class ShadowVolumeSourceEditor : Editor
{
	protected static string shadowColorTooltip = "Gets or sets the shadow color used to determine the color and strength of the shadow volumes controlled by this light. The RGB channels decide the color while the A channel controls the shadow strength. The final screen color will be linearly interpolated from the original scene color to the shadow color using the shadow strength value.";
	protected static string extrudeBiasTooltip = "Gets or sets the extrude bias used to offset the shadow volumes in the light direction. When no extrude bias is used the shadow volume is rendered at the same location as the shadow caster and z-fighting occurs.";
	protected static string extrudeAmountTooltip = "Gets or sets the extrude amount used to decide how far the shadow volumes will reach from their origin.";
	
	protected bool showAdvanced = false;
	
	public override void OnInspectorGUI()
	{
		ShadowVolumeSource source = (ShadowVolumeSource)target;
		
		source.ShadowColor = EditorGUILayout.ColorField(new GUIContent("Shadow Color & Strength", shadowColorTooltip), source.ShadowColor);
		
		showAdvanced = EditorGUILayout.Foldout(showAdvanced, "Advanced");
		
		if (showAdvanced)
		{
			source.ExtrudeBias = EditorGUILayout.FloatField(new GUIContent("Extrude Bias", extrudeBiasTooltip), source.ExtrudeBias);
			source.ExtrudeAmount = EditorGUILayout.FloatField(new GUIContent("Extrude Amount", extrudeAmountTooltip), source.ExtrudeAmount);
		}
		
		if (GUI.changed)
		{
			EditorUtility.SetDirty(target);
		}
	}
}