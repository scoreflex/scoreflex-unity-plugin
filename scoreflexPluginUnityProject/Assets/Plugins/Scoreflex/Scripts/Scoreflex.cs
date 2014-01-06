using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;

public class Scoreflex : MonoBehaviour
{
	public static Scoreflex Instance { get; private set; }

	public string id;
	public string secret;
	public bool sandbox;
	
	void Awake()
	{
		if(Instance == null)
		{
			scoreflexInitialize(id, secret, sandbox);
			GameObject.DontDestroyOnLoad(gameObject);
			Instance = this;
			scoreflexSetUnityObjectName(gameObject.name);
		}
		else if(Instance != this)
		{
			GameObject.Destroy(gameObject);
		}
	}

	public System.Action SubmitTurnCallback = null;
	public System.Action SubmitScoreCallback = null;

	void HandleSubmitTurn(string figure)
	{
		if(SubmitTurnCallback != null)
		{
			SubmitTurnCallback();
		}
		else
		{
			Debug.Log("Scoreflex: Turn submission completed. No handlers found.");
		}
	}
	
	void HandleSubmitScore(string figure)
	{
		if(SubmitScoreCallback != null)
		{
			SubmitScoreCallback();
		}
		else
		{
			Debug.Log("Scoreflex: Score submission completed. No handlers found.");
		}
	}

	public string GetPlayerId()
	{
		var buffer = new byte[512];
		scoreflexGetPlayerId(buffer, buffer.Length);
		int stringLength = 0;
		while(stringLength < buffer.Length && buffer[stringLength] != '\0') stringLength++;
		string result = System.Text.Encoding.Unicode.GetString(buffer);
		return result;
	}
	
	public float GetPlayingTime()
	{
		return scoreflexGetPlayingTime();
	}

	public void SetDeviceToken(string deviceToken)
	{
		scoreflexSetDeviceToken(deviceToken);
	}

	public void ShowDeveloperGames(string developerId)
	{
		scoreflexShowDeveloperGames(developerId);
	}

	public void ShowDeveloperProfile(string developerId)
	{
		scoreflexShowDeveloperProfile(developerId);
	}

	public void ShowGameDetails(string gameId)
	{
		scoreflexShowGameDetails(gameId);
	}

	public void ShowGamePlayers(string gameId)
	{
		scoreflexShowGamePlayers(gameId);
	}

	public void ShowLeaderboard(string leaderboardId)
	{
		scoreflexShowLeaderboard(leaderboardId);
	}

	public void ShowLeaderboardOverview(string leaderboardId)
	{
		scoreflexShowLeaderboardOverview(leaderboardId);
	}

	public void ShowPlayerChallenges()
	{
		scoreflexShowPlayerChallenges();
	}

	public void ShowPlayerFriends(string playerId = null)
	{
		scoreflexShowPlayerFriends(playerId);
	}

	public void ShowPlayerNewsFeed()
	{
		scoreflexShowPlayerNewsFeed();
	}

	public void ShowPlayerProfile(string playerId = null)
	{
		scoreflexShowPlayerProfile(playerId);
	}
	
	public void ShowPlayerProfileEdit()
	{
		scoreflexShowPlayerProfileEdit();
	}

	public void ShowPlayerRating()
	{
		scoreflexShowPlayerRating();
	}
	
	public void ShowPlayerSettings()
	{
		scoreflexShowPlayerSettings();
	}

	public void ShowRanksPanel(string leaderboardId, int score)
	{
		scoreflexShowRanksPanel(leaderboardId, score);
	}

	public void ShowSearch()
	{
		scoreflexShowSearch();
	}

	public void StartPlayingSession()
	{
		scoreflexStartPlayingSession();
	}

	public void StopPlayingSession()
	{
		scoreflexStopPlayingSession();
	}

	public void SubmitTurn(string challengeInstanceId)
	{
		scoreflexSubmitTurn(challengeInstanceId);
	}

	public void SubmitScore(string leaderboardId, int score)
	{
		scoreflexSubmitScore(leaderboardId, score);
	}

	public void SubmitScoreAndShowRanksPanel(string leaderboardId, int score)
	{
		scoreflexSubmitScoreAndShowRanksPanel(leaderboardId, score);
	}

	public void SubmitTurnAndShowChallengeDetail(string challengeLeaderboardId)
	{
		scoreflexSubmitTurnAndShowChallengeDetail(challengeLeaderboardId);
	}

	#region Imports
	[DllImport ("__Internal", CharSet = CharSet.Unicode)]
	private static extern void scoreflexSetUnityObjectName(string unityObjectName);

	[DllImport ("__Internal", CharSet = CharSet.Unicode)]
	private static extern void scoreflexInitialize(string clientId, string secret, bool sandbox);

	[DllImport ("__Internal")]
	private static extern void scoreflexGetPlayerId(byte[] buffer, int bufferLength);
	
	[DllImport ("__Internal")]
	private static extern float scoreflexGetPlayingTime();

	[DllImport ("__Internal", CharSet = CharSet.Unicode)]
	private static extern void scoreflexSetDeviceToken(string deviceToken);

	[DllImport ("__Internal", CharSet = CharSet.Unicode)]
	private static extern void scoreflexShowDeveloperGames(string developerId);

	[DllImport ("__Internal", CharSet = CharSet.Unicode)]
	private static extern void scoreflexShowDeveloperProfile(string developerId);

	[DllImport ("__Internal", CharSet = CharSet.Unicode)]
	private static extern void scoreflexShowGameDetails(string gameId);

	[DllImport ("__Internal", CharSet = CharSet.Unicode)]
	private static extern void scoreflexShowGamePlayers(string gameId);

	[DllImport ("__Internal", CharSet = CharSet.Unicode)]
	private static extern void scoreflexShowLeaderboard(string leaderboardId);

	[DllImport ("__Internal", CharSet = CharSet.Unicode)]
	private static extern void scoreflexShowLeaderboardOverview(string leaderboardId);

	[DllImport ("__Internal")]
	private static extern void scoreflexShowPlayerChallenges();

	[DllImport ("__Internal", CharSet = CharSet.Unicode)]
	private static extern void scoreflexShowPlayerFriends(string playerId = null);

	[DllImport ("__Internal")]
	private static extern void scoreflexShowPlayerNewsFeed();

	[DllImport ("__Internal", CharSet = CharSet.Unicode)]
	private static extern void scoreflexShowPlayerProfile(string playerId = null);

	[DllImport ("__Internal")]
	private static extern void scoreflexShowPlayerProfileEdit();

	[DllImport ("__Internal")]
	private static extern void scoreflexShowPlayerRating();

	[DllImport ("__Internal")]
	private static extern void scoreflexShowPlayerSettings();

	[DllImport ("__Internal", CharSet = CharSet.Unicode)]
	private static extern void scoreflexShowRanksPanel(string leaderboardId, int score);
	
	[DllImport ("__Internal")]
	private static extern void scoreflexShowSearch();
	
	[DllImport ("__Internal")]
	private static extern void scoreflexStartPlayingSession();
	
	[DllImport ("__Internal")]
	private static extern void scoreflexStopPlayingSession();
	
	[DllImport ("__Internal", CharSet = CharSet.Unicode)]
	private static extern void scoreflexSubmitTurn(string challengeInstanceId);
	
	[DllImport ("__Internal", CharSet = CharSet.Unicode)]
	private static extern void scoreflexSubmitScore(string leaderboardId, int score);

	[DllImport ("__Internal", CharSet = CharSet.Unicode)]
	private static extern void scoreflexSubmitScoreAndShowRanksPanel(string leaderboardId, int score);

	[DllImport ("__Internal", CharSet = CharSet.Unicode)]
	private static extern void scoreflexSubmitTurnAndShowChallengeDetail(string challengeLeaderboardId);
	#endregion
}


































