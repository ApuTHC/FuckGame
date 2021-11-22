using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadlockController : MonoBehaviour
{
    private Rigidbody2D _rb2d;
	private SpriteRenderer _spr;

    void Start()
    {
        _rb2d = GetComponentInParent<Rigidbody2D>();
		_spr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Ground")
		{
			_rb2d.isKinematic = true;
			_rb2d.velocity = Vector3.zero;
		}
	}
}
