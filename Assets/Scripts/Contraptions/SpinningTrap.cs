using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningTrap : MonoBehaviour
{
	public Vector3 RotationSpeed = new Vector3(0f, 120f, 0f);
	public float Delay;
	public bool IsLocalRotation = true;


	void Start()
	{
		StartCoroutine(RotateObject());
	}

	private IEnumerator RotateObject()
	{
		yield return new WaitForSeconds(Delay);

		WaitForEndOfFrame delay = new WaitForEndOfFrame();
		while (true)
		{
			transform.Rotate(RotationSpeed * Time.deltaTime, IsLocalRotation ? Space.Self : Space.World);
			yield return delay;
		}
	}
}
