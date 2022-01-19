using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private PlayerController _player;
    private Rigidbody2D _rb2d;

    [SerializeField]
    private float delayPlatFall = 0.5f;

    void Start()
    {
        _player = GetComponentInParent<PlayerController>();
        _rb2d = GetComponentInParent<Rigidbody2D>();
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Head" || col.gameObject.tag == "PlatStatic")
        {
            _player.SetGround(true);
        }
        if (col.gameObject.tag == "Platform")
        {
            _rb2d.velocity = Vector3.zero;
            _player.transform.parent = col.transform;
            _player.SetGround(true);
            col.gameObject.SendMessage("SetPlayer", true);
        }
        if (col.gameObject.tag == "PlatFall")
        {
            _player.SetGround(true);
            Invoke("Fall", delayPlatFall);
        }
        if (col.gameObject.tag == "EndPoint")
        {
            _player.SetGround(true);
            //Invoke("Win", 1.5f);
        }
    }

    void Fall()
    {
        _player.SetGround(false);
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "EndPoint" || col.gameObject.tag == "PlatStatic")
        {
            _player.SetGround(false);
        }
        if (col.gameObject.tag == "Platform")
        {
            _player.transform.parent = null;
            _player.SetGround(false);
            col.gameObject.SendMessage("SetPlayer", false);
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
