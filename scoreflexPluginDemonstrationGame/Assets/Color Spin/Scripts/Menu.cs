using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
	void OnGUI()
	{
		bool concedeGame = false;
		bool startGame = false;
		bool showPlayer = false;

		GUILayout.BeginArea(new Rect(10, 10, 72, 96));
		showPlayer = GUILayout.Button("Profile");
		if(GameState.live && GameState.points > 5)
		{
			concedeGame = GUILayout.Button("Submit");
		}
		else if(!GameState.live)
		{
			startGame = GUILayout.Button("Start");
		}
		GUILayout.EndArea();
				
		if(showPlayer)
		{
			Scoreflex.Instance.ShowPlayerProfile();
		}
		if(concedeGame)
		{
			GameStateController.EndGame();
		}
		if(startGame)
		{
			GameStateController.NewGame();
		}
	}
}
