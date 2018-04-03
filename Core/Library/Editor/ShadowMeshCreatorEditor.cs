// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
using UnityEngine;
using UnityEditor;

public class ShadowMeshCreatorEditor : EditorWindow
{
	private static string helpText = "Please select a reference mesh below to continue. The reference mesh needs to be closed (each edge belongs to exactly two triangles) to avoid shadow artifacts. The reference mesh is usually the same as the shadow caster mesh but it is also possible to specify a low polygon version of the shadow caster mesh to gain performance. The local bounds of the shadow mesh will be extended by the bounds margin value in order to render the shadow even when the game object is outside the view frustum. If the shadows disappear when they should not, increase the bounds margin and recreate the mesh.";
	
	private float boundsMargin = 2.0f;
	
	private Mesh referenceMesh;
	
	[MenuItem("Window/Shadow Volumes Toolkit/Shadow Mesh Creator")]
	public static void ShowWindow()
	{
		EditorWindow window = EditorWindow.GetWindow<ShadowMeshCreatorEditor>();
		
		window.title = "Shadow Mesh Creator";
	}
	
	public void OnGUI()
	{
		// Show initial GUI components
		EditorGUILayout.HelpBox(helpText, MessageType.Info);
		
		boundsMargin = EditorGUILayout.FloatField("Bounds Margin", boundsMargin);
		
		referenceMesh = (Mesh)EditorGUILayout.ObjectField("Reference Mesh", referenceMesh, typeof(Mesh), false);
		
		if (referenceMesh != null)
		{
			string referencePath = AssetDatabase.GetAssetPath(referenceMesh);
			string shadowPath = ShadowAssetCreator.ConstructAssetPath(referenceMesh);
			
			// Show additional GUI components
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Reference Mesh location:");
			EditorGUILayout.LabelField(referencePath);
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Shadow Mesh location:");
			EditorGUILayout.LabelField(shadowPath);
			EditorGUILayout.EndHorizontal();
			
			if (GUILayout.Button("Create shadow mesh"))
			{
				ShadowAssetCreator.CreateAsset(referenceMesh, boundsMargin, shadowPath);
			}
		}
	}
}