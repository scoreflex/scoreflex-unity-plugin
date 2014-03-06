using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChallengeRequest {
	protected Dictionary<string, object> _jsonValue;

	public string id { 
		get {return (string) DictionaryUtils.getValueForKey(_jsonValue, "id", null);}
	}

	public string configId {
		get {return (string) DictionaryUtils.getValueForKey(_jsonValue, "configId", null);}
	}

	public long configVersion { 
		get {return (long) DictionaryUtils.getValueForKey(_jsonValue, "configVersion", (long)-1);}
	}

	public long creationDate { 
		get {return (long) DictionaryUtils.getValueForKey(_jsonValue, "creationDate", (long) -1);}
	}

	public long resolvedDate { 
		get {return (long) DictionaryUtils.getValueForKey(_jsonValue, "resolvedDate", (long) -1);}
	}

	public string autoFillMode {
		get {return (string) DictionaryUtils.getValueForKey(_jsonValue, "autoFillMode", null);}
	}

	public string status {
		get {return (string) DictionaryUtils.getValueForKey(_jsonValue, "status", null);}
	}

	public bool hasInvitations {
		get {return (bool) DictionaryUtils.getValueForKey(_jsonValue, "hasInvitations", false);}
	}

	public string instanceId { 
		get {return (string) DictionaryUtils.getValueForKey(_jsonValue, "instanceId", null);} 
	}

	public Dictionary<string, object> inviterPlayers { 
		get {return (Dictionary<string, object>) DictionaryUtils.getValueForKey(_jsonValue, "invitedPlayer", null);}
	}

	public Dictionary<string, object> sharedConfig { 
		get {return (Dictionary<string, object>) DictionaryUtils.getValueForKey(_jsonValue, "sharedConfig", null);}
	}

	public MatchmakingConfiguration matchmakingConfig {
		get;
		private set;
	}

	public static void findById(string id, Dictionary<string, object> customParameters, Scoreflex.ModelCallback<ChallengeRequest> callback) { 
		Scoreflex.Get("/challenges/requests/"+id, customParameters, (bool success, Dictionary<string, object> result) => {
			if (success == false) { 
				callback(false, null);
				return;
			}
			callback(success, new ChallengeRequest(result));
		});
	}

	public static void findById(string id, Scoreflex.ModelCallback<ChallengeRequest> callback) { 
		findById(id, null, callback);
	}

	public ChallengeRequest(Dictionary<string, object> values) { 
		_jsonValue = values;
		object value; 

		values.TryGetValue("matchmakingConfig", out value); 
		if (value != null) { 

			//TODO: UNCOMENT HERE AFTER VERSION OF SCOREFLEX GETS UPDATED WITH revision:  7f0d8792
//			matchmakingConfig = new MatchmakingConfiguration((Dictionary<string, object>)value);
		}
	}
}
