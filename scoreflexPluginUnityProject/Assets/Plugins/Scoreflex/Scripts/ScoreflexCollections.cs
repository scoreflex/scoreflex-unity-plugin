using UnityEngine;
using System.Collections.Generic;

public abstract class ScoreflexCollections<entryType, collectionType> where collectionType : ScoreflexCollections<entryType, collectionType> {
	string _next;
	string _previous;
	string _resource;
	Dictionary<string, object> _requestParams;
	List<entryType> _items;

	protected abstract string collectionRootNodeName();

	protected abstract entryType factory(Dictionary<string, object> entryDefinition);


	public entryType this[int index] { 
		get { 
			return _items[index];
		}
	}


	public List<entryType> getEntries() { 
		return _items;
	}

	public ScoreflexCollections(string resource, Dictionary<string, object> requestParams)  {
		_resource = resource;
		_requestParams = requestParams;
	}

	public bool hasNext() { 
		return _next != null;
	}

	public bool hasPrevious() { 
		return _previous != null;
	}

	public void loadNext(int count, Scoreflex.ModelCallback<collectionType> callback) { 
		Dictionary<string, object> parameters;
		if (_requestParams == null) {
			parameters = new Dictionary<string, object>();
		} else { 
			parameters = new Dictionary<string, object>(_requestParams);
		}

		if (_next != null) {
			parameters["next"] = _next;
		}
		parameters["count"] = count;

		Scoreflex.Get(_resource, parameters, (bool success, Dictionary<string, object> result) => {
			if (success == false) { 
				callback(false, (collectionType)this);
				return;
			}
			List<object> results = (List<object>)result[collectionRootNodeName()];
			int len = results.Count;

			if (_items == null) { 
				_items = new List<entryType>();
				// set the prev cursor only on first request
				if (result.ContainsKey("prev")) { 
					_previous = (string)result["prev"];
				}
			}

			for (int i = 0; i < len; i++) { 
				Dictionary<string, object> entry = (Dictionary<string, object>) results[i];
				entryType typedEntry = factory(entry);
				_items.Add(typedEntry);
			}



			_next = null;
			if (result.ContainsKey("next")) { 
				_next = (string)result["next"];
			}

			callback(success, (collectionType)this);
		});
	}

	public void loadPrevious(int count, Scoreflex.ModelCallback<collectionType> callback) { 
		Dictionary<string, object> parameters;
		if (_requestParams == null) {
			parameters = new Dictionary<string, object>();
		} else { 
			parameters = new Dictionary<string, object>(_requestParams);
		}

		if (_previous != null) { 
			parameters["prev"] = _previous;
		}

		Scoreflex.Get(_resource, parameters, (bool success, Dictionary<string, object> result) => {
			if (success == false) { 
				callback(false, (collectionType)this);
				return;
			}
			List<object> results = (List<object>)result[collectionRootNodeName()];
			int len = results.Count;

			if (_items == null) { 
				_items = new List<entryType>();
				// set the next cursor only on first request
				if (result.ContainsKey("next")) { 
					_next = (string)result["next"];
				}
			}

			for (int i = len - 1; i >= 0; i--) { 
				Dictionary<string, object> entry = (Dictionary<string, object>) results[i];
				entryType typedEntry = factory(entry);
				_items.Insert(0,typedEntry);
			}
		

			_previous = null;
			if (result.ContainsKey("prev")) { 
				_previous = (string)result["prev"];
			}

			callback(success, (collectionType)this);
		});
	}
}
