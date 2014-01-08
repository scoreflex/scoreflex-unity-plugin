using UnityEngine;
using System.Collections;

public static class GameState
{
	public static bool live = true;

	public static int hits = 0;
	public static int misses = 0;

	public static int points { get { return hits - misses; } }
}
