using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OutcomeConfiguration {
	protected Dictionary<string, object> _jsonValue;

	public string showScoresPolicy { 
		get {return (string) DictionaryUtils.getValueForKey(_jsonValue, "showScoresPolicy", null);}
	}

	public string scoreOrder { 
		get {return (string) DictionaryUtils.getValueForKey(_jsonValue, "scoreOrder", null);}
	}

	public bool sameRankScoreEq { 
		get {return (bool) DictionaryUtils.getValueForKey(_jsonValue, "sameRankScoreEq", false);}
	}

	public ScoreFormatter scoreFormatter { 
		get;
		private set;
	}

	public string scoreAggregation { 
		get {return (string) DictionaryUtils.getValueForKey(_jsonValue, "scoreAggregation", null);}
	}
	

	public object winnersCount { 
		get {return _jsonValue["winnersCount"];}
	}

	public OutcomeConfiguration(Dictionary<string, object> value) { 
		_jsonValue = value;
		object objectValue; 
		value.TryGetValue("scoreFormatter", out objectValue);
		if (objectValue != null) { 
			scoreFormatter = new ScoreFormatter((Dictionary<string, object>) objectValue);
		}
	}
}
