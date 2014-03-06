using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreCollection : ScoreflexDictionary<Score, ScoreCollection> {

	string _requestType;
		

	protected override string collectionRootNodeName()
	{
		if (_requestType == "bests") {
			return "bestScores";
		} else if (_requestType == "byLeaderboard") {
			return "ranks";
		}
		return "latestScores";
	}
	
	protected override Score factory(Dictionary<string, object> entry) { 
		return new Score(entry);
	}

	public Score getChallengeScore(string challengeName) { 
		Score value = null;
		getEntries().TryGetValue("!challenges." + challengeName,out value);
		return value;
	}

	public Score getChallengeEloScore() { 
		Score value = null;
		getEntries().TryGetValue("!challenges",out value);
		return value;
	}
	public Score getLeaderboardScore(string leaderboardId) { 
		Score value = null;
		getEntries().TryGetValue(leaderboardId,out value);
		return value;
	}

	public ScoreCollection(string requestType, Dictionary<string, object> requestParams) : base("/scores/"+requestType, requestParams) {
		_requestType = requestType;
	}

	public ScoreCollection(string requestType, string leaderboardId,  Dictionary<string, object> requestParams) : base("/scores/"+leaderboardId+"/ranks", requestParams) {
		_requestType = requestType;
	}

	public static void findBests(int count, Dictionary<string, object> requestParameters,Scoreflex.ModelCallback<ScoreCollection> callback) { 
		ScoreCollection result = new ScoreCollection("bests", requestParameters);
		result.loadNext(count, callback);
	}

	public static void findBests(int count, Scoreflex.ModelCallback<ScoreCollection> callback) { 
		findBests(count, null, callback);
	}

	public static void findLatests(int count, Dictionary<string, object> requestParameters, Scoreflex.ModelCallback<ScoreCollection> callback) { 
		ScoreCollection result = new ScoreCollection("latests", requestParameters);
		result.loadNext(count, callback);
	}

	public static void findLatests(int count, Scoreflex.ModelCallback<ScoreCollection> callback) { 
		findLatests(count, null, callback);
	}

//
//	public static void findByLeaderboardId(int count, string leaderboardId, Dictionary<string, object> requestParameters,  Scoreflex.ModelCallback<ScoreCollection> callback) { 
//		ScoreCollection result = new ScoreCollection("byLeaderboard", leaderboardId, requestParameters);
//		result.loadNext(count, callback);
//	}
//
//	public static void findByLeaderboardId(int count, string leaderboardId, Scoreflex.ModelCallback<ScoreCollection> callback) { 
//		findByLeaderboardId(count, leaderboardId, null, callback);
//	}

}
