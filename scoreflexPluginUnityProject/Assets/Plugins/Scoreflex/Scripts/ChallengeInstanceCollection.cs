using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChallengeInstanceCollection  : ScoreflexDictionary<Dictionary<string, List<ChallengeInstance>>,ChallengeInstanceCollection> {

	public enum ChallengeType {
		invitation, 
		yourTurn,
		othersTurn,
		waitingForStart,
		pendingRequest,
		ended
	}

	protected override string collectionRootNodeName() {
		return null; // we are doing the parsing here no need to return the root node
	}

	protected override Dictionary<string, List<ChallengeInstance>> factory(Dictionary<string,object> result) {
		return null; // we are doing the parsing here no need to return the instanciated
	}


	public Dictionary<string, List<ChallengeInstance>> getChallenges(ChallengeType type) { 
		switch(type) { 
			case ChallengeType.ended: 
				return this["ended"];
				break;

			case ChallengeType.invitation: 
				return this["invitations"];
				break;

			case ChallengeType.othersTurn: 
				return this["othersTurn"];
				break;
	
			case ChallengeType.pendingRequest: 
				return this["pendingRequest"];
				break;

			case ChallengeType.waitingForStart: 
				return this["waitingForStart"];
				break;

			case ChallengeType.yourTurn: 
				return this["yourTurn"];
				break;

		}
		return null;
	}

	protected override void parseResult(Dictionary<string, object> results) {

		if (_items == null) { 
			_items = new Dictionary<string, Dictionary<string, List<ChallengeInstance>>>();
			// set the prev cursor only on first request
		}
		
		foreach (KeyValuePair<string, object> keyValue in results) { 
			Dictionary<string, object> instanceList = (Dictionary<string, object>)keyValue.Value;
			if (!_items.ContainsKey(keyValue.Key)) {
				_items[keyValue.Key] = new Dictionary<string, List<ChallengeInstance>>();
			}
			foreach (KeyValuePair<string, object> instanceNames in instanceList) { 
//				Debug.Log ("value: " + instanceNames.Value);
				List<object> instances = (List<object>) instanceNames.Value;	
				if (!_items[keyValue.Key].ContainsKey(instanceNames.Key)) { 
					_items[keyValue.Key][instanceNames.Key] = new List<ChallengeInstance>();
				}
				foreach (Dictionary<string, object> instance in instances) {

					ChallengeInstance challengeInstance = new ChallengeInstance(instance);
					_items[keyValue.Key][instanceNames.Key].Add(challengeInstance);
				}
			}
		}
		
		return ;

	}

	public ChallengeInstanceCollection() : base("/challenges/instances", null) { 

	}

	public ChallengeInstanceCollection(Dictionary<string,object> requestParameter) : base("/challenges/instances", requestParameter) { 
		
	}

	
	public static void findAll(Dictionary<string,object> requestParameter, Scoreflex.ModelCallback<ChallengeInstanceCollection> callback) { 
		ChallengeInstanceCollection collection = new ChallengeInstanceCollection(requestParameter);
		collection.loadNext(10, callback);
	}

	public static void findAll(Scoreflex.ModelCallback<ChallengeInstanceCollection> callback) { 
		findAll(null, callback);
	}



}
