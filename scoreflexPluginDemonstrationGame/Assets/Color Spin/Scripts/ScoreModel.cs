using UnityEngine;
using System.Collections;

public class ScoreModel
{
	public readonly static ScoreModel instance = new ScoreModel();

	public int hits = 0;
	public int misses = 0;

	public int points { get { return hits - misses; } }
}
