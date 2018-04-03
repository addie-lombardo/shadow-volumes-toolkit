// Shadow Volumes Toolkit
// Copyright 2012 Gustav Olsson
using System.Collections.Generic;
using UnityEngine;

public class MimicSkinnedMeshRenderer : MonoBehaviour
{
	[SerializeField]
	private SkinnedMeshRenderer reference;
	
	private IList<Transform> sortedSourceBones, sortedTargetBones;
	
	/// <summary>
	/// Gets or sets the reference renderer. The renderer that is attached to this game object will mimic the reference renderer.
	/// </summary>
	public SkinnedMeshRenderer Reference
	{
		get { return reference; }
		set { reference = value; }
	}
	
	private void CreateSortedBones(IList<Transform> sourceBones, IList<Transform> targetBones)
	{
		if (sourceBones.Count == targetBones.Count)
		{
			// Initialize bone lists
			int boneCount = sourceBones.Count;
			
			sortedSourceBones = new List<Transform>(boneCount);
			sortedTargetBones = new List<Transform>(boneCount);
			
			// Find root bones
			for (int i = 0; i < boneCount; i++)
			{
				Transform bone = sourceBones[i];
				
				if (!sourceBones.Contains(bone.parent))
				{
					sortedSourceBones.Add(sourceBones[i]);
					sortedTargetBones.Add(targetBones[i]);
				}
			}
			
			// Find child bones
			ExpandBoneTree(sortedSourceBones);
			
			ExpandBoneTree(sortedTargetBones);
		}
	}
	
	private void ExpandBoneTree(IList<Transform> bones)
	{
		int start = 0;
		
		while (start < bones.Count)
		{
			int length = bones.Count;
			
			// Add child bones to the end of the list
			for (int i = start; i < length; i++)
			{
				foreach (Transform child in bones[i])
				{
					bones.Add(child);
				}
			}
			
			start = length;
		}
	}
	
	public void Start()
	{
		if (reference != null)
		{
			CreateSortedBones(reference.bones, GetComponent<SkinnedMeshRenderer>().bones);
		}
	}
	
	public void LateUpdate()
	{
		if (sortedSourceBones != null && sortedTargetBones != null)
		{
			for (int i = 0; i < sortedSourceBones.Count; i++)
			{
				Transform sourceBone = sortedSourceBones[i];
				Transform targetBone = sortedTargetBones[i];
				
				targetBone.position = sourceBone.position;
				targetBone.rotation = sourceBone.rotation;
				targetBone.localScale = sourceBone.localScale;
			}
		}
	}
}