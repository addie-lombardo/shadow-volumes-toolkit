// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
using System.Collections.Generic;
using UnityEngine;

public static class ShadowMeshCreator
{
	public static Mesh CalculateShadowMesh(Mesh reference, float boundsMargin)
	{
		Vector3[] referenceVertices = reference.vertices;
		BoneWeight[] referenceBoneWeights = reference.boneWeights;
		int[] referenceIndices = reference.triangles;
		
		bool isSkinned = referenceBoneWeights.Length > 0;
		
		// Allocate space
		Vector3[] vertices = new Vector3[referenceIndices.Length];
		Vector3[] normals = new Vector3[referenceIndices.Length];
		
		BoneWeight[] boneWeights = null;
		
		if (isSkinned)
		{
			boneWeights = new BoneWeight[referenceIndices.Length];
		}
		
		int[] indices = new int[referenceIndices.Length];
		
		int triangleCount = referenceIndices.Length / 3;
		
		// Create vertices and initial indices
		for (int i = 0; i < referenceIndices.Length; i++)
		{
			vertices[i] = referenceVertices[referenceIndices[i]];
			
			indices[i] = i;
		}
		
		// Create normals
		for (int i = 0; i < triangleCount; i++)
		{
			int index0 = i * 3 + 0;
			int index1 = i * 3 + 1;
			int index2 = i * 3 + 2;
			
			Vector3 normal = Vector3.Cross(vertices[index1] - vertices[index0], vertices[index2] - vertices[index0]);
			
			normal.Normalize();
			
			normals[index0] = normal;
			normals[index1] = normal;
			normals[index2] = normal;
		}
		
		// Create bone weights
		if (isSkinned)
		{
			for (int i = 0; i < referenceIndices.Length; i++)
			{
				boneWeights[i] = referenceBoneWeights[referenceIndices[i]];
			}
		}
		
		// Create degenerate edge quads
		List<int> finalIndices = new List<int>(indices);
		
		int[] a = new int[3];
		int[] b = new int[3];
		
		int[] neighborCount = new int[triangleCount];
		
		for (int i = 0; i < triangleCount; i++)
		{
			a[0] = i * 3 + 0;
			a[1] = i * 3 + 1;
			a[2] = i * 3 + 2;
			
			for (int j = i + 1; j < triangleCount; j++)
			{
				b[0] = j * 3 + 0;
				b[1] = j * 3 + 1;
				b[2] = j * 3 + 2;
				
				for (int m = 0; m < 3; m++)
				{
					for (int n = 0; n < 3; n++)
					{
						int a0 = a[m];
						int a1 = a[(m + 1) % 3];
						
						int b0 = b[n];
						int b1 = b[(n + 1) % 3];
						
						// Does edge m on triangle A match edge n on triangle B?
						if (vertices[a0] == vertices[b1] &&
						    vertices[a1] == vertices[b0])
						{
							// Create a quad between the two edges
							finalIndices.Add(a0);
							finalIndices.Add(b1);
							finalIndices.Add(a1);
							
							finalIndices.Add(a1);
							finalIndices.Add(b1);
							finalIndices.Add(b0);
							
							// A neighbor was found
							neighborCount[i]++;
							neighborCount[j]++;
							
							// Stop the edge matching between the triangles
							m = 3;
							break;
						}
					}
				}
			}
		}
		
		// Validate mesh
		for (int i = 0; i < triangleCount; i++)
		{
			if (neighborCount[i] != 3)
			{
				// The mesh is not properly closed (2-manifold)
				return null;
			}
		}
		
		// Create output
		Mesh mesh = new Mesh();
		
		mesh.name = reference.name + "Shadow";
		mesh.vertices = vertices;
		mesh.normals = normals;
		
		if (isSkinned)
		{
			mesh.boneWeights = boneWeights;
			mesh.bindposes = reference.bindposes;
		}
		
		mesh.triangles = finalIndices.ToArray();
		
		// Expand bounds
		Bounds bounds = mesh.bounds;
		
		bounds.Expand(boundsMargin);
		
		mesh.bounds = bounds;
		
		return mesh;
	}
}