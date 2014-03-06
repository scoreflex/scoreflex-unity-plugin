using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerFilter  {
	protected Dictionary<string, object> _jsonValue;

	public string lastActivityMin { 
		get {return (string)_jsonValue["lastActivityMin"]; }
	}

	public string lastActivityMax { 
		get {return (string)_jsonValue["lastActivityMax"]; }
	}

	public long gamesCountMin { 
		get {return (long)_jsonValue["gamesCountMin"]; }
	}

	public long gamesCountMax { 
		get {return (long)_jsonValue["gamesCountMax"]; }
	}

	public List<string> games { 
		get {return (List<string>)_jsonValue["games"]; }
	}

	public long friendsCountMin { 
		get {return (long)_jsonValue["friendsCountMin"]; }
	}

	public long friendsCountMax { 
		get {return (long)_jsonValue["friendsCountMax"]; }
	}

	public string memberSinceMin { 
		get {return (string)_jsonValue["memberSinceMin"]; }
	}

	public string memberSinceMax { 
		get {return (string)_jsonValue["memberSinceMax"]; }
	}

	public List<string> gender { 
		get {return (List<string>)_jsonValue["gender"]; }
	}

	public List<string> birthWeekDay { 
		get {return (List<string>)_jsonValue["birthWeekDay"]; }
	}

	public List<object> birthDay { 
		get {return (List<object>)_jsonValue["birthDay"]; }
	}

	public List<object> birthMonth { 
		get {return (List<object>)_jsonValue["birthMonth"]; }
	}

	public List<long> birthYearMin { 
		get {return (List<long>)_jsonValue["birthYearMin"]; }
	}

	public List<long> birthYearMax { 
		get {return (List<long>)_jsonValue["birthYearMax"]; }
	}

	public long birthDateMin { 
		get {return (long)_jsonValue["birthDateMin"]; }
	}

	public long birthDateMax { 
		get {return (long)_jsonValue["birthDateMax"]; }
	}

	public string anniversary { 
		get {return (string)_jsonValue["anniversary"]; }
	}

	public long ageMin { 
		get {return (long)_jsonValue["ageMin"]; }
	}

	public long ageMax { 
		get {return (long)_jsonValue["ageMax"]; }
	}

	public List<string> language { 
		get {return (List<string>)_jsonValue["language"]; }
	}

	public List<string> geo { 
		get {return (List<string>)_jsonValue["geo"]; }
	}

	public List<string> nationality { 
		get {return (List<string>)_jsonValue["nationality"]; }
	}

	public Dictionary<string, object> custom { 
		get {return (Dictionary<string, object>)_jsonValue["custom"]; }
	}
	
	public PlayerFilter(Dictionary<string, object> values) { 
		_jsonValue = values;
	}
}
