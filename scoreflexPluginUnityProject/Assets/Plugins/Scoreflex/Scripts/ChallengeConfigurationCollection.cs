using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChallengeConfigurationCollection : ScoreflexDictionary<ChallengeConfiguration, ChallengeConfigurationCollection> {

	protected override string collectionRootNodeName() {
		return "challengeConfigs";
	}
	
	protected override ChallengeConfiguration factory(Dictionary<string, object> entryDefinition) {
		return new ChallengeConfiguration(entryDefinition);
	}

	public ChallengeConfigurationCollection(Dictionary<string, object> requestParams) : base("/challenges/configs", requestParams) { 

	}

	public ChallengeConfigurationCollection(string challengeId, Dictionary<string, object> requestParams) : base("/challenges/configs"+challengeId, requestParams) { 
		
	}

	public static void findAll(Dictionary<string, object> customParameters, Scoreflex.ModelCallback<ChallengeConfigurationCollection> callback) { 
		ChallengeConfigurationCollection collection = new ChallengeConfigurationCollection(customParameters);
		collection.loadNext(10, callback);
	}

	public static void findAll(Scoreflex.ModelCallback<ChallengeConfigurationCollection> callback) { 
		findAll(null, callback);
	}

	public static void findByChallengeId(string challengeId, Dictionary<string, object> customParameters, Scoreflex.ModelCallback<ChallengeConfigurationCollection> callback) {
		ChallengeConfigurationCollection collection = new ChallengeConfigurationCollection(challengeId, customParameters);
		collection.loadNext(10, callback);
	}
}
