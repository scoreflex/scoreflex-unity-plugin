using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Developer {
	Dictionary<string, object> _jsonValues;

	public string id { 
		get { return  (string) DictionaryUtils.getValueForKey(_jsonValues, "id", null);}
	}

	public string name  {
		get {return (string) DictionaryUtils.getValueForKey(_jsonValues, "name", null);}
	}

	public string website  {
		get {return (string) DictionaryUtils.getValueForKey(_jsonValues, "website", null);}
	}

	public string description  {
		get {return (string) DictionaryUtils.getValueForKey(_jsonValues, "description", null);}
	}

	public Developer(Dictionary<string, object> values) { 
		_jsonValues = values; 
	}

	public static void findById(string id, Dictionary<string, object> customParameter, Scoreflex.ModelCallback<Developer> callback) { 
		Scoreflex.Get("/developers/"+id,customParameter, (bool success, Dictionary<string, object> result) => {
			if (success == false) { 
				callback(false, null);
				return;
			}
			callback(success, new Developer(result));
		});
	}

	public static void findById(string id, Scoreflex.ModelCallback<Developer> callback) { 
		findById(id, null, callback);
	}
}
