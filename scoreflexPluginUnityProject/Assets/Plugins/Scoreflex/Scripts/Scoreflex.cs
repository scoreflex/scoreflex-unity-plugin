using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;

public class Scoreflex : MonoBehaviour
{
	public static Scoreflex Instance { get; private set; }

	public string id;
	public string secret;
	public bool sandbox;

	[DllImport ("__Internal", CharSet = CharSet.Ansi)]
	private static extern void scoreflexInitialize(string clientId, string secret, bool sandbox);

	[DllImport ("__Internal")]
	private static extern void scoreflexShowPlayerProfile();

	void Awake()
	{
		if(Instance == null)
		{
			scoreflexInitialize(id, secret, sandbox);
			GameObject.DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if(Instance != this)
		{
			GameObject.Destroy(gameObject);
		}
	}

	public void ShowPlayerProfile()
	{
		scoreflexShowPlayerProfile();
	}
}
