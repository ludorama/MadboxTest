using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
	public int RunnerIndex;
	public int CurrentTrackPoint;
	public bool MovingForward;

	public float MovementSpeed = 6f;


	protected Rigidbody _rbody;
	private Renderer[] _rend;


	protected virtual void OnCollisionEnter(Collision collision)
	{
		string lname = LayerMask.LayerToName(collision.gameObject.layer);
		switch (lname)
		{
			case "Contraptions":
				Respawn();
				break;
		}
	}



	public void Setup(int newRunnerIndex)
	{
		MoveToTrackpoint();
		RunnerIndex = newRunnerIndex;
		_rbody = GetComponent<Rigidbody>();
		_rend = GetComponentsInChildren<Renderer>();
		LookToTrackPoint();
	}

	protected void Move()
	{
		_rbody.MovePosition(transform.position + transform.forward * MovementSpeed * Time.deltaTime);
	}

	virtual protected void FixedUpdate()
	{
		// No reason to work out of work time
		if (MatchManager.Instance.Status != MatchManager.MatchStatus.Running)
			return;
		// Make sure we're looking at the track
		Vector3 dir = TrackManager.Instance.GetTrackOrientation(transform, ref CurrentTrackPoint);
		if (dir == Vector3.zero)
			LookToTrackPoint();
		else
			transform.rotation = Quaternion.LookRotation(dir);
		// Move if user requested
		if (MovingForward)
			Move();
		// Check our race progress
		if (TrackManager.Instance.ReachedDestination(transform.position))
			MatchManager.Instance.GameEnded();
	}

	protected void LookToTrackPoint()
	{
		Vector3 pos = TrackManager.Instance.TrackPoints[CurrentTrackPoint + 1].position;
		pos.y = transform.position.y;
		transform.LookAt(pos);
	}

	protected void MoveToTrackpoint()
	{
		transform.position = TrackManager.Instance.TrackPoints[CurrentTrackPoint].position + Vector3.up;
	}

	protected void Respawn()
	{
		SetVisiblity(false);
		_rbody.isKinematic = true;
		Debug.Log("OUCH!");
		Invoke("BackToGame", 1f);
	}

	protected void BackToGame()
	{
		MoveToTrackpoint();
		_rbody.isKinematic = false;
		_rbody.Sleep();
		LookToTrackPoint();
		SetVisiblity(true);
	}

	protected void SetVisiblity(bool newStatus)
	{
		foreach (Renderer rnd in _rend)
			rnd.enabled = newStatus;
	}
}
