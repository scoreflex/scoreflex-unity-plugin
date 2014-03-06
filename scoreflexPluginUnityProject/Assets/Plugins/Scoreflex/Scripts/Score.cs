using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Score {

	Dictionary<string, object>	_jsonValues;

	public string formattedCompact { 
		get { return (string)DictionaryUtils.getValueForKey(_jsonValues, "formattedCompact", null);}
	}

	public string deviceModel {
		get { return (string) DictionaryUtils.getValueForKey(_jsonValues,"deviceModel", null);}
	}

	public long score { 
		get { return (long) DictionaryUtils.getValueForKey(_jsonValues,"score", (long)-1);}
	}

	public string formattedLong { 
		get {return (string) DictionaryUtils.getValueForKey(_jsonValues, "formattedLong", null);}
	}

	public string devicePlatform { 
		get {return (string) DictionaryUtils.getValueForKey(_jsonValues, "devicePlatform", null);}
	}

	public long time {
		get {return (long) DictionaryUtils.getValueForKey(_jsonValues, "time", (long) -1);}
	}

	public Score(Dictionary<string, object> score) { 
		_jsonValues = score;
	}

}
