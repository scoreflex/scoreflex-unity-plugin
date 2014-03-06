using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardCollection : ScoreflexCollections<Leaderboard, LeaderboardCollection> {

	protected override string collectionRootNodeName()
	{
		return "leaderboard";
	}

	protected override Leaderboard factory(Dictionary<string, object> entry) { 
		return new Leaderboard(entry);
	}
	

	public LeaderboardCollection(string id, Dictionary<string, object> requestParameter) : base("/leaderboards/"+id, requestParameter) {

	}

	public LeaderboardCollection(string id) : base("/leaderboards/"+id, null) {

	}

	public static void findLeaderboardById(string id, Dictionary<string, object> requestParameter, Scoreflex.ModelCallback<LeaderboardCollection> callback) { 
		LeaderboardCollection result = new LeaderboardCollection(id, requestParameter);
		result.loadNext(10, callback);
	}

	public static void findLeaderboardById(string id,Scoreflex.ModelCallback<LeaderboardCollection> callback) { 
		findLeaderboardById(id, null, callback);
	}
}


