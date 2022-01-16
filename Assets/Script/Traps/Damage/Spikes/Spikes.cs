using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
	public int damage = 5;

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			Vector3 vector = new Vector3(transform.position.x, transform.position.y, damage);
			col.gameObject.SendMessage("EnemyKnockBack", vector);
		}
	}
}
