using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenCoin : MonoBehaviour
{
    [SerializeField]
    private int _lives = 1;
    private Animator _animator;
	private CircleCollider2D _cc2d;
    private PlayerController _player;
    void Start()
    {
        _animator = GetComponent<Animator>();
		_cc2d = GetComponent<CircleCollider2D>();
        _player = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			_player.LiveUp(_lives);
			_animator.SetTrigger("Collected");
			Destroy(this.gameObject, 0.35f);
		}
	}
}
