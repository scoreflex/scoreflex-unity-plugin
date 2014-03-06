using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticipantConfiguration  {
	protected Dictionary<string, object> _jsonValue;

	public List<object> validParticipantCounts { 
		get {return (List<object>)DictionaryUtils.getValueForKey(_jsonValue, "validParticipantCounts", null);}
	}

	public string improveParticipantCountTimeout { 
		get {return (string) DictionaryUtils.getValueForKey(_jsonValue, "improveParticipantCountTimeout", null);}
	}

	public string invitationTimeout {
		get {return (string) DictionaryUtils.getValueForKey(_jsonValue, "invitationTimeout", null);}
	}


	public ParticipantConfiguration(Dictionary<string, object> value) { 
		_jsonValue = value;
	}
}
