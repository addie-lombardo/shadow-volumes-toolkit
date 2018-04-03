// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShadowVolumeRenderer))]
public class ShadowVolumeRendererEditor : Editor
{
	protected static string stencilBufferBlurb = "The Stencil Buffer backend has better performance and device compatibility than the Alpha Channel backend. Unity Pro/iOS Pro/Android Pro required.";
	protected static string alphaChannelBlurb = "The Alpha Channel backend is slower than the Stencil Buffer backend but works with all Unity licenses. A 32bit display buffer is required.";
	protected static string layerTooltip = "Gets or sets the layer this shadow volume renderer is responsible for rendering.";
	
	public override void OnInspectorGUI()
	{
		ShadowVolumeRenderer source = (ShadowVolumeRenderer)target;
		
		source.Backend = (ShadowVolumeBackend)EditorGUILayout.EnumPopup("Backend", source.Backend);
		
		if (source.Backend == ShadowVolumeBackend.StencilBuffer || source.Backend == ShadowVolumeBackend.StencilBufferNoTwoSided)
		{
			EditorGUILayout.HelpBox(stencilBufferBlurb, MessageType.Info);
		}
		else
		{
			EditorGUILayout.HelpBox(alphaChannelBlurb, MessageType.Info);
		}
		
		source.Layer = EditorGUILayout.LayerField(new GUIContent("Layer", layerTooltip), source.Layer);
		
		if (GUI.changed)
		{
			EditorUtility.SetDirty(target);
			
			// Force update all ShadowVolume instances when global settings are changed
			ShadowVolume[] volumes = (ShadowVolume[])GameObject.FindObjectsOfType(typeof(ShadowVolume));
			
			foreach (ShadowVolume volume in volumes)
			{
				// Does not need saving to disk, just update
				volume.Update();
			}
		}
	}
}