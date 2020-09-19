using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wreckage : MonoBehaviour
{
	public float SwingAngleMax = 45f;
	public float SwingSpeed = 120f;
	public float SwingDelay = 1f;

	public Transform[] WreckingBalls = new Transform[0];


	void Start()
	{
		for(int i = 0; i < WreckingBalls.Length; i++)
			StartCoroutine(SwingBall(WreckingBalls[i], (float)i));
	}

	private IEnumerator SwingBall(Transform Ball, float multiplier)
	{
		yield return new WaitForSeconds(SwingDelay * multiplier);
		WaitForEndOfFrame delay = new WaitForEndOfFrame();

		float factor = 1f;
		float angle = 0f;
		float destination = SwingAngleMax * factor;
		while (true)
		{
			destination = SwingAngleMax * factor;
			angle = Mathf.MoveTowardsAngle(angle, destination, SwingSpeed * Time.deltaTime);
			if (angle == destination)
				factor *= -1f;
			Ball.localEulerAngles = Vector3.right * angle;

			yield return delay;
		}
	}
}
