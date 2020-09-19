using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrackManager : MonoBehaviour
{
	static public TrackManager Instance;

	public Color LineColor = Color.yellow;

	public Transform[] TrackPoints = new Transform[0];


	void Awake()
	{
		Instance = this;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = LineColor;
		for(int i = 0; i < TrackPoints.Length; i++)
		{
			if(i < TrackPoints.Length - 1)
				Gizmos.DrawLine(TrackPoints[i].position, TrackPoints[i + 1].position);
			Gizmos.DrawSphere(TrackPoints[i].position, .3f);
		}
	}

	readonly private float _ROTATING_DISTANCE = 1f;
	public Vector3 GetTrackOrientation(Transform runnerTransf, ref int refIndex)
	{
		Vector3 orientation = Vector3.zero;
		// If we're at the last point, there's no new orientation. Move forward
		if (refIndex == TrackPoints.Length - 2)
			return orientation;
		// It's a very generic system: lerping towards next direction based on proximity.
		// No time to build a proper spline-based movement, sorry
		Vector3 pos = TrackPoints[refIndex + 1].position;
		pos.y = runnerTransf.position.y;
		float distToPoint = Vector3.Distance(runnerTransf.position, pos);
		// If we're midway, keep forward
		if (distToPoint > _ROTATING_DISTANCE)
			return orientation;
		else
		{
			// Close enough. Rotate
			Vector3 oldDdir = (TrackPoints[refIndex + 1].position - TrackPoints[refIndex].position).normalized;
			Vector3 newDir = (TrackPoints[refIndex + 2].position - TrackPoints[refIndex +1].position).normalized;
			float factor = 1f - Mathf.Clamp01(distToPoint / _ROTATING_DISTANCE);
			if(factor > .5f)
			{
				factor = 1f;
				refIndex++;
			}
			orientation = Vector3.Lerp(oldDdir, newDir, factor);
		}
		return orientation;
	}

	public bool ReachedDestination(Vector3 runnerPosition)
	{
		Vector3 pos = TrackPoints[TrackPoints.Length -1].position;
		pos.y = transform.position.y;
		float dist = Vector3.Distance(runnerPosition, pos);
		return dist <= 1f;
	}
}
