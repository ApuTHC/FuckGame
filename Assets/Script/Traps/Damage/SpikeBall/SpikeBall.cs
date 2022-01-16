using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class SpikeBall : MonoBehaviour
{
    public Transform spikeBall;
    public GameObject chain;
	public float rightLimit;
	public float leftLimit;
	public float speed;

	private Rigidbody2D rb2d;

	void Start()
    {
		rb2d = GetComponent<Rigidbody2D>();
		rb2d.angularVelocity = 500;

		float distanceY = Mathf.Abs(transform.position.y) - Mathf.Abs(spikeBall.position.y);
		Vector3 corregirPos = new Vector3(transform.position.x, transform.position.y, 0f);
		GameObject chains = Instantiate(chain, corregirPos, Quaternion.identity);
		int aux = Mathf.RoundToInt(distanceY / 0.25f);
		for (int i = 1; i <= aux; i++)
		{
			Vector3 corregirPos1 = new Vector3(transform.position.x , transform.position.y - (i * 0.25f), 0f);
			GameObject chainschilds = Instantiate(chain, corregirPos1, Quaternion.identity);
			chainschilds.transform.parent = transform;
		}

	}

    // Update is called once per frame
    void FixedUpdate()
    {
		Move();
	}

	void Move()
    {
        if (transform.rotation.z < rightLimit && rb2d.angularVelocity > 0 && rb2d.angularVelocity < speed)
        {
			rb2d.angularVelocity = speed;
        }
        else if (transform.rotation.z > leftLimit && rb2d.angularVelocity < 0 && rb2d.angularVelocity > -speed)
        {
			rb2d.angularVelocity = -speed;
		}
    }
}
