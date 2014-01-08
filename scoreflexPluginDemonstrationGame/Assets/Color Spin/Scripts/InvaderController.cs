using UnityEngine;
using System.Collections;

public class InvaderController : MonoBehaviour
{
	private new Transform transform;

	public int identity = 0;

	public float unitsPerSecond = 1f;
	public Vector3 targetPosition = Vector3.zero;

	public float sizeFactor = 2f;

	private bool live = true;

	void Start ()
	{
		transform = GetComponent<Transform>();
	}

	void Update ()
	{
		if(GameState.live)
		{
			Vector3 step = (targetPosition - transform.position).normalized * Time.deltaTime * unitsPerSecond;
			transform.position = transform.position + step;
		}
		else
		{
			GameObject.Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(live)
		{
			//Die in the time it takes to traverse one self.
			float size = transform.localScale.magnitude * sizeFactor;
			float deathTime = size / unitsPerSecond;
			GameObject.Destroy(gameObject, deathTime);

			SpinnerSlice slice = other.GetComponent<SpinnerSlice>();

			if(slice != null)
			{
				if(slice.identity == identity)
				{
					GameState.hits++;
				}
				else
				{
					GameState.misses++;
				}

				live = false;
			}
		}
	}
}
