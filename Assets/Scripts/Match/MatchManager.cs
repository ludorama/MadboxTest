using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour
{
	static public MatchManager Instance;

	public enum MatchStatus { Waiting, Running, Ended }
	public MatchStatus Status;//{ get; private set; }


	void Awake()
	{
		Instance = this;
	}

	void Update()
	{
		
	}

	public void GameEnded()
	{
		Status = MatchManager.MatchStatus.Ended;
		Debug.Log("=====  YAY! YOU WON!  =====");
		Invoke("Replay", 3f);
	}

	private void Replay()
	{
		SceneManager.LoadSceneAsync(0);
	}
}
