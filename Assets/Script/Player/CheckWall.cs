using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWall : MonoBehaviour
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
            Vector3 _fixedVelocity = _rb2d.velocity;
            _fixedVelocity.y *= 0.75f;
            _rb2d.velocity = _fixedVelocity;
            _player.SetWall(true);
            _player.SetWallJump(false);
        }

    }

    void OnCollisionExit2D(Collision2D _col)
    {
        if (_col.gameObject.tag == "Ground")
        {
            _player.SetWall(false);
            //_player.SetWallJump(true);
        }
    }

    public void SetLiveScore(Vector2 vector)
    {
        _player.SetLiveScore(vector);
    }

    public void BoxJump(Vector3 _boxPos)
    {
        _player.BoxJump(_boxPos);
    }

    public void EnemyKnockBack(Vector3 enemyPos)
	{
		_player.EnemyKnockBack(enemyPos);
	}

}
