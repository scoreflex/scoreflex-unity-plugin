using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnConfiguration  {
	protected Dictionary<string, object> _jsonValue;

	public string turnTimeout { 
		get {return (string) DictionaryUtils.getValueForKey(_jsonValue,"turnTimeout", null);}
	}

	public string initialTurnStrategy { 
		get {return (string) DictionaryUtils.getValueForKey(_jsonValue, "initialTurnStrategy", null);}
	}

	public string turnStrategy { 
		get {return (string) DictionaryUtils.getValueForKey(_jsonValue, "turnStrategy", null);}
	}

	public TurnConfiguration(Dictionary<string, object> value) { 
		_jsonValue = value;
	}
}
