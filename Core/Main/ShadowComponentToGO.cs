// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
using UnityEngine;

/// <summary>
/// This component can be used to enable dynamic batching of shadows. It is a temporary work-around,
/// so do not use it unless you absolutely have to. When Unity implements support for dynamic batching
/// for the Graphics.DrawMesh() call, this script is no longer needed.
/// 
/// Instructions: Attach this component to all game objects with a ShadowVolume component. When the scene
/// starts, the ShadowVolume components will be replaced by new game objects that will handle the rendering
/// of the shadows. When used, this component will not let you change ShadowVolume properties during runtime.
/// </summary>
[RequireComponent(typeof(ShadowVolume))]
public class ShadowComponentToGO : MonoBehaviour
{
    public void Start()
    {
        ShadowVolumeRenderer shadowVolumeRenderer = ShadowVolumeRenderer.Instance;

        if (shadowVolumeRenderer == null)
        {
            return;
        }

        ShadowVolumeBackend backend = shadowVolumeRenderer.Backend;
        ShadowVolume component = GetComponent<ShadowVolume>();

        GameObject shadow = new GameObject("Shadow");

        shadow.layer = component.Layer;

        shadow.transform.parent = gameObject.transform;
        shadow.transform.localPosition = Vector3.zero;
        shadow.transform.localRotation = Quaternion.identity;

        shadow.AddComponent<MeshFilter>().sharedMesh = component.ShadowMesh;

        Material[] materials = null;

        if (backend == ShadowVolumeBackend.StencilBuffer)
        {
            if (component.IsSimple)
            {
                materials = new Material[] { component.stencilFrontBack };
            }
            else
            {
                materials = new Material[] { component.stencilBackFrontAlways, component.stencilFrontBack };
            }
        }
        else if (backend == ShadowVolumeBackend.StencilBufferNoTwoSided)
        {
            if (component.IsSimple)
            {
                materials = new Material[] { component.stencilFront, component.stencilBack };
            }
            else
            {
                materials = new Material[] { component.stencilBackAlways, component.stencilFrontAlways, component.stencilFront, component.stencilBack };
            }
        }
        else if (backend == ShadowVolumeBackend.AlphaChannel)
        {
            if (component.IsSimple)
            {
                materials = new Material[] { component.alphaFront, component.alphaBack };
            }
            else
            {
                materials = new Material[] { component.alphaBackAlways, component.alphaFrontAlways, component.alphaFront, component.alphaBack };
            }
        }
        else if (backend == ShadowVolumeBackend.AlphaChannelNoBlendOp)
        {
            if (component.IsSimple)
            {
                materials = new Material[] { component.alphaFront, component.alphaBackNoBlendOp };
            }
            else
            {
                materials = new Material[] { component.alphaBackAlways, component.alphaFrontAlwaysNoBlendOp, component.alphaFront, component.alphaBackNoBlendOp };
            }
        }

        shadow.AddComponent<MeshRenderer>().sharedMaterials = materials;

        component.enabled = false;
    }
}