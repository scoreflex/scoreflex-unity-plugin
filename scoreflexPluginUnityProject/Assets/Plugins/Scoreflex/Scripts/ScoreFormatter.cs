using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreFormatter {
	protected Dictionary<string, object> _jsonValue;

	public string type {
		get {return (string) DictionaryUtils.getValueForKey(_jsonValue, "type", null);}
	}
	
	public Dictionary<string, object> unit {
		get {return (Dictionary<string, object>) DictionaryUtils.getValueForKey(_jsonValue, "unit", null);}
	}

	public Dictionary<string, object> config {
		get {return (Dictionary<string, object>) DictionaryUtils.getValueForKey(_jsonValue, "config", null);}
	}

	public ScoreFormatter(Dictionary<string, object> value) { 
		_jsonValue = value;
	}
}
