using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public class Player {

	private Dictionary<string,object> _jsonValues;
	private List<Player> _devicePlayers;
	private List<Player> _friends;
	private Geo _geo;

	
	public Player(Dictionary<string,object> player) { 
		_jsonValues = player;

		_devicePlayers = new List<Player>();
		if (player.ContainsKey("devicePlayers")) {
//			Debug.Log("Type " + ((List<object>)player["devicePlayers"])[1]);
			List<object> devicePlayers = (List<object>)player["devicePlayers"];
			int len = devicePlayers.Count;
			for (int i = 0; i < len; i++) {
				Dictionary<string, object> dictonaryPlayer = ((Dictionary<string, object>) devicePlayers[i]);
				
				Player newPlayer = new Player(dictonaryPlayer);
				_devicePlayers.Add(newPlayer);
			}
		}

		_friends = new List<Player>(); 
		if (player.ContainsKey("firends")) { 
			List<object> friends = (List<object>)player["firends"];
			int len = friends.Count;
			for (int i = 0; i < len; i++) {
				Dictionary<string, object> dictonaryPlayer = ((Dictionary<string, object>) friends[i]);
				Player newPlayer = new Player(dictonaryPlayer);
				_friends.Add(newPlayer);
			}
		}

		if (player.ContainsKey("Geo"))  {
			_geo = new Geo((Dictionary<string, object>)player["geo"]);
		}
	}
		
	public int friendsCount {
		get {return (int)DictionaryUtils.getValueForKey(_jsonValues, "friendsCount", -1);}
	}

	public string gameSkill {
		get {return (string)DictionaryUtils.getValueForKey(_jsonValues, "gameSkill", null);}
	}

	public long lastGameActivity {
		get{return (long)DictionaryUtils.getValueForKey(_jsonValues, "gameLastActivity", (long)-1);}
	}

	public string url {
		get {return (string)DictionaryUtils.getValueForKey(_jsonValues, "url", null);}
	}

	public string id { 
		get {return (string)DictionaryUtils.getValueForKey(_jsonValues,"id", null);}
	}

	public List<string> serviceIds {
		get{return (List<string>)DictionaryUtils.getValueForKey(_jsonValues, "serviceIds", null);}
	}

	public string nationality {
		get{return (string) DictionaryUtils.getValueForKey(_jsonValues, "nationality", null);}
	}

	public string nickname { 
		get {return (string) DictionaryUtils.getValueForKey(_jsonValues, "nickName", null);}
	}

	public string avatarUrl { 
		get{return (string) DictionaryUtils.getValueForKey(_jsonValues, "avatarUrl", null);}
	}

	public bool isMe {
		get{return (bool)DictionaryUtils.getValueForKey(_jsonValues, "me", false);}
	}

	public string language {
		get {return (string)DictionaryUtils.getValueForKey(_jsonValues, "language", null);}
	}
	
	 public List<Player> devicePlayers {
		get {return _devicePlayers;}
	 }


	public static void findById(string playerId, Dictionary<string, object> requestParameters, Scoreflex.ModelCallback<Player> callback) { 
		Scoreflex.Get("/players/"+playerId, requestParameters, (bool success, Dictionary<string,object> answer) => {
			if (success == false) { 
				callback(false, null);
				return;
			}
			Player result = new Player(answer);
			callback(success, result);
		});
	}

	public static void findById(string playerId, Scoreflex.ModelCallback<Player> callback) { 
		findById(playerId, null, callback);
	}

	public static void findMe(Dictionary<string, object> requestParameters, Scoreflex.ModelCallback<Player> callback) { 
		Player.findById("me", requestParameters, callback);
	}

	public static void findMe(Scoreflex.ModelCallback<Player> callback) { 
		Player.findById("me", callback);
	 }
	
}
