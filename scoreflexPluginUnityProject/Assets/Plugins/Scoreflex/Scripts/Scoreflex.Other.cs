﻿using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;

public partial class Scoreflex
{
	#if !UNITY_IPHONE && !UNITY_ANDROID
	void Awake()
	{
		if(Instance == null)
		{
			initialized = false;
			GameObject.DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if(Instance != this)
		{
			GameObject.Destroy(gameObject);
		}
	}

	private string _GetPlayerId()
	{
		Debug.Log(ErrorNotLive);
		return string.Empty;
	}

	private float _GetPlayingTime()
	{
		Debug.Log(ErrorNotLive);
		return 0f;
	}

	private void _ShowFullscreenView(string resource, Dictionary<string,object> parameters = null)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private View _ShowPanelView(string resource, Dictionary<string,object> parameters = null, Gravity gravity = Gravity.Top)
	{
		Debug.Log(ErrorNotLive);
		return null;
	}

	private void _SetDeviceToken(string deviceToken)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _ShowDeveloperGames(string developerId, Dictionary<string,object> parameters = null)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _ShowDeveloperProfile(string developerId, Dictionary<string,object> parameters = null)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _ShowGameDetails(string gameId, Dictionary<string,object> parameters = null)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _ShowGamePlayers(string gameId, Dictionary<string,object> parameters = null)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _ShowLeaderboard(string leaderboardId, Dictionary<string,object> parameters = null)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _ShowLeaderboardOverview(string leaderboardId, Dictionary<string,object> parameters = null)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _ShowPlayerChallenges(Dictionary<string,object> parameters = null)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _ShowPlayerFriends(string playerId = null, Dictionary<string,object> parameters = null)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _ShowPlayerNewsFeed(Dictionary<string,object> parameters = null)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _ShowPlayerProfile(string playerId = null, Dictionary<string,object> parameters = null)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _ShowPlayerProfileEdit(Dictionary<string,object> parameters = null)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _ShowPlayerRating(Dictionary<string,object> parameters = null)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _ShowPlayerSettings(Dictionary<string,object> parameters = null)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _ShowRanksPanel(string leaderboardId, long score, Dictionary<string,object> parameters = null, Gravity gravity = Gravity.Top)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _HideRanksPanel()
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _ShowSearch(Dictionary<string,object> parameters = null)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _StartPlayingSession()
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _StopPlayingSession()
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _Get(string resource, Dictionary<string,object> parameters, Callback callback)
	{
		Debug.Log(ErrorNotLive);
		if(callback != null) callback(false, new Dictionary<string,object>());
		return;
	}

	private void _Put(string resource, Dictionary<string,object> parameters, Callback callback)
	{
		Debug.Log(ErrorNotLive);
		if(callback != null) callback(false, new Dictionary<string,object>());
		return;
	}
	
	private void _Post(string resource, Dictionary<string,object> parameters, Callback callback)
	{
		Debug.Log(ErrorNotLive);
		if(callback != null) callback(false, new Dictionary<string,object>());
		return;
	}

	private void _PostEventually(string resource, Dictionary<string,object> parameters, Callback callback)
	{
		Debug.Log(ErrorNotLive);
		if(callback != null) callback(false, new Dictionary<string,object>());
		return;
	}

	private void _Delete(string resource, Dictionary<string,object> parameters, Callback callback)
	{
		Debug.Log(ErrorNotLive);
		if(callback != null) callback(false, new Dictionary<string,object>());
		return;
	}

	private void _SubmitTurn(string challengeInstanceId, long score, Dictionary<string,object> parameters = null, Callback callback = null)
	{
		Debug.Log(ErrorNotLive);
		if(callback != null) callback(false, new Dictionary<string,object>());
		return;
	}

	private void _SubmitScore(string leaderboardId, long score, Dictionary<string,object> parameters = null, Callback callback = null)
	{
		Debug.Log(ErrorNotLive);
		if(callback != null) callback(false, new Dictionary<string,object>());
		return;
	}

	private void _SubmitScoreAndShowRanksPanel(string leaderboardId, long score, Dictionary<string,object> parameters = null, Gravity gravity = Gravity.Top)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _SubmitTurnAndShowChallengeDetail(string challengeLeaderboardId, long score, Dictionary<string,object> parameters = null)
	{
		Debug.Log(ErrorNotLive);
		return;
	}
	private string _GetLanguageCode()
	{
		Debug.Log(ErrorNotLive);
		return string.Empty;
	}
	
	private void _SetLanguageCode(string languageCode)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _PreloadResource(string resource)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private void _FreePreloadedResource(string resource)
	{
		Debug.Log(ErrorNotLive);
		return;
	}

	private bool _IsReachable {
		get {
			return false;
		}
	}
#endif
}


































