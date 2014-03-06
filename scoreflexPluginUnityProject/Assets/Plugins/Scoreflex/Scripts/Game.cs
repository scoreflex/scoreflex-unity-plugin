using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game  {
	Dictionary<string, object> _jsonValues;
	
	public string id { 
		get { return  (string) DictionaryUtils.getValueForKey(_jsonValues, "id", null);}
	}

	public string website  {
		get {return (string) DictionaryUtils.getValueForKey(_jsonValues, "website", null);}
	}

	public string shortDescription {
		get {return (string) DictionaryUtils.getValueForKey(_jsonValues, "shortDescription", null);}
	}

	public List<object> genres { 
		get {return (List<object>) DictionaryUtils.getValueForKey(_jsonValues, "genres", null);}
	}

	public List<object> platforms { 
		get {return (List<object>) DictionaryUtils.getValueForKey(_jsonValues, "platforms", null);}
	}

	public Developer developer { 
		get;
		private set;
	}

	public string description {
		get {return (string) DictionaryUtils.getValueForKey(_jsonValues, "description", null);}
	}

	public string name {
		get {return (string) DictionaryUtils.getValueForKey(_jsonValues, "name", null);}
	}

	public string url {
		get {return (string) DictionaryUtils.getValueForKey(_jsonValues, "url", null);}
	}

	public Game(Dictionary<string, object> values) { 
		_jsonValues = values; 
		object developerObject = null;
		values.TryGetValue("developer", out developerObject);
		if (developerObject != null) { 
			developer = new Developer((Dictionary<string, object>) developerObject);
		}
	}

	public static void findById(string id, Dictionary<string, object> requestParameters, Scoreflex.ModelCallback<Game> callback) { 
		Scoreflex.Get("/games/"+id, requestParameters, (bool success, Dictionary<string, object> result) => {
			if (success == false) {
				callback(false, null);
				return;
			} 

			callback(success, new Game(result));
		}); 
	}

	public static void findById(string id, Scoreflex.ModelCallback<Game> callback) { 
		findById(id, null, callback);
	}
}
