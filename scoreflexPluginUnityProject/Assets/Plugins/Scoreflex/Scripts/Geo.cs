using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Geo  {
	protected Dictionary<string, object> _jsonValues;

	public Geo(Dictionary<string, object> geo) { 
		_jsonValues = geo;
	}

	public string id {
		get {return (string)DictionaryUtils.getValueForKey(_jsonValues, "id", null);}
	}

	public string countryCode { 
		get {return (string)DictionaryUtils.getValueForKey(_jsonValues, "countryCode", null);}
	}

	public string title {
		get {return (string)DictionaryUtils.getValueForKey(_jsonValues, "title", null);}
	}

	public string formatted { 
		get {return (string)DictionaryUtils.getValueForKey(_jsonValues, "formatted", null);}
	}

	public string adminLevel {
		get {return (string)DictionaryUtils.getValueForKey(_jsonValues, "adminLevel", null);}
	}
}
