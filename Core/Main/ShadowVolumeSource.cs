// Shadow Volumes Toolkit
// Copyright 2013 Gustav Olsson
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Light))]
public class ShadowVolumeSource : MonoBehaviour
{
	private static string colorPropertyName = "_shadowVolumeColor";
	private static string sourcePropertyName = "_shadowVolumeSource";
	private static string extrudeBiasPropertyName = "_shadowVolumeExtrudeBias";
	private static string extrudeAmountPropertyName = "_shadowVolumeExtrudeAmount";
	
	[SerializeField]
	private Color shadowColor = new Color(0.0f, 0.0f, 0.0f, 0.5f);
	
	[SerializeField]
	private float extrudeBias = 0.03f;
	
	[SerializeField]
	private float extrudeAmount = 100.0f;
	
	/// <summary>
	/// Gets or sets the shadow color used to determine the color and strength of the shadow volumes controlled by this light. The RGB channels decide the color while the A channel controls the shadow strength. The final screen color will be linearly interpolated from the original scene color to the shadow color using the shadow strength value.
	/// </summary>
	public Color ShadowColor
	{
		get { return shadowColor; }
		set { shadowColor = value; }
	}
	
	/// <summary>
	/// Gets or sets the extrude bias used to offset the shadow volumes in the light direction. When no extrude bias is used the shadow volume is rendered at the same location as the shadow caster and z-fighting occurs.
	/// </summary>
	public float ExtrudeBias
	{
		get { return extrudeBias; }
		set { extrudeBias = value; }
	}
	
	/// <summary>
	/// Gets or sets the extrude amount used to decide how far the shadow volumes will reach from the original mesh.
	/// </summary>
	public float ExtrudeAmount
	{
		get { return extrudeAmount; }
		set { extrudeAmount = value; }
	}
	
	public void Update()
	{
		// Set light properties
		Vector4 source;
		
		if (GetComponent<Light>().type == LightType.Directional)
		{
			Vector3 direction = -GetComponent<Light>().transform.forward;
			
			source = new Vector4(direction.x, direction.y, direction.z, 0.0f);
		}
		else
		{
			Vector3 position = GetComponent<Light>().transform.position;
			
			source = new Vector4(position.x, position.y, position.z, 1.0f);
		}
		
		// Shadow Volume properties
		Shader.SetGlobalVector(sourcePropertyName, source);
		Shader.SetGlobalFloat(extrudeBiasPropertyName, extrudeBias);
		Shader.SetGlobalFloat(extrudeAmountPropertyName, extrudeAmount);
		
		// Renderer properties
		Shader.SetGlobalColor(colorPropertyName, shadowColor);
	}
}