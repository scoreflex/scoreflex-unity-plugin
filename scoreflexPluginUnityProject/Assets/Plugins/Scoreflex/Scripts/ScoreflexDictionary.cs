using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ScoreflexDictionary<entryType, dictionaryType> where dictionaryType :  ScoreflexDictionary<entryType, dictionaryType> {
	string _next;
	string _previous;
	string _resource;
	protected Dictionary<string, entryType> _items;
	protected Dictionary<string, object> _requestParams;
	
	protected abstract string collectionRootNodeName();
	
	protected abstract entryType factory(Dictionary<string, object> entryDefinition);

	public entryType this[string index] { 
		get { 
			return _items[index];
		}
	}
		
	public Dictionary<string, entryType> getEntries() { 
		return _items;
	}
	
	public ScoreflexDictionary(string resource, Dictionary<string, object> requestParams)  {
		_resource = resource;
		_requestParams = requestParams;
	}
	
	public bool hasNext() { 
		return _next != null;
	}
	
	public bool hasPrevious() { 
		return _previous != null;
	}

	protected virtual void parseResult(Dictionary<string, object> result) {
		string rootKeyName = collectionRootNodeName();
		Dictionary<string,object> results = result;
		if (rootKeyName != null) {
			results = (Dictionary<string, object>)result[rootKeyName];
		}
		
		if (_items == null) { 
			_items = new Dictionary<string, entryType>();
			// set the prev cursor only on first request
		}

		foreach (KeyValuePair<string, object> keyValue in results) { 
			Debug.Log ("key:" + keyValue.Key);
			_items[keyValue.Key] = factory( (Dictionary<string, object>) keyValue.Value);
		}

		return ;

	}

	public void loadNext(int count, Scoreflex.ModelCallback<dictionaryType> callback) { 
		Dictionary<string, object> parameters =  null;
		if (_requestParams == null) { 
			parameters = new Dictionary<string,object>();
		} else {
			parameters = new Dictionary<string, object>(_requestParams);
		}
		if (_next != null) {
			parameters["next"] = _next;
		}
		parameters["count"] = count;
		
		Scoreflex.Get(_resource, parameters, (bool success, Dictionary<string, object> result) => {
			if (success == false) { 
				callback(false, (dictionaryType)this);
				return;
			}
			bool mustSetPrevious = false;
			if (_items == null) {
				mustSetPrevious = true;
			}

			parseResult(result);

			if (mustSetPrevious) {
				if (result.ContainsKey("prev")) { 
					_previous = (string)result["prev"];
				}
			}	
			
			_next = null;
			if (result.ContainsKey("next")) { 
				_next = (string)result["next"];
			}
			
			callback(success, (dictionaryType)this);
		});
	}
	
	public void loadPrevious(int count, Scoreflex.ModelCallback<dictionaryType> callback) { 
		Dictionary<string, object> parameters =  null;
		if (_requestParams == null) { 
			parameters = new Dictionary<string,object>();
		} else {
			new Dictionary<string, object>(_requestParams);
		}

		if (_previous != null) { 
			parameters["prev"] = _previous;
		}
		
		Scoreflex.Get(_resource, parameters, (bool success, Dictionary<string, object> result) => {
			if (success == false) { 
				callback(false, (dictionaryType)this);
				return;
			}
			bool mustSetNext = false;
			if (_items == null) { 
				mustSetNext = true;
			}

			parseResult(result);

			if (mustSetNext && result.ContainsKey("next")) { 
				_next = (string)result["next"];
			}
						
			_previous = null;
			if (result.ContainsKey("prev")) { 
				_previous = (string)result["prev"];
			}
			
			callback(success, (dictionaryType)this);
		});
	}


}
