using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChallengeInstance {
	protected Dictionary<string, object> _jsonValue;

	public string id { 
		get {return (string)DictionaryUtils.getValueForKey(_jsonValue, "id", null); }
	}

	public string revengeId { 
		get {return (string)DictionaryUtils.getValueForKey(_jsonValue, "revengeId", null); }
	}

	public long lastIndexUpdate { 
		get { return (long) DictionaryUtils.getValueForKey(_jsonValue, "lastIndexUpdate", -1); }
	}

	public Dictionary<string, object> requestIds {
		get {return (Dictionary<string, object>) DictionaryUtils.getValueForKey(_jsonValue, "requestIds", null);}
	}

	public ChallengeConfiguration configuration { 
		get;
		private set;
	}

	public Dictionary<string, object> sharedConfiguration {
		get {return (Dictionary<string, object>) DictionaryUtils.getValueForKey(_jsonValue, "sharedConfig", null);}
	}

	public Dictionary<string, object> invitedPlayers {
		get {return (Dictionary<string, object>) DictionaryUtils.getValueForKey(_jsonValue, "invitedPlayers", null);}
	}


	public Dictionary<string, object> participants {
		get {return (Dictionary<string, object>) DictionaryUtils.getValueForKey(_jsonValue, "participants", null);}
	}

	public string status { 
		get {return (string) DictionaryUtils.getValueForKey(_jsonValue, "status", null);}
	}

	public long startDate { 
		get {return (long) DictionaryUtils.getValueForKey(_jsonValue, "startDate", -1);}
	}

	public long creationDate { 
		get {return (long) DictionaryUtils.getValueForKey(_jsonValue, "creationDate", -1);}
	}	

	public long endDate { 
		get {return (long) DictionaryUtils.getValueForKey(_jsonValue, "endDate", -1);}
	}	

	public long seed { 
		get {return (long) DictionaryUtils.getValueForKey(_jsonValue, "seed", -1);}
	}	

	public ChallengeInstance(Dictionary<string, object> values) { 
		_jsonValue = values;

		object value;
		values.TryGetValue("config", out value);
		if (value != null) { 
			configuration = new ChallengeConfiguration((Dictionary<string, object>)value);
		}
	}


	public static void findById(string id, Scoreflex.ModelCallback<ChallengeInstance> callback) { 
		Dictionary<string, object> param = new Dictionary<string, object>(); 
		param["fields"] = "core,config,turn,turnHistory,outcome";
		Scoreflex.Get("/challenges/instances/"+id, param, (bool success, Dictionary<string, object> result) => {
			if (success == false) { 
				callback(false, null);
				return;
			}
			callback(success, new ChallengeInstance(result));
		});
	}
}
