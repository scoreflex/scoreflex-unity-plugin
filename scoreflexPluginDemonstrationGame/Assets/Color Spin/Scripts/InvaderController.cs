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
		Vector3 step = (targetPosition - transform.position).normalized * Time.deltaTime * unitsPerSecond;
		transform.position = transform.position + step;
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
					ScoreModel.instance.hits++;
				}
				else
				{
					ScoreModel.instance.misses++;
				}

				live = false;
			}
		}
	}
}
