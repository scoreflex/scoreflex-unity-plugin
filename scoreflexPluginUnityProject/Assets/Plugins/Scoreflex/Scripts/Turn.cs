using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Turn  {
	Dictionary<string, object> _jsonValue; 

	public long sequence {
		get {return (long) DictionaryUtils.getValueForKey(_jsonValue, "sequence", (long) -1);}
	}

	public List<string> currentPlayers {
		get {return (List<string>) DictionaryUtils.getValueForKey(_jsonValue, "currentPlayers", null);}
	}

	public long startTimestamp {
		get {return (long) DictionaryUtils.getValueForKey(_jsonValue, "startTimestamp", (long) -1);}
	}

	public long expireTimestamp {
		get {return (long) DictionaryUtils.getValueForKey(_jsonValue, "expireTimestamp", (long)-1);}
	}

	public Dictionary<string, object> turnHistory {
		get{return (Dictionary<string, object>) DictionaryUtils.getValueForKey(_jsonValue, "turnHistory", null);}
	}

	public Dictionary<string, object> outcome {
		get{return (Dictionary<string, object>) DictionaryUtils.getValueForKey(_jsonValue, "outcome", null);}
	}
	
	public Turn(Dictionary<string, object> values) { 
		_jsonValue = values;
	}
}
