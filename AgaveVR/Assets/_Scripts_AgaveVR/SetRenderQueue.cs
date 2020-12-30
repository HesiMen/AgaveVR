﻿/*
	SetRenderQueue.cs
 
	Sets the RenderQueue of an object's materials on Awake. This will instance
	the materials, so the script won't interfere with other renderers that
	reference the same materials.
*/

using UnityEngine;

[AddComponentMenu("Rendering/SetRenderQueue")]
[ExecuteInEditMode]

public class SetRenderQueue : MonoBehaviour
{

	[SerializeField]
	protected int[] m_queues = new int[] { 3000 };

	protected void Awake()
	{
		SetVals();
	}

	private void OnValidate()
	{
		SetVals();
	}

	private void SetVals()
	{
		Material[] materials = GetComponent<Renderer>().sharedMaterials;
		for (int i = 0; i < materials.Length && i < m_queues.Length; ++i)
		{
			materials[i].renderQueue = m_queues[i];
		}
	}
}