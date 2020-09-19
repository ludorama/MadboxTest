using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightDirection : MonoBehaviour
{
	



	void Start()
	{
		Shader.SetGlobalVector("_LightVector", transform.forward);
	}

	void Update()
	{
		
	}
}
