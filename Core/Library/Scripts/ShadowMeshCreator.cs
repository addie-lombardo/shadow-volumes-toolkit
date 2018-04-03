// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
using System.Collections.Generic;
using UnityEngine;

public class ShadowMeshCreator
{
	// Assume 1 unit equals 1 meter and use a collision cell size of 1cm
	protected const float cellSize = 0.01f;
	
	protected struct Edge
	{
		public Vector3 a;
		public Vector3 b;
		
		public Edge(Vector3 a, Vector3 b)
		{
			this.a = a;
			this.b = b;
		}
		
		public bool SameRobust(Edge other)
		{
			return  (a == other.a && b == other.b) ||
					(a == other.b && b == other.a);
		}
		
		public bool Same(Edge other)
		{
			return  (a.x == other.a.x && a.y == other.a.y && a.z == other.a.z && b.x == other.b.x && b.y == other.b.y && b.z == other.b.z) ||
					(a.x == other.b.x && a.y == other.b.y && a.z == other.b.z && b.x == other.a.x && b.y == other.a.y && b.z == other.a.z);
		}
		
		public int CalculateHashCode()
		{
			int hashA = (int)(a.x / cellSize) * 73856093 ^
						(int)(a.y / cellSize) * 19349663 ^
						(int)(a.z / cellSize) * 83492791;
			int hashB = (int)(b.x / cellSize) * 73856093 ^
						(int)(b.y / cellSize) * 19349663 ^
						(int)(b.z / cellSize) * 83492791;
			
			int min, max;
			
			if (hashA < hashB)
			{
				min = hashA;
				max = hashB;
			}
			else
			{
				min = hashB;
				max = hashA;
			}
			
			return min ^ max;
		}
	}
	
	protected struct EdgeEqualityComparerRobust : IEqualityComparer<Edge>
	{
		public bool Equals(Edge x, Edge y)
		{
			return x.SameRobust(y);
		}
		
		public int GetHashCode(Edge obj)
		{
			return 0;
		}
	}
	
	protected struct EdgeEqualityComparer : IEqualityComparer<Edge>
	{
		public bool Equals(Edge x, Edge y)
		{
			return x.Same(y);
		}
		
		public int GetHashCode(Edge obj)
		{
			return obj.CalculateHashCode();
		}
	}
	
	protected static void AddEdge(IDictionary<Edge, List<int>> edges, Edge edge, int triangleIndex)
	{
		if (!edges.ContainsKey(edge))
		{
			List<int> triangles = new List<int>();
			
			triangles.Add(triangleIndex);
			
			edges.Add(edge, triangles);
		}
		else
		{
			List<int> triangles = edges[edge];
			
			triangles.Add(triangleIndex);
		}
	}
	
	protected static bool NeighborSameWindingOrder(Vector3[] vertices, int[] indices, int triangleA, int triangleB)
	{
		int[] a = new int[3];
		int[] b = new int[3];
		
		a[0] = indices[triangleA * 3 + 0];
		a[1] = indices[triangleA * 3 + 1];
		a[2] = indices[triangleA * 3 + 2];
		
		b[0] = indices[triangleB * 3 + 0];
		b[1] = indices[triangleB * 3 + 1];
		b[2] = indices[triangleB * 3 + 2];
		
		for (int m = 0; m < 3; m++)
		{
			int a0 = a[m];
			int a1 = a[(m + 1) % 3];
			
			for (int n = 0; n < 3; n++)
			{
				int b0 = b[n];
				int b1 = b[(n + 1) % 3];
				
				// Does edge m on triangle A match edge n on triangle B?
				if (vertices[a0] == vertices[b1] &&
					vertices[a1] == vertices[b0])
				{
					return true;
				}
			}
		}
		
		return false;
	}
	
	protected static void CreateDegenerateQuad(Vector3[] vertices, int[] indices, Vector3 vertexA, Vector3 vertexB, int triangleA, int triangleB, ICollection<int> outIndices)
	{
		int[] a = new int[3];
		int[] b = new int[3];
		
		a[0] = indices[triangleA * 3 + 0];
		a[1] = indices[triangleA * 3 + 1];
		a[2] = indices[triangleA * 3 + 2];
		
		b[0] = indices[triangleB * 3 + 0];
		b[1] = indices[triangleB * 3 + 1];
		b[2] = indices[triangleB * 3 + 2];
		
		for (int m = 0; m < 3; m++)
		{
			int a0 = a[m];
			int a1 = a[(m + 1) % 3];
			
			for (int n = 0; n < 3; n++)
			{
				int b0 = b[n];
				int b1 = b[(n + 1) % 3];
				
				// Does edge m on triangle A match edge n on triangle B?
				if (vertices[a0] == vertices[b1] &&
					vertices[a1] == vertices[b0])
				{
					// Was this the sought after edge?
					if ((vertices[a0] == vertexA && vertices[a1] == vertexB) ||
						(vertices[a0] == vertexB && vertices[a1] == vertexA))
					{
						// Create a quad between the two edges
						outIndices.Add(a0);
						outIndices.Add(b1);
						outIndices.Add(a1);
						
						outIndices.Add(a1);
						outIndices.Add(b1);
						outIndices.Add(b0);
						
						return;
					}
				}
			}
		}
		
		Debug.LogError("Could not create degenerate quad!");
	}
	
	public static Mesh CalculateShadowMesh(Mesh reference, float boundsMargin)
	{
		// Reference mesh
		Vector3[] refVertices = reference.vertices;
		BoneWeight[] refBoneWeights = reference.boneWeights;
		int[] refIndices = reference.triangles;
		
		int triangleCount = refIndices.Length / 3;
		bool isSkinned = refBoneWeights.Length > 0;
		
		// Shadow mesh
		Vector3[] vertices = new Vector3[refIndices.Length];
		Vector3[] normals = new Vector3[refIndices.Length];
		BoneWeight[] boneWeights = isSkinned ? new BoneWeight[refIndices.Length] : null;
		int[] indices = new int[refIndices.Length];
		
		// Create vertices and initial indices
		// Note that indices are useless at this stage
		for (int i = 0; i < refIndices.Length; i++)
		{
			vertices[i] = refVertices[refIndices[i]];
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
			for (int i = 0; i < refIndices.Length; i++)
			{
				boneWeights[i] = refBoneWeights[refIndices[i]];
			}
		}
		
		// Build edge map
		IDictionary<Edge, List<int>> edges = new Dictionary<Edge, List<int>>(new EdgeEqualityComparer());
		
		for (int i = 0; i < triangleCount; i++)
		{
			Vector3 t0 = vertices[i * 3 + 0];
			Vector3 t1 = vertices[i * 3 + 1];
			Vector3 t2 = vertices[i * 3 + 2];
			
			AddEdge(edges, new Edge(t0, t1), i);
			AddEdge(edges, new Edge(t1, t2), i);
			AddEdge(edges, new Edge(t2, t0), i);
		}
		
		// Validate edge map
		bool validTwoManifold = true;
		
		foreach (Edge edge in edges.Keys)
		{
			List<int> triangles = edges[edge];
			
			if (triangles.Count != 2 || !NeighborSameWindingOrder(vertices, indices, triangles[0], triangles[1]))
			{
                validTwoManifold = false;
                break;
			}
		}
		
		if (!validTwoManifold)
		{
			// The non-manifold mesh can be visualized as an outer shell. The
			// following code duplicates this outer shell and flips normals
			// to create a new inner shell. The shells are then connected
			// together to form a manifold mesh.
			int vertexOffset = vertices.Length;
			int triangleOffset = triangleCount;

            // Duplicate shell and flip normals
			Vector3[] newVertices = new Vector3[vertices.Length * 2];
			Vector3[] newNormals = new Vector3[vertices.Length * 2];
            BoneWeight[] newBoneWeights = isSkinned ? new BoneWeight[vertices.Length * 2] : null;
            int[] newIndices = new int[vertices.Length * 2];
			
			// Duplicate vertices
            vertices.CopyTo(newVertices, 0);
			vertices.CopyTo(newVertices, vertexOffset);
			
			// Duplicate and flip normals
            normals.CopyTo(newNormals, 0);
			
			for (int i = 0; i < normals.Length; i++)
			{
				newNormals[vertexOffset + i] = -normals[i];
			}
			
			// Duplicate bone weights
            if (isSkinned)
            {
                boneWeights.CopyTo(newBoneWeights, 0);
				boneWeights.CopyTo(newBoneWeights, vertexOffset);
            }
			
			// Duplicate indices and reverse winding order
			// From this point and onward, indices matter
			indices.CopyTo(newIndices, 0);
			
			for (int i = 0; i < triangleCount; i++)
			{
				int index0 = i * 3 + 0;
				int index1 = i * 3 + 1;
				int index2 = i * 3 + 2;
				
				newIndices[vertexOffset + index0] = vertexOffset + index0;
				newIndices[vertexOffset + index1] = vertexOffset + index2;
				newIndices[vertexOffset + index2] = vertexOffset + index1;
			}

            // Create degenerate quads
			List<int> finalIndices = new List<int>(newIndices);
			
			foreach (Edge edge in edges.Keys)
			{
				List<int> triangles = edges[edge];
				
				// Connect triangles on the same shell whenever possible in order to keep stencil buffer overdraw to a minimum
				if (triangles.Count == 2 && NeighborSameWindingOrder(newVertices, newIndices, triangles[0], triangles[1]))
				{
                    // Use a quad to connect the two triangles sharing the edge on the outer and inner shell respectively
					CreateDegenerateQuad(newVertices, newIndices, edge.a, edge.b, triangles[0], triangles[1], finalIndices);
					CreateDegenerateQuad(newVertices, newIndices, edge.a, edge.b, triangleOffset + triangles[0], triangleOffset + triangles[1], finalIndices);
				}
				else
				{
					for (int i = 0; i < triangles.Count; i++)
					{
						// Use a quad to connect the triangle on the outer shell with the triangle on the inner shell
						CreateDegenerateQuad(newVertices, newIndices, edge.a, edge.b, triangles[i], triangleOffset + triangles[i], finalIndices);
					}
				}
			}
			
			vertices = newVertices;
			normals = newNormals;
            boneWeights = newBoneWeights;
			indices = finalIndices.ToArray();
		}
		else
		{
            // Create degenerate quads
			List<int> finalIndices = new List<int>(indices);
			
			foreach (Edge edge in edges.Keys)
			{
				List<int> triangles = edges[edge];
				
				if (triangles.Count == 2)
				{
                    // Use a quad to connect the two triangles sharing the edge
					CreateDegenerateQuad(vertices, indices, edge.a, edge.b, triangles[0], triangles[1], finalIndices);
				}
			}
			
			indices = finalIndices.ToArray();
		}
		
		// Create output mesh
		if (vertices.Length > 65500)
		{
			return null;
		}
		
		Mesh mesh = new Mesh();
		
		mesh.name = reference.name + "Shadow";
		mesh.vertices = vertices;
		mesh.normals = normals;
		
		if (isSkinned)
		{
			mesh.boneWeights = boneWeights;
			mesh.bindposes = reference.bindposes;
		}
		
		mesh.triangles = indices;
		
		// Expand bounds
		Bounds bounds = mesh.bounds;
		
		bounds.Expand(boundsMargin);
		
		mesh.bounds = bounds;
		
		return mesh;
	}
}