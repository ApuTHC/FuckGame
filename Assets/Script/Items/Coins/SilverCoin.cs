using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverCoin : MonoBehaviour
{
    [SerializeField]
    private int _coins = 1;
    [SerializeField]
    private int _type = 0;
    private Animator _animator;
	private CircleCollider2D _cc2d;
    private Rigidbody2D _rb2d;
    private PlayerController _player;
	public GameObject _pickUp;
    void Start()
    {
        _animator = GetComponent<Animator>();
		_cc2d = GetComponent<CircleCollider2D>();
        _rb2d = GetComponentInParent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerController>();

        if (_type==1)
        {
            _rb2d.velocity = Vector3.zero;
			_rb2d.isKinematic = true;
			_cc2d.isTrigger = true;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			_player.CoinUp(_coins);
			_animator.SetTrigger("Collected");
			Instantiate(_pickUp, this.transform.position, Quaternion.identity);
			Destroy(this.gameObject, 0.35f);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Ground")
		{
			_rb2d.velocity = Vector3.zero;
			_rb2d.isKinematic = true;
			_cc2d.isTrigger = true;
		}
        if (col.gameObject.tag == "Player")
        {
			_rb2d.velocity = Vector3.zero;
			_rb2d.isKinematic = true;
			_cc2d.isTrigger = true;
            _player.CoinUp(_coins);
			_animator.SetTrigger("Collected");
			Instantiate(_pickUp, this.transform.position, Quaternion.identity);
			Destroy(this.gameObject, 0.35f);
		}
	}
}
