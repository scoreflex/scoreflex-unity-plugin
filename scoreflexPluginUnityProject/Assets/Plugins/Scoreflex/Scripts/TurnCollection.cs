using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnCollection : ScoreflexCollections<Turn, TurnCollection> {
	
	protected override string collectionRootNodeName()
	{
		return "turns";
	}
	
	protected override Turn factory(Dictionary<string, object> entry) { 
		return new Turn(entry);
	}
	
	
	public TurnCollection(string instanceId, Dictionary<string, object> requestParams) : base("/challenges/instances/"+instanceId+"/turns", requestParams) {
		
	}

	public static void findByInstanceId(string instanceId, Dictionary<string, object> requestParam, Scoreflex.ModelCallback<TurnCollection> callback) { 
		TurnCollection result = new TurnCollection(instanceId, requestParam);
		result.loadNext(10, callback);
	}
	
	public static void findByInstanceId(string instanceId, Scoreflex.ModelCallback<TurnCollection> callback) { 
		findByInstanceId(instanceId, null, callback);
	}
}
