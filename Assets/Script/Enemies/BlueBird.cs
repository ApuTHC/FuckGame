using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBird : MonoBehaviour
{
    public float _speed;
    public int damage = 25;
    public Transform _target;
    private float initialSpeed;
    private int hp;
    private bool pain = false;
    private bool aux;
    private bool _stop = true;
    private bool _stopAux = false;
    private Vector3 _start, _end;
    private GameObject player;
    private Animator anim;
    private Rigidbody2D rb2d;
    private SpawnCoins _spawnCoins;

    private float _lifetime = 4.0f;
    private float _timeAlive = 0.0f;
    private bool _isCool = false;

    public float _posY = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        _spawnCoins = GetComponent<SpawnCoins>();
        initialSpeed = _speed;
        if (_target != null)
        {
            _target.parent = null;
            _start = transform.position;
            _end = _target.position;
        }
    }

    void Update()
	{
		if (_timeAlive > _lifetime && _isCool)
        {
			MoveOn();
        }
        if (_isCool)
        {
            _timeAlive += Time.deltaTime;
        }
	}

	private void MoveOn(){
		Color color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        this.gameObject.GetComponent<SpriteRenderer>().color = color;
		_isCool = false;
	}
    	public void SetCool(bool cool)
	{
		_timeAlive = 0;
		_isCool = cool;
	}

    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x, _start.y+_posY);
        if (transform.position.x < _target.position.x+0.2f && transform.position.x > _target.position.x-0.2f)
        {
            _target.position = (_target.position == _start) ? _end : _start;
            _stop = true;
        }
        if (_stop && !_stopAux)
        {
            rb2d.velocity = new Vector2(0f, 0f);
            _stopAux = true;
            Invoke("Stop", 0.5f);
        }

        float h = _target.position.x - transform.position.x;

        if (h < -0.05f && !pain && !_isCool && !_stop)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            rb2d.velocity = new Vector2(-_speed, 0f);
        }

        if (h > 0.05f && !pain && !_isCool && !_stop)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            rb2d.velocity = new Vector2(_speed, 0f);
        }

        if (_isCool)
        {
            rb2d.velocity = new Vector2(0f, 0f);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            float yOffset = 0.1f;
            if (transform.position.y + yOffset < col.transform.position.y && transform.position.x < col.transform.position.x + 0.5f && transform.position.x > col.transform.position.x - 0.5f && !pain)
            {
                player.SendMessage("EnemyJump");
                Dead();
            }
            else if(!pain)
            {
                Vector3 vector = new Vector3(transform.position.x, transform.position.y, damage);
                player.SendMessage("EnemyKnockBack", vector);
                _stop = true;
            }


        }
    }

    public void Dead()
    {
        pain = true;
        anim.SetBool("Hit", true);
        _spawnCoins.Destroyer();
        Destroy(_target.gameObject, 1.5f);
        Vector2 aux = new Vector2 (0f, 250f);
        player.SendMessage("SetLiveScore", aux);
        player.SendMessage("Dead");
        Destroy(this.gameObject, 1f);
    }

    private void Stop()
    {
        _stop = false;
        _stopAux = false;
    }  
}
