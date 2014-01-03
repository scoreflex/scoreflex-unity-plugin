using UnityEngine;
using System.Collections;

public class StubGUI : MonoBehaviour
{	
	void OnGUI()
	{
		if(GUI.Button(new Rect(10, 10, 96, 48), "Show"))
		{
			Scoreflex.Instance.ShowPlayerProfile();
		}
	}

}
