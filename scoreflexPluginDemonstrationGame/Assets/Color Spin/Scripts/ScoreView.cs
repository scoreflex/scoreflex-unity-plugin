using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextMesh))]
public class ScoreView : MonoBehaviour
{
	private TextMesh textMesh;
	public string format = "{0} Points";

	private int lastObservedScore = int.MinValue;

	void Start ()
	{
		textMesh = gameObject.GetComponent<TextMesh>();	
	}
	
	void Update ()
	{
		if(GameState.points != lastObservedScore)
		{
			textMesh.text = string.Format(format, GameState.points);
		}
	}
}
