using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Scoreflex exposes via static methods the whole of Scoreflex's behavior.
/// </summary>
public partial class Scoreflex : MonoBehaviour
{
	public string ClientId;
	public string ClientSecret;
	public bool Sandbox;
	public bool Verbose = false;

	public enum Gravity { Default = 0, Bottom = 1, Top = 2 };
	
	/// <summary>
	/// Scoreflex.View instances are associated with panel views; they allow you to close them.
	/// </summary>
	public class View
	{
		public readonly int handle;

		public View(int _handle)
		{
			handle = _handle;
		}
		
		/// <summary>
		/// Close an on-screen panel view.
		/// </summary>
		public void Close()
		{
			#if UNITY_IPHONE
			Scoreflex.scoreflexHidePanelView(handle);
			#elif UNITY_ANDROID
			if(Scoreflex.Instance != null) Scoreflex.Instance._HidePanelView(handle);
			#endif
		}
	}

	private static Scoreflex Instance;

	private bool initialized = false;
	
	/// <summary>
	/// Checks if <see cref="Scoreflex"/> is initialized.
	/// </summary>
	/// <value><c>true</c> if live; otherwise, <c>false</c>.</value>
	public static bool Live {
		get {
			return Instance != null && Instance.initialized;
		}
	}

	private const string ErrorNotLive = "Scoreflex: Method called while not live.";
	
	/// <summary>
	/// Methods added to this delegate will be called when Scoreflex wishes
	/// to start a solo play session. The single argument is the leaderboard ID.
	/// </summary>
	public static System.Action<string> PlaySoloHandlers = null;

	/// <summary>
	/// Methods added to this delegate will be called when the Scoreflex user
	/// wants to play an accepted challenge. Challenges should be handled by
	/// calling up the relevant level and allowing the player to make their move,
	/// followed by a call to Scoreflex.SubmitTurn.
	/// </summary>
	public static System.Action<Dictionary<string,object>> ChallengeHandlers = null;

	private const Gravity SuperDefaultGravity = Gravity.Top;
	public static Gravity DefaultGravity = SuperDefaultGravity;
	
	private static Gravity FilterGravity(Gravity gravity)
	{
		if(gravity == Gravity.Default) gravity = DefaultGravity;
		if(gravity == Gravity.Default) gravity = SuperDefaultGravity;
		return gravity;
	}

	// CALLBACK FACILITY //
	
	public delegate void Callback(bool success, Dictionary<string,object> response);
	
	private readonly Dictionary<string,Callback> CallbackTable = new Dictionary<string,Callback>();
	
	string RegisterCallback(Callback callback)
	{
		string key;
		var random = new System.Random();
		do {
			key = random.Next().ToString();
		} while(CallbackTable.ContainsKey(key));
		
		CallbackTable[key] = callback;
		
		return key;
	}
	
	void HandleCallback(string figure)
	{
		if(figure.Contains(":"))
		{
			bool success = figure.Contains("success");
			string handlerKey = figure.Split(':')[0];
			string jsonString = figure.Substring(handlerKey.Length + ":success:".Length); // :failure is the same length
			
			var dictionary = new Dictionary<string,object>();
			
			try
			{
				if(jsonString.Length > 0)
				{
					var parsed = MiniJSON.Json.Deserialize(jsonString) as Dictionary<string,object>;

					foreach(var kvp in parsed)
					{
						dictionary.Add(kvp.Key, kvp.Value);
					}
				}
			}
			catch(System.Exception ex)
			{
				Debug.LogException(ex);
				Debug.LogError("Scoreflex: Received unparsable JSON code: " + jsonString);
			}
			
			if(CallbackTable.ContainsKey(handlerKey))
			{
				CallbackTable[handlerKey](success, dictionary);
				CallbackTable.Remove(handlerKey);
			}
			else
			{
				Debug.Log("Scoreflex: Received invalid callback code from native library: " + handlerKey);
			}
		}
		else
		{
			Debug.Log("Scoreflex: Received invalid callback code from native library: " + figure);
		}
	}
	
	void HandlePlaySolo(string figure)
	{
		if(PlaySoloHandlers == null)
		{
			Debug.LogError("Scoreflex: Instructed to play solo, but no handlers configured! Please assign to Scoreflex.Instance.PlaySoloHandlers");
		}
		else
		{
			PlaySoloHandlers(figure);
		}
	}
	
	void HandleChallenge(string figure)
	{
		if(ChallengeHandlers == null)
		{
			Debug.LogError("Scoreflex: Received challenge, but found no challenge handler! Please assign to Scoreflex.Instance.ChallengeHandlers");
		}
		else
		{
			var dict = MiniJSON.Json.Deserialize(figure) as Dictionary<string,object>;
			ChallengeHandlers(dict);
		}
	}

	// WRAPPERS //
	
	/// <summary>
	/// Gets the current language used by Scoreflex.
	/// </summary>
	/// <returns>Valid figures are:
	/// af, ar, be,	bg, bn, ca, cs, da, de, el, en, en_GB, en_US,
	/// es, es_ES, es_MX, et, fa, fi, fr, fr_FR, fr_CA,
	/// he, hi, hr, hu, id, is, it, ja, ko, lt, lv,
	/// mk, ms, nb, nl, pa, pl, pt, pt_PT, pt_BR, ro,
	/// ru, sk, sl, sq, sr, sv, sw, ta, th, tl, tr,
	/// uk, vi, zh, zh_CN, zh_TW, zh_HK
	/// </returns>
	public static string GetLanguageCode()
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
			return string.Empty;
		}
		else
			return Instance._GetLanguageCode();
	}
	
	/// <summary>
	/// Sets the Scoreflex language.
	/// </summary>
	/// <param name="languageCode">Language code.
	/// Valid figures are:
	/// af, ar, be,	bg, bn, ca, cs, da, de, el, en, en_GB, en_US,
	/// es, es_ES, es_MX, et, fa, fi, fr, fr_FR, fr_CA,
	/// he, hi, hr, hu, id, is, it, ja, ko, lt, lv,
	/// mk, ms, nb, nl, pa, pl, pt, pt_PT, pt_BR, ro,
	/// ru, sk, sl, sq, sr, sv, sw, ta, th, tl, tr,
	/// uk, vi, zh, zh_CN, zh_TW, zh_HK</param>
	public static void SetLanguageCode(string languageCode)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._SetLanguageCode(languageCode);
	}
	
	/// <summary>
	/// If network is available, preload a view with the specified ressource and
	/// hold a reference on it until the view is shown or freed.
	/// </summary>
	/// <param name="resource">Name of the resource to preload.</param>
	public static void PreloadResource(string resource)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._PreloadResource(resource);
	}
	
	/// <summary>
	/// Frees the preloaded resource.
	/// </summary>
	/// <param name="resource">Name of the preloaded resource.</param>
	public static void FreePreloadedResource(string resource)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._FreePreloadedResource(resource);
	}
	
	/// <summary>
	/// Identifies whether the Scoreflex server is reachable.
	/// </summary>
	/// <value><c>true</c> if the server is reachable; otherwise, <c>false</c>.</value>
	public static bool IsReachable {
		get {
			if(!Live) {
				Debug.Log(ErrorNotLive);
				return false;
			}
			else
				return Instance._IsReachable;
		}
	}
	
	/// <summary>
	/// Gets the player identifier.
	/// </summary>
	/// <returns>The player identifier.</returns>
	public static string GetPlayerId()
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
			return string.Empty;
		}
		else
			return Instance._GetPlayerId();
	}
	
	/// <summary>
	/// Returns the time interval between when <see cref="StartPlayingSession"/> and
	/// <see cref="StopPlayingSession"/> have been called. 
	/// </summary>
	/// <returns>The playing time.</returns>
	public static float GetPlayingTime()
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
			return 0f;
		}
		else
			return Instance._GetPlayingTime();
	}
	
	/// <summary>
	/// Call up a given full screen view.
	/// </summary>
	/// <param name="resource">Scoreflex resource name.</param>
	/// <param name="parameters">Parameters. These will be encoded as JSON and passed up to the server with the REST call.</param>
	public static void ShowFullscreenView(string resource, Dictionary<string,object> parameters = null)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._ShowFullscreenView(resource, parameters);
	}
	
	/// <summary>
	/// Call up a given panel view.
	/// </summary>
	/// <returns>A <see cref="Scoreflex.View"/> instance which can close the panel view at any time.</returns>
	/// <param name="resource">Scoreflex resource name.</param>
	/// <param name="parameters">Parameters. These will be encoded as JSON and passed up to the server with the REST call.</param>
	/// <param name="gravity">Vertical alignment. Can be Scoreflex.Gravity.Top, .Bottom or .Default.</param>
	public static View ShowPanelView(string resource, Dictionary<string,object> parameters = null, Gravity gravity = Gravity.Top)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
			return null;
		}
		else
			return Instance._ShowPanelView(resource, parameters, gravity);
	}
	
	/// <summary>
	/// Sets the device token. This interface is for iOS push notifications.
	/// </summary>
	/// <param name="deviceToken">Device token.</param>
	public static void SetDeviceToken(string deviceToken)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._SetDeviceToken(deviceToken);
	}
	
	/// <summary>
	/// Shows (fullscreen) other games from the given developer.
	/// </summary>
	/// <param name="developerId">Developer identifier.</param>
	/// <param name="parameters">Parameters which will be passed up with the REST call. Optional; can be null.</param>
	public static void ShowDeveloperGames(string developerId, Dictionary<string,object> parameters = null)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._ShowDeveloperGames(developerId, parameters);
	}
	
	/// <summary>
	/// Shows (fullscreen) the developer profile.
	/// </summary>
	/// <param name="developerId">Developer identifier.</param>
	/// <param name="parameters">Parameters which will be passed up with the REST call. Optional; can be null.</param>
	public static void ShowDeveloperProfile(string developerId, Dictionary<string,object> parameters = null)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._ShowDeveloperProfile(developerId, parameters);
	}
	
	/// <summary>
	/// Shows (fullscreen) the game details.
	/// </summary>
	/// <param name="gameId">Game identifier.</param>
	/// <param name="parameters">Parameters which will be passed up with the REST call. Optional; can be null.</param>
	public static void ShowGameDetails(string gameId, Dictionary<string,object> parameters = null)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._ShowGameDetails(gameId, parameters);
	}
	
	/// <summary>
	/// Shows (fullscreen) the game players.
	/// </summary>
	/// <param name="gameId">Game identifier.</param>
	/// <param name="parameters">Parameters which will be passed up with the REST call. Optional; can be null.</param>
	public static void ShowGamePlayers(string gameId, Dictionary<string,object> parameters = null)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._ShowGamePlayers(gameId, parameters);
	}
	
	/// <summary>
	/// Shows (fullscreen) the given leaderboard.
	/// </summary>
	/// <param name="leaderboardId">Leaderboard identifier.</param>
	/// <param name="parameters">Parameters which will be passed up with the REST call. Optional; can be null.</param>
	public static void ShowLeaderboard(string leaderboardId, Dictionary<string,object> parameters = null)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._ShowLeaderboard(leaderboardId, parameters);
	}
	
	/// <summary>
	/// Shows (fullscreen) the leaderboard overview.
	/// </summary>
	/// <param name="leaderboardId">Leaderboard identifier.</param>
	/// <param name="parameters">Parameters which will be passed up with the REST call. Optional; can be null.</param>
	public static void ShowLeaderboardOverview(string leaderboardId, Dictionary<string,object> parameters = null)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._ShowLeaderboardOverview(leaderboardId, parameters);
	}
	
	/// <summary>
	/// Shows (fullscreen) the player challenges.
	/// </summary>
	/// <param name="parameters">Parameters which will be passed up with the REST call. Optional; can be null.</param>
	public static void ShowPlayerChallenges(Dictionary<string,object> parameters = null)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._ShowPlayerChallenges(parameters);
	}
	
	/// <summary>
	/// Shows (fullscreen) the player friends.
	/// </summary>
	/// <param name="playerId">Player identifier.</param>
	/// <param name="parameters">Parameters which will be passed up with the REST call. Optional; can be null.</param>
	public static void ShowPlayerFriends(string playerId = null, Dictionary<string,object> parameters = null)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._ShowPlayerFriends(playerId, parameters);
	}
	
	/// <summary>
	/// Shows (fullscreen) the player's news feed.
	/// </summary>
	/// <param name="parameters">Parameters which will be passed up with the REST call. Optional; can be null.</param>
	public static void ShowPlayerNewsFeed(Dictionary<string,object> parameters = null)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._ShowPlayerNewsFeed(parameters);
	}
	
	/// <summary>
	/// Shows (fullscreen) the player profile.
	/// </summary>
	/// <param name="playerId">Player identifier.</param>
	/// <param name="parameters">Parameters which will be passed up with the REST call. Optional; can be null.</param>
	public static void ShowPlayerProfile(string playerId = null, Dictionary<string,object> parameters = null)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._ShowPlayerProfile(playerId, parameters);
	}
	
	/// <summary>
	/// Shows (fullscreen) the player profile edit.
	/// </summary>
	/// <param name="parameters">Parameters which will be passed up with the REST call. Optional; can be null.</param>
	public static void ShowPlayerProfileEdit(Dictionary<string,object> parameters = null)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._ShowPlayerProfileEdit(parameters);
	}
	
	/// <summary>
	/// Shows (fullscreen) the player rating.
	/// </summary>
	/// <param name="parameters">Parameters which will be passed up with the REST call. Optional; can be null.</param>
	public static void ShowPlayerRating(Dictionary<string,object> parameters = null)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._ShowPlayerRating(parameters);
	}
	
	/// <summary>
	/// Shows (fullscreen) the player settings.
	/// </summary>
	/// <param name="parameters">Parameters which will be passed up with the REST call. Optional; can be null.</param>
	public static void ShowPlayerSettings(Dictionary<string,object> parameters = null)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._ShowPlayerSettings(parameters);
	}
	
	/// <summary>
	/// Shows (fullscreen) the search.
	/// </summary>
	/// <param name="parameters">Parameters which will be passed up with the REST call. Optional; can be null.</param>
	public static void ShowSearch(Dictionary<string,object> parameters = null)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._ShowSearch(parameters);
	}
	
	/// <summary>
	/// Show the ranks panel immediately with a given score. The score will not be sent up to the server. Call this in
	/// conjunction with SubmitScore which will asynchronously push the score up to the server.
	/// </summary>
	/// <param name="leaderboardId">Leaderboard identifier.</param>
	/// <param name="score">Score.</param>
	/// <param name="parameters">Parameters which will be passed up with the REST call. Optional; can be null.</param>
	/// <param name="gravity">Vertical alignment. Can be Scoreflex.Gravity.Top, .Bottom or .Default.</param>
	public static void ShowRanksPanel(string leaderboardId, long score, Dictionary<string,object> parameters = null, Gravity gravity = Gravity.Top)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._ShowRanksPanel(leaderboardId, score, parameters, gravity);
	}
	
	/// <summary>
	/// Hide the ranks panel if it is on screen.
	/// </summary>
	public static void HideRanksPanel()
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._HideRanksPanel();
	}

	/// <summary>
	/// Start a play session.
	/// </summary>
	public static void StartPlayingSession()
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._StartPlayingSession();
	}

	/// <summary>
	/// Stops timing the play session.
	/// </summary>
	public static void StopPlayingSession()
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._StopPlayingSession();
	}

	/// <summary>
	/// A GET request to the Scoreflex server.
	/// </summary>
	/// <param name="resource">The Scoreflex resource.</param>
	/// <param name="parameters">Parameters passed up with the REST query.</param>
	/// <param name="callback">The response handler.</param>
	public static void Get(string resource, Dictionary<string,object> parameters, Callback callback)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
			callback(false, new Dictionary<string,object>());
		}
		else
			Instance._Get(resource, parameters, callback);
	}
	
	/// <summary>
	/// A PUT request to the Scoreflex server.
	/// </summary>
	/// <param name="resource">The Scoreflex resource.</param>
	/// <param name="parameters">Parameters passed up with the REST query.</param>
	/// <param name="callback">The response handler.</param>
	public static void Put(string resource, Dictionary<string,object> parameters, Callback callback)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
			callback(false, new Dictionary<string,object>());
		}
		else
			Instance._Put(resource, parameters, callback);
	}
	
	/// <summary>
	/// A POST request to the Scoreflex server.
	/// </summary>
	/// <param name="resource">The Scoreflex resource.</param>
	/// <param name="parameters">Parameters passed up with the REST query.</param>
	/// <param name="callback">The response handler.</param>
	public static void Post(string resource, Dictionary<string,object> parameters, Callback callback)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
			callback(false, new Dictionary<string,object>());
		}
		else
			Instance._Post(resource, parameters, callback);
	}
	
	/// <summary>
	/// An HTTP POST request that is guaranteed to be executed when a network
	/// connection is present, surviving application reboot. The responseHandler
	/// will be called only if the network is present when the request is first run.
	/// </summary>
	/// <param name="resource">The Scoreflex resource.</param>
	/// <param name="parameters">Parameters passed up with the REST query.</param>
	/// <param name="callback">The response handler.</param>
	public static void PostEventually(string resource, Dictionary<string,object> parameters, Callback callback)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
			callback(false, new Dictionary<string,object>());
		}
		else
			Instance._PostEventually(resource, parameters, callback);
	}
	
	/// <summary>
	/// A DELETE request to the Scoreflex server.
	/// </summary>
	/// <param name="resource">The Scoreflex resource.</param>
	/// <param name="parameters">Parameters passed up with the REST query.</param>
	/// <param name="callback">The response handler.</param>
	public static void Delete(string resource, Dictionary<string,object> parameters, Callback callback)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
			callback(false, new Dictionary<string,object>());
		}
		else
			Instance._Delete(resource, parameters, callback);
	}

	/// <summary>
	/// A helper method that submits a turn to a challenge instance.
	/// </summary>
	/// <param name="challengeInstanceId">Challenge instance identifier.</param>
	/// <param name="score">Score.</param>
	/// <param name="parameters">Additional parameters which will be passed up with the REST call. Optional; may be null.</param>
	/// <param name="callback">The response handler.</param>
	public static void SubmitTurn(string challengeInstanceId, long score, Dictionary<string,object> parameters = null, Callback callback = null)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
			if(callback != null) callback(false, new Dictionary<string,object>());
		}
		else
			Instance._SubmitTurn(challengeInstanceId, score, parameters, callback);
	}

	/// <summary>
	/// A helper method that submits a score to a leaderboard.
	/// </summary>
	/// <param name="leaderboardId">Leaderboard identifier.</param>
	/// <param name="score">Score.</param>
	/// <param name="parameters">Additional parameters which will be passed up with the REST call. Optional; may be null.</param>
	/// <param name="callback">The response handler.</param>
	public static void SubmitScore(string leaderboardId, long score, Dictionary<string,object> parameters = null, Callback callback = null)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
			if(callback != null) callback(false, new Dictionary<string,object>());
		}
		else
			Instance._SubmitScore(leaderboardId, score, parameters, callback);
	}

	/// <summary>
	/// A helper method which submits a score to the server and immediately brings up a ranks panel view
	/// which can be dismissed with <see cref="HideRanksPanel"/>.
	/// </summary>
	/// <param name="leaderboardId">Leaderboard identifier.</param>
	/// <param name="score">Score.</param>
	/// <param name="parameters">Additional parameters which will be passed up with the REST call. Optional; may be null.</param>
	/// <param name="gravity">Vertical alignment. Can be Scoreflex.Gravity.Top, .Bottom or .Default.</param>
	public static void SubmitScoreAndShowRanksPanel(string leaderboardId, long score, Dictionary<string,object> parameters = null, Gravity gravity = Gravity.Top)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._SubmitScoreAndShowRanksPanel(leaderboardId, score, parameters, gravity);
	}
	
	/// <summary>
	/// A helper method which submits a turn to the server and, once the call is successful, brings up the
	/// full screen challenge detail view.
	/// </summary>
	/// <param name="challengeInstanceId">Challenge instance identifier.</param>
	/// <param name="score">Score.</param>
	/// <param name="parameters">Additional parameters which will be passed up with the REST call. Optional; may be null.</param>
	public static void SubmitTurnAndShowChallengeDetail(string challengeInstanceId, long score, Dictionary<string,object> parameters = null)
	{
		if(!Live) {
			Debug.Log(ErrorNotLive);
		}
		else
			Instance._SubmitTurnAndShowChallengeDetail(challengeInstanceId, score, parameters);
	}

}


































