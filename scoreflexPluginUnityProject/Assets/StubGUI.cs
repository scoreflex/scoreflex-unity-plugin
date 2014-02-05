using UnityEngine;
using System.Collections.Generic;

using System.Runtime.InteropServices;

public class StubGUI : MonoBehaviour
{
	public void Callback(string s)
	{
		Debug.Log("Message from C function: " + s);
	}


	void OnGUI()
	{
		if(GUI.Button(new Rect(10, 10, 96, 48), "Show"))
		{
			Scoreflex.Instance.ShowPlayerProfile();
		}

		if(GUI.Button(new Rect(10, 68, 96, 48), "Trip Test"))
		{
			var id = Scoreflex.Instance.GetPlayerId();
			Debug.Log("Player ID: " + id);
			Debug.Log("Playing Time: " + Scoreflex.Instance.GetPlayingTime());
			Scoreflex.Instance.Get("/players/"+id, null, (bool success, Dictionary<string,object> result) => {
				Debug.Log("REQUEST EXECUTED");
			});
		}
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
	}

}
