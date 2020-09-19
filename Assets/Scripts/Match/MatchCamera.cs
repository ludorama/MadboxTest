using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MatchCamera : MonoBehaviour
{
	public Transform Target;

	public Vector3 Offset = new Vector3(0f, 10f, -10f);
	public float MovementDamp = .5f;
	private Vector3 _destination, _currentPosition, _refDamp;

	public Vector3 CamAngle = new Vector3(-35f, 0f, 0f);
	public float AngleDamp = .5f;
	private Vector3 _desiredDirection, _currentDirection, _refAngleDamp;



	void Start()
	{
		UpdatePositionAndRotation(true);
	}

	void Update()
	{
		UpdatePositionAndRotation(false);
	}

	void LateUpdate()
	{
		transform.position = _currentPosition;
	}

	private void UpdatePositionAndRotation (bool isInstant)
	{
		// Position
		_destination = Target.position +
			(Target.right * Offset.x) +
			(Target.forward * Offset.z);
		_destination.y = Offset.y; // The Y axis is fixed
		if (isInstant)
		{
			_currentPosition = _destination;
			transform.position = _currentPosition;
		}
		else
		{
			_currentPosition = Vector3.SmoothDamp(_currentPosition, _destination, ref _refDamp, MovementDamp);
		}
		// Rotation
		transform.LookAt(Target.position);
	}
}
