using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovilePlatform1 : MonoBehaviour
{
	public Transform target;
	public float speed;
	public GameObject chain;
	public bool isRect;
	public bool isCircle;
	public bool isEllipse;
	public float curvature;

	float distanceX;
	float distanceY;

	int aux;
	bool stop = false;
	int iteration = 0;
	bool player;

	private Vector3 start, end;

	// Use this for initialization
	void Start()
	{

		distanceX = Mathf.Abs(target.position.x) - Mathf.Abs(transform.position.x);
		distanceY = Mathf.Abs(target.position.y) - Mathf.Abs(transform.position.y);

		if (isRect)
		{
			if (target != null)
			{
				target.parent = null;
				start = transform.position;
				end = target.position;
			}
			float angle = Mathf.Atan2(distanceY, distanceX);
			float distance = distanceY / Mathf.Sin(angle);
			if (distance == 0f)
			{
				distance = distanceX / Mathf.Cos(angle);
			}
			float increaseY = System.Convert.ToSingle(0.2 * Mathf.Sin(angle));
			float increaseX = System.Convert.ToSingle(0.2 * Mathf.Cos(angle));
			aux = Mathf.RoundToInt(distance / 0.2f);
			Vector3 corregirPos = new Vector3(transform.position.x, transform.position.y, 0f);
			GameObject chains = Instantiate(chain, corregirPos, Quaternion.identity);
			for (int i = 1; i <= aux; i++)
			{
				Vector3 corregirPos1 = new Vector3(transform.position.x + (i * increaseX), transform.position.y + (i * increaseY), 0f);
				GameObject chainschilds = Instantiate(chain, corregirPos1, Quaternion.identity);
				chainschilds.transform.parent = chains.transform;
			}
		}
		if (isCircle)
		{
			target.parent = null;

			distanceX = Mathf.Abs(transform.position.x) - Mathf.Abs(target.position.x);
			aux = Mathf.RoundToInt(distanceX * 2 * Mathf.PI / 0.2f);
			Vector3 corregirPos = new Vector3(transform.position.x, transform.position.y, 0f);
			GameObject chains = Instantiate(chain, corregirPos, Quaternion.identity);
			for (int i = 1; i <= aux; i++)
			{
				float beta = (0.2f * i) / distanceX;
				float x = distanceX * Mathf.Cos(beta);
				float y = distanceX * Mathf.Sin(beta);
				Vector3 corregirPos1 = new Vector3(target.position.x + (x), target.position.y + (y), 0f);
				GameObject chainschilds = Instantiate(chain, corregirPos1, Quaternion.identity);
				chainschilds.transform.parent = chains.transform;
			}
		}
		if (isEllipse)
		{
			target.parent = null;

			distanceX = Mathf.Abs(transform.position.x) - Mathf.Abs(target.position.x);
			//int aux = Mathf.RoundToInt(distanceX * 2 * Mathf.PI / 0.2f);
			Vector3 corregirPos = new Vector3(transform.position.x, transform.position.y, 0f);
			GameObject chains = Instantiate(chain, corregirPos, Quaternion.identity);
			aux = Mathf.RoundToInt(360 / 3.7f);
			for (int i = 1; i <= aux; i++)
			{
				float beta = Mathf.Deg2Rad * 3.7f * i;
				float x = distanceX * Mathf.Cos(beta);
				float y = curvature * Mathf.Sin(beta);
				Vector3 corregirPos1 = new Vector3(target.position.x + (x), target.position.y + (y), 0f);
				GameObject chainschilds = Instantiate(chain, corregirPos1, Quaternion.identity);
				chainschilds.transform.parent = chains.transform;
				beta = 3.7f * i;
				if (distanceX > curvature)
				{
					if (beta < 30f)
					{
						i++;
					}
					if (beta > 150f && beta < 210f)
					{
						i++;
					}
					if (beta > 330f)
					{
						i++;
					}
				}
				if (distanceX < curvature)
				{
					if (beta > 60f && beta < 120f)
					{
						i++;
					}
					if (beta > 240f && beta < 300f)
					{
						i++;
					}
				}
			}
		}
	}

	void FixedUpdate()
	{
		if (isRect)
		{
			if (target != null && player)
			{
				float fixedSpeed = speed * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, target.position, fixedSpeed);
			}
            else if (!player)
            {
				float fixedSpeed = speed * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, start, fixedSpeed);
			}

			if (transform.position == target.position)
			{
				target.position = (target.position == start) ? end : start;
			}
		}

		if (isCircle && player)
		{
			if (!stop)
			{
				aux = Mathf.RoundToInt(distanceX * 2 * Mathf.PI / (0.02f * speed));
				stop = true;
			}
			float beta = ((0.02f * speed) * iteration) / distanceX;
			float x = distanceX * Mathf.Cos(beta);
			float y = distanceX * Mathf.Sin(beta);
			transform.position = new Vector3(target.position.x + (x), target.position.y + (y), 0f);
			iteration++;
			if (iteration == aux)
			{
				iteration = 0;
			}
		}

		if (isEllipse && player)
		{
			if (!stop)
			{
				aux = Mathf.RoundToInt(360 / (0.5f * speed));
				stop = true;
			}
			float beta = Mathf.Deg2Rad * (0.5f * speed) * iteration;
			float x = distanceX * Mathf.Cos(beta);
			float y = curvature * Mathf.Sin(beta);
			transform.position = new Vector3(target.position.x + (x), target.position.y + (y), 0f);
			iteration++;
			if (iteration == aux)
			{
				iteration = 0;
			}
		}
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			player = true;
		}
		
	}
	void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			player = false;
		}

	}

}
