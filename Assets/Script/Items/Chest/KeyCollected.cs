using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollected : MonoBehaviour
{
    private Animator _animator;
	private BoxCollider2D _bc2d;
    private PlayerController _player;

    void Start()
    {
        _animator = GetComponent<Animator>();
		_bc2d = GetComponent<BoxCollider2D>();
        _player = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			_player.SetKey(true);
			_animator.SetTrigger("Collected");
			Destroy(this.gameObject, 0.35f);
		}
	}
}
