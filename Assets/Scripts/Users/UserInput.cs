using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{

	private RunnerUser _user;


	void Start()
	{
		_user = GetComponent<RunnerUser>();
	}

	void Update()
	{
		_user.MovingForward = Input.GetMouseButton(0);
	}
}
