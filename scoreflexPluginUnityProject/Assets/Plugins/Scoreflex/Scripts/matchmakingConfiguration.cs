using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatchmakingConfiguration {

	public PlayerFilter must { 
		get;
		private set;
	}

	public PlayerFilter mustNot { 
		get;
		private set;
	}

	public PlayerFilter should { 
		get;
		private set;
	}

	public PlayerFilter shouldNot { 
		get;
		private set;
	}

	public MatchmakingConfiguration(Dictionary<string, object> values) { 
		object value = null;

		values.TryGetValue("must", out value); 
		if (value != null) { 
			must = new PlayerFilter((Dictionary<string, object>)value);
		}

		values.TryGetValue("mustNot", out value); 
		if (value != null) { 
			mustNot = new PlayerFilter((Dictionary<string, object>)value);
		}

		values.TryGetValue("should", out value); 
		if (value != null) { 
			should = new PlayerFilter((Dictionary<string, object>)value);
		}

		values.TryGetValue("shouldNot", out value); 
		if (value != null) { 
			shouldNot = new PlayerFilter((Dictionary<string, object>)value);
		}

	}
}
