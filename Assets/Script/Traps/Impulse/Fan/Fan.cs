using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
	[SerializeField]
	private float _impulseFanX;

	[SerializeField]
	private float _impulseFanY;

	[SerializeField]
	private int _damage = 5;
	private Collider2D _player;
	private bool _aux = false;

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Player_Ground")
		{
			Vector3 vector = new Vector3(transform.position.x, transform.position.y, _damage);
			col.gameObject.SendMessage("EnemyKnockBack", vector);
		}
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player_Ground" && !_aux)
		{
			_player = col;
			_aux = true;
			Invoke("Impulse", 0.15f);
		}
	}

	private void Impulse()
	{
			Vector2 vector = new Vector2(_impulseFanX, _impulseFanY);
			_player.SendMessage("Impulse", vector);
			_aux = false;
	}
}
