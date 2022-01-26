using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public GameObject _death;
    public int damage = 25;
    public float _timeAttack = 1.5f;
    private int hp;
    private bool pain = false;
    private bool aux;
    private bool _attack = true;
    private GameObject _player;
    public GameObject _bullet;
    private Animator anim;
    private SpawnCoins _spawnCoins;

    [SerializeField]
    private float _maxSpeed = 8f;

    [SerializeField]
    private float _minSpeed = 4f;

    private float _lifetime = 4.0f;
    private float _timeAlive = 0.0f;
    private bool _isCool = false;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        _spawnCoins = GetComponent<SpawnCoins>();
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
        _attack = true;
	}
    public void SetCool(bool cool)
	{
		_timeAlive = 0;
		_isCool = cool;
        _attack = false;
	}

    void FixedUpdate()
    {
        if (_attack && !_isCool)
        {
            _attack = false;
            Invoke("Attack", _timeAttack);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            float yOffset = 0.1f;
            if (transform.position.y + yOffset < col.transform.position.y && transform.position.x < col.transform.position.x + 0.5f && transform.position.x > col.transform.position.x - 0.5f && !pain)
            {
                _player.SendMessage("EnemyJump");
                Dead();
            }
            else if(!pain)
            {
                Vector3 vector = new Vector3(transform.position.x, transform.position.y, damage);
                _player.SendMessage("EnemyKnockBack", vector);
                _attack = true;
            }


        }
    }

    public void Dead()
    {
        pain = true;
        anim.SetBool("Hit", true);
        _spawnCoins.Destroyer();
        Vector2 aux = new Vector2 (0f, 250f);
        _player.SendMessage("SetLiveScore", aux);
        _player.SendMessage("SetKillBar", 50);
        _player.SendMessage("Dead");
        Instantiate(_death, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject, 1f);
    }
    private void Attack()
    {
        if (!_isCool)
        {
            anim.SetTrigger("Attack");
            _attack = true;
            Invoke("Shoot",0.37f);
        }
    }

    private void Shoot()
    {
        Vector3 _shootPos = new Vector3( transform.position.x, transform.position.y+0.06f, 0f);
        GameObject _bulletObject = Instantiate(_bullet, _shootPos, Quaternion.identity);
        _bulletObject.transform.parent = this.transform;
        var _speed =  Random.Range(_minSpeed, _maxSpeed);
        _bulletObject.GetComponent<Rigidbody2D>().velocity = new Vector3( -transform.localScale.x *_speed, 0f, 0f);
    }

}
