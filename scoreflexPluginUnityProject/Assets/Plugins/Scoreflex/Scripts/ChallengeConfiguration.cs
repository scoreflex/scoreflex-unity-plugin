using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ChallengeConfiguration  {
	protected Dictionary<string, object> _jsonValue;

	public long version { 
		get {return DictionaryUtils.getValueForKey(_jsonValue, "version", (long)-1);}
	}	
	
	public bool replayable { 
		get {return DictionaryUtils.getValueForKey(_jsonValue, "replayable", false);}
	}
	
	public long maxSeedValue {
		get {return DictionaryUtils.getValueForKey(_jsonValue, "maxSeedValue", (long)-1);}
	}

	public ParticipantConfiguration participantConfiguration { 
		get; 
		private set;
	}

	public ChallengeEndConditions challengeEndConditions {
		get;
		private set;
	}

	public PlayerFilter target { 
		get; 
		private set;
	}

	public OutcomeConfiguration outcomeConfiguration { 
		get;
		private set;
	}

	public string getDescription(string locale)  { 
		Dictionary<string, object> displayDescriptions = (Dictionary<string, object>) DictionaryUtils.getValueForKey(_jsonValue, "displayDescription", null);

		if (displayDescriptions == null) { 
			return null;
		}
		object displayDescription = null;
		displayDescriptions.TryGetValue(locale, out displayDescription); 
		
		return (string) displayDescription;
	}

	public string getDisplayName(string locale)  { 
		Dictionary<string, object> displayNames = (Dictionary<string, object>) DictionaryUtils.getValueForKey(_jsonValue, "displayName", null);
		if (displayNames == null) { 
			return null;
		}

		object displayName = null;
		displayNames.TryGetValue(locale, out displayName); 

		return (string) displayName;
	}

	public MatchmakingConfiguration matchmakingConfiguration {
		get;
		private set;
	}

	public Dictionary<string, object> customSettings {
		get {return (Dictionary<string, object>) DictionaryUtils.getValueForKey(_jsonValue, "customSettings", null);}
	}

	public ChallengeConfiguration(Dictionary<string, object> values) { 
		_jsonValue = values;
		object value; 
		values.TryGetValue("participantsConfig", out value); 
		if (value != null) { 
			participantConfiguration = new ParticipantConfiguration((Dictionary<string, object>) value);
		}
		values.TryGetValue("challengeEndConditions", out value); 
		if (value != null) {
			challengeEndConditions = new ChallengeEndConditions((Dictionary<string, object>) values);
		}
		values.TryGetValue("target", out value); 
		if (value != null) {
			target = new PlayerFilter((Dictionary<string, object>) values);
		}
		values.TryGetValue("matchmakingConfig", out value); 
		if (value != null) {
			matchmakingConfiguration = new MatchmakingConfiguration((Dictionary<string, object>) values);
		}
		values.TryGetValue("outcomeConfiguration", out value); 
		if (value != null) {
			outcomeConfiguration = new OutcomeConfiguration((Dictionary<string, object>) values);
		}
	}

}
