using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;

public class Scoreflex : MonoBehaviour
{
	public string id;
	public string secret;
	public bool sandbox;

	[DllImport ("__Internal", CharSet = CharSet.Unicode)]
	private static extern void scoreflexInitialize(string clientId, string secret, bool sandbox);

	[DllImport ("__Internal")]
	private static extern void scoreflexShowPlayerProfile();


	// Use this for initialization
	void Start () {
		scoreflexInitialize(id, secret, sandbox);

		scoreflexShowPlayerProfile();
	}

}
