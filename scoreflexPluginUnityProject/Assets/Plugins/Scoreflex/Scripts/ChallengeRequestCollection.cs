using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChallengeRequestCollection :ScoreflexDictionary<ChallengeRequest, ChallengeRequestCollection> {
	
	protected override string collectionRootNodeName() {
		return "requests";
	}
	
	protected override ChallengeRequest factory(Dictionary<string, object> entryDefinition) {
		return new ChallengeRequest(entryDefinition);
	}
	
	public ChallengeRequestCollection(Dictionary<string, object> requestParameters) : base("/challenges/requests", requestParameters) { 
		
	}

	public ChallengeRequestCollection() : base("/challenges/requests", null) { 
		
	}

	public static void findAll(Dictionary<string, object> requestParameters, Scoreflex.ModelCallback<ChallengeRequestCollection> callback) { 
		ChallengeRequestCollection collection = new ChallengeRequestCollection(requestParameters);
		collection.loadNext(10, callback);
	}

	public static void findAll(Scoreflex.ModelCallback<ChallengeRequestCollection> callback) { 
		findAll(null, callback);
	}
}
