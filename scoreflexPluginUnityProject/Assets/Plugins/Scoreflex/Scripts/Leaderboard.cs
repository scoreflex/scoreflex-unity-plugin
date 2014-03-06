using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Leaderboard {

	public Player player { 
		get;
		private set;
	}

	public Score score { 
		get;
		private set;
	}

	public long rank { 
		get; 
		private set;
	}

	public static void findById(string leaderboardId, Dictionary<string, object> requestParams, Scoreflex.ModelCallback<Leaderboard> callback) { 
		Scoreflex.Get ("/leaderboards/"+leaderboardId, requestParams, (bool success, Dictionary<string, object> result) => {
			if (success == false) { 
				callback(false, null);
				return;
			}
			callback(success, new Leaderboard(result));
		});
	}

	public static void findById(string leaderboardId, Scoreflex.ModelCallback<Leaderboard> callback) { 
		findById(leaderboardId, callback);
	}

	public Leaderboard(Dictionary<string, object> entry) { 
		player = new Player((Dictionary<string, object>) entry["player"]);
		score = new Score((Dictionary<string, object>) entry["score"]);
		rank = (long)entry["rank"];
	}
}
