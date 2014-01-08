using UnityEngine;
using System.Collections;

public static class GameStateController
{
	const string LeaderboardID = "Scores";

	public static void EndGame()
	{
		if(Scoreflex.Instance.SubmitScoreCallback == null) Scoreflex.Instance.SubmitScoreCallback = Callback;
		Scoreflex.Instance.SubmitScoreAndShowRanksPanel(LeaderboardID, GameState.points);
		GameState.live = false;
	}

	static void Callback(bool b)
	{
		Debug.Log("Got Callback: " + b);
	}

	public static void NewGame()
	{
		GameState.live = true;
		GameState.hits = 0;
		GameState.misses = 0;
	}
}
