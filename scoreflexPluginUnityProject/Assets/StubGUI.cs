using UnityEngine;
using System.Collections;

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
			Debug.Log("Player ID: " + Scoreflex.Instance.GetPlayerId());
			Debug.Log("Playing Time: " + Scoreflex.Instance.GetPlayingTime());
		}
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
	}

}
