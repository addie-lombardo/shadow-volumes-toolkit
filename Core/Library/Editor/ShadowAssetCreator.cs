// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
using UnityEngine;
using UnityEditor;

public class ShadowAssetCreator : Object
{
	public static string ConstructAssetPath(Mesh referenceMesh)
	{
		string referencePath = AssetDatabase.GetAssetPath(referenceMesh);
		
		string path;
		
		if (referencePath == string.Empty)
		{
			path = "Assets/";
		}
		else
		{
			path = referencePath;
		}
		
		int subEnd = path.LastIndexOf('/');
		
		if (subEnd == -1)
		{
			subEnd = path.Length;
		}
		
		string desiredPath = path.Substring(0, subEnd) + "/" + referenceMesh.name + "Shadow.asset";
		
		return desiredPath;
	}
	
	public static Mesh CreateAsset(Mesh referenceMesh, float boundsMargin)
	{
		string path = ConstructAssetPath(referenceMesh);
		
		return CreateAsset(referenceMesh, boundsMargin, path);
	}
	
	public static Mesh CreateAsset(Mesh referenceMesh, float boundsMargin, string assetPath)
	{
		Mesh shadowMesh = ShadowMeshCreator.CalculateShadowMesh(referenceMesh, boundsMargin);
		
		if (shadowMesh != null)
		{
			Mesh existingMesh = (Mesh)AssetDatabase.LoadAssetAtPath(assetPath, typeof(Mesh));
			
			if (existingMesh == null)
			{
				// Create new asset
				AssetDatabase.CreateAsset(shadowMesh, assetPath);
				
				Debug.Log(assetPath + " was successfully created!");
				
				return shadowMesh;
			}
			else
			{
				// Update existing asset
				existingMesh.Clear(false);
				existingMesh.vertices = shadowMesh.vertices;
				existingMesh.normals = shadowMesh.normals;
				
				if (shadowMesh.boneWeights.Length > 0)
				{
					existingMesh.boneWeights = shadowMesh.boneWeights;
					existingMesh.bindposes = shadowMesh.bindposes;
				}
				
				existingMesh.triangles = shadowMesh.triangles;
				
				EditorUtility.SetDirty(existingMesh);
				DestroyImmediate(shadowMesh);
				
				Debug.Log(assetPath + " was successfully updated!");
				
				return existingMesh;
			}
		}
		else
		{
			Debug.LogError("A shadow mesh for " + referenceMesh.name + " could not be created because the maximum vertex count was exceeded.");
		}
		
		return null;
	}
}