using UnityEngine;
using System.Collections.Generic;

using System.Runtime.InteropServices;

public class StubGUI : MonoBehaviour
{
	public void Callback(string s)
	{
		Debug.Log("Message from C function: " + s);
	}

	void Start()
	{
		Scoreflex.PlaySoloHandlers = PlaySoloHandler;
	}

	void PlaySoloHandler(string leaderboardId)
	{
		Debug.Log("Received order to play solo on: " + leaderboardId);
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(10, 10, 96, 64), "Show"))
		{
			Scoreflex.ShowPlayerProfile();
		}

		if(GUI.Button(new Rect(10, 84, 96, 64), "Trip Test"))
		{
			var id = Scoreflex.GetPlayerId();
			Debug.Log("Player ID: " + id);
			Debug.Log("Playing Time: " + Scoreflex.GetPlayingTime());
			Scoreflex.Get("/players/"+id, null, (bool success, Dictionary<string,object> result) => {
				Debug.Log("REQUEST EXECUTED");
			});
		}
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
	}

}
