using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections;
using System.Text;

public partial class Scoreflex
{
	#if !UNITY_IPHONE && !UNITY_ANDROID

	#region Figures for REST Calls; constants and inferred.
	private string ScoreflexURL {
		get {
			return Sandbox ? "https://sandbox.api.scoreflex.com/" : "https://api.scoreflex.com/";
		}
	}

	private string _State = null;
	private string State {
		get {
			if(_State == null) _State = Random.Range(1000000, 9999999).ToString();
			return _State;
		}
	}

	private string DevicePlatform {
		get {
			return "Android";
		}
	}

	private string DeviceModel {
		get {
			return "WhiskeyTangoFoxtrot";
		}
	}
	#endregion

	void Awake()
	{
		if(Instance == null)
		{
			//GET v1/oauth/web/authorize?clientId=foobar&state=foobar&devicePlatform=iOS&deviceModel=foobar

			AuthorizeAsGuest();

			initialized = false;
			GameObject.DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if(Instance != this)
		{
			GameObject.Destroy(gameObject);
		}
	}

	private void AuthorizeAsGuest()
	{
		_Post("v1/oauth/anonymousAccessToken", new Dictionary<string,object> {
			{ "clientId", ClientId },
		//	{ "clientSecret", ClientSecret },
			{ "devicePlatform", DevicePlatform },
			{ "deviceModel", DeviceModel },
			{ "deviceId", SystemInfo.deviceUniqueIdentifier }
		}, HandleAuthorization);
	}

	private void HandleAuthorization(bool success, Dictionary<string,object> figures)
	{
		var sb = new StringBuilder();
		sb.AppendLine("Received authorization callback. " + success + ": " + (figures == null).ToString());
		if(figures != null)
		{
			foreach(var kvp in figures)
			{
				sb.Append(kvp.Key);
				sb.Append(" = ");
				sb.Append(kvp.Value);
				sb.AppendLine();
			}
		}
		Debug.Log(sb.ToString());
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

	private void printIfVerbose(string s)
	{
		if(Verbose) {
			string text = string.Format("Scoreflex: {0}", s);
			Debug.Log(text);
		}
	}

	private byte[] BuildPost(Dictionary<string,object> parameters)
	{
		WWWForm form = new WWWForm();
		foreach(var kvp in parameters) form.AddField(kvp.Key, kvp.Value.ToString());
		return form.data;
	}

	private string BuildQueryString(string resource, Dictionary<string,object> parameters = null)
	{
		var builder = new StringBuilder();
		builder.Append(ScoreflexURL);
		builder.Append(resource);
		if(parameters != null) {
			bool isFirst = true;
			foreach(var kvp in parameters)
			{
				string prefix = isFirst ? "?" : "&";
				string key = WWW.EscapeURL(kvp.Key);
				string value = WWW.EscapeURL(kvp.Value.ToString());
				builder.Append(prefix);
				builder.Append(key);
				builder.Append("=");
				builder.Append(value);
				isFirst = false;
			}
		}
		return builder.ToString();
	}

	enum Method { Post, Put, Get, Delete };

	private static string SignatureStringProcessor(string s)
	{
		// (ALPHA:"a-zA-Z", DIGIT:"0-9", "-", ".", "_", "~"
		var validCharacters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM0123456789-._~".ToCharArray();
		var encoder = System.Text.Encoding.UTF8;
		var sb = new StringBuilder();
		foreach(char c in s)
		{
			if(System.Array.IndexOf(validCharacters, c) == -1)
			{
				var encoded = encoder.GetBytes(new char[] { c });
				for(int i = 0; i < encoded.Length; i++)
				{
					sb.Append('%');
					sb.Append(encoded[i].ToString("X2"));
				}
			}
			else
			{
				sb.Append(c);
			}
		}
		return sb.ToString();
	}

	private Hashtable BuildRequestHeaders(Method method, string resource, Dictionary<string,object> parameters)
	{
		Hashtable ht = new Hashtable();

//		if(Sandbox)
//		{
//			ht.Add("X-Scoreflex-Lenient", "yes");
//		}
//		else
		if(method != Method.Get)
		{
			var elements = new List<string>();

			// Step 1: add HTTP method uppercase
			elements.Add(method.ToString().ToUpper());
	
			// Step 2: add the URI, without the query string (if one is caught up in there for any reason)
			{
				var indexOfQuestionMark = resource.IndexOf('?');
				bool resourceStringHasParameters = indexOfQuestionMark >= 0;
				string URI;
				if(resourceStringHasParameters) {
					URI = ScoreflexURL + resource;
				}
				else {
					URI = ScoreflexURL + resource.Substring(0, resource.IndexOf('?'));
				}
				elements.Add(URI);
			}

			// Step 3: add URL encoded parameters (sorted by key)
			{
				var indexOfQuestionMark = resource.IndexOf('?');
				bool resourceStringHasParameters = indexOfQuestionMark >= 0;
				if(resourceStringHasParameters)
				{
					string queryString = resource.Substring(indexOfQuestionMark + 1);
					parameters = new Dictionary<string, object>(parameters);
					throw new System.NotImplementedException();
				}
			}

			#warning Could there be parameters placed explicitly into the request? If so, clone the parameters array and merge these figures. Review later.

			string[] keys = new string[parameters.Count];
			parameters.Keys.CopyTo(keys, 0);
			System.Array.Sort(keys);
			var parametersBuilder = new StringBuilder();
			for(int i = 0; i < keys.Length; i++)
			{
				if(i != 0) parametersBuilder.Append('&');
				var key = keys[i];
				var value = parameters[key].ToString();
				parametersBuilder.Append(string.Format("{0}={1}", key, value));
			}
			elements.Add(parametersBuilder.ToString());
		
			// Step 4: add body, which for now is an emtpy string. So says the docs:
			//  Raw body if not Content-Type: application/x-www-form-urlencoded. If Content-Type:
			//  application/x-www-form-urlencoded, consider this part as an empty string.
			elements.Add(string.Empty);

			var sb = new StringBuilder();
			for(int i = 0; i < elements.Count; i++)
			{
				if(i != 0) sb.Append("&");
				sb.Append(SignatureStringProcessor( elements[i] ));
			}

			var clientSecretUTF8 = System.Text.Encoding.UTF8.GetBytes(ClientSecret);

			var encrypter = new System.Security.Cryptography.HMACSHA1(clientSecretUTF8);

			encrypter.Initialize();
			var clearSignature = sb.ToString();
			var clearSignatureUTF8 = System.Text.Encoding.UTF8.GetBytes(clearSignature);
			var digest = encrypter.ComputeHash(clearSignatureUTF8);
			var sig = System.Convert.ToBase64String(digest).Trim();

			sig = SignatureStringProcessor(sig);

			printIfVerbose(clearSignature);
			printIfVerbose(sig);

			ht["X-Scoreflex-Authorization"] = string.Format("Scoreflex sig=\"{0}\", meth=\"0\"", sig);
		}

		return ht;		
	}

	private void _Get(string resource, Dictionary<string,object> parameters, Callback callback)
	{	
		string queryString = BuildQueryString(resource, parameters);
		printIfVerbose("Performing GET request: " + queryString);
		StartCoroutine(CallCoroutine(queryString, null, null, callback));
	}

	private void _Put(string resource, Dictionary<string,object> parameters, Callback callback)
	{
		Debug.Log(ErrorNotLive);
		if(callback != null) callback(false, new Dictionary<string,object>());
		return;
	}

	private void _Post(string resource, Dictionary<string,object> parameters, Callback callback)
	{
		string queryString = BuildQueryString(resource);
		byte[] post = BuildPost(parameters);
		var headers = BuildRequestHeaders(Method.Post, resource, parameters);
		printIfVerbose("Performing POST request: " + queryString);
		StartCoroutine(CallCoroutine(queryString, post, headers, callback));
	}

	IEnumerator CallCoroutine(string url, byte[] post, Hashtable headers, Callback callback)
	{
		var connection = new WWW(url, post, headers);
		
		yield return connection;
		
		if(connection.error != null)
		{
			var error = new StringBuilder();
			error.AppendLine("Scoreflex: call returned an error.");
			error.Append("URL and query string: ");
			error.AppendLine(url);
			if(post != null)
			{
				error.Append("POST: ");
				error.AppendLine(System.Text.Encoding.UTF8.GetString(post));
			}
			error.Append("Error: ");
			error.AppendLine(connection.error);
			Debug.LogError(error);
			if(callback != null) callback(false, new Dictionary<string,object>());
		}
		else
		{
			if(callback != null) {
				printIfVerbose("Scoreflex: call returned text: " + connection.text);
				var response = MiniJSON.Json.Deserialize(connection.text) as Dictionary<string,object>;
				callback(true, response);
			}
		}
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


































