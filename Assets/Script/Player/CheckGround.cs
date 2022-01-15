using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
	private PlayerController _player;
    private Rigidbody2D _rb2d;

    void Start()
    {
        _player = GetComponentInParent<PlayerController>();
        _rb2d = GetComponentInParent<Rigidbody2D>();
    }

	void OnCollisionStay2D(Collision2D _col)
	{
		if (_col.gameObject.tag == "Ground")
		{
			_player.SetGround(true);
		}
	}

	void OnCollisionExit2D(Collision2D _col)
	{
		if (_col.gameObject.tag == "Ground")
		{
			_player.SetGround(false);
		}
	}

	public void BoxJump(Vector3 _boxPos)
    {
        _player.BoxJump(_boxPos);
    }

	public void Impulse(Vector2 vector)
	{
		_player.Impulse(vector);
	}

	public void EnemyKnockBack(Vector3 enemyPos)
	{
		_player.EnemyKnockBack(enemyPos);
	}
}
