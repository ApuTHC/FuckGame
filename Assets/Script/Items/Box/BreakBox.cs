using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBox : MonoBehaviour
{
	private Rigidbody2D _rb2d;
	private SpriteRenderer _spr;
	private int _cycle = 4;

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
			Invoke("Desappearing", 0.5f);
		}
	}

	void Desappearing()
    {
		_cycle--;
		_spr.enabled = false;
		Invoke("Appearing", 0.1f);
        if (_cycle <= 0)
        {
			Destroy(this.gameObject);
		}
    }


	void Appearing()
    {
		_spr.enabled = true;
		Invoke("Desappearing", 0.1f);
	}
}
