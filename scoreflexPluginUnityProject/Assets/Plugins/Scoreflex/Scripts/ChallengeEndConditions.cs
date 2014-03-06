using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChallengeEndConditions {
	protected Dictionary<string, object> _jsonValue;

	public string duration {
		get {return (string) DictionaryUtils.getValueForKey(_jsonValue ,"duration", null);}
	}

	public long scoreToBeat {
		get {return (long) DictionaryUtils.getValueForKey(_jsonValue, "scoreToBeat", -1);}
	}

	public List<object> scoreToBeatLimits {
		get {return (List<object>) DictionaryUtils.getValueForKey(_jsonValue, "scoreToBeatLimits", null);}
	}

	public string maxTimePerPlayer { 
		get {return (string) DictionaryUtils.getValueForKey(_jsonValue,"maxTimePerPlayer",null);}
	}

	public string maxPlayingTimePerPlayer { 
		get {return (string) DictionaryUtils.getValueForKey(_jsonValue, "maxPlayingTimePerPlayer", null);}
	}

	public string maxTurnTimePerPlayer { 
		get {return (string) DictionaryUtils.getValueForKey(_jsonValue, "maxTurnTimePerPlayer", null);}
	}

	public string maxTurnsPerPlayer { 
		get {return (string) DictionaryUtils.getValueForKey(_jsonValue, "maxTurnsPerPlayer", null);}
	}

	public ChallengeEndConditions(Dictionary<string, object> value) { 
		_jsonValue = value;

	}

}
