using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPig : MonoBehaviour
{
    public GameObject _hurt;
    public GameObject _death;
    public float _speed;
    public int maxHp = 3;
    public int damage = 25;
    public Transform _target;
    float initialSpeed;
    int hp;
    bool pain = false;
    bool aux;
    private Vector3 _start, _end;
    private GameObject player;
    private Animator anim;
    private Rigidbody2D rb2d;
    private SpawnCoins _spawnCoins;

    private float _lifetime = 4.0f;
    private float _timeAlive = 0.0f;
    private bool _isCool = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        _spawnCoins = GetComponent<SpawnCoins>();
        hp = maxHp;
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
        anim.SetBool("Walk", false);
        anim.SetBool("Run", false);
	}

    void FixedUpdate()
    {
        if (transform.position.x < _target.position.x+0.2f && transform.position.x > _target.position.x-0.2f)
        {
            _target.position = (_target.position == _start) ? _end : _start;
        }
        if (hp == maxHp && !pain && !_isCool)
        {
            anim.SetBool("Walk", true);
        }
        if (hp < maxHp && !pain && !_isCool)
        {
            anim.SetBool("Run", true);
        }

        float h = _target.position.x - transform.position.x;

        if (h < -0.05f && !pain && !_isCool)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            rb2d.velocity = new Vector2(-_speed, 0f);
        }

        if (h > 0.05f && !pain && !_isCool)
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
                pain = true;
                player.SendMessage("EnemyJump");
                Dead();
            }
            else if(!pain)
            {
                Vector3 vector = new Vector3(transform.position.x, transform.position.y, damage);
                player.SendMessage("EnemyKnockBack", vector);
            }


        }
    }
    public void Dead()
    {
        hp--;
        if (hp>0)
        {
            if (!aux)
            {
                _speed = 2 * initialSpeed;
                aux = true;
            }
            anim.SetBool("Walk", false);
            anim.SetBool("Hit", true);
            anim.SetBool("Run", false);
            float sidex = Mathf.Sign(Mathf.Abs(player.transform.position.x) - Mathf.Abs(transform.position.x));
            rb2d.AddForce(Vector2.left * sidex * 3f, ForceMode2D.Impulse);
            Invoke("Hit", 1f);
            Instantiate(_hurt, this.transform.position, Quaternion.identity);
        }
        if (hp == 0)
        {
            anim.SetBool("Hit2", true);
            _spawnCoins.Destroyer();
            Destroy(this.gameObject, 2f);
            Destroy(_target.gameObject, 2f);
            Vector2 aux = new Vector2 (0f, 350f);
            player.SendMessage("SetLiveScore", aux);
            player.SendMessage("SetKillBar", 50);
            player.SendMessage("Dead");
            Instantiate(_death, this.transform.position, Quaternion.identity);
        }
    }
    void Hit()
    {
        if (hp>0)
        {
            anim.SetBool("Hit", false);
            anim.SetBool("Run", true);
            pain = false;
        }
    }  
}
