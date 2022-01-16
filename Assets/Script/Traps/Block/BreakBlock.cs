using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{
	private Animator anim;
	private Rigidbody2D rb2d;
	private BoxCollider2D bc2d;
	private SpriteRenderer spr;
	int aux = 4;

	void Start()
	{
		anim = GetComponent<Animator>();
		rb2d = GetComponentInParent<Rigidbody2D>();
		bc2d = GetComponent<BoxCollider2D>();
		spr = GetComponent<SpriteRenderer>();
		Invoke("Destroyer", 0.14f);
	}

	void Destroyer()
    {
		anim.SetTrigger("Destroy");
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Ground")
		{
			rb2d.velocity = Vector3.zero;
			rb2d.isKinematic = true;
			bc2d.isTrigger = true;
			Invoke("Desappearing", 0.5f);
		}
	}

	void Desappearing()
	{
		aux--;
		spr.enabled = false;
		Invoke("Appearing", 0.1f);
		if (aux <= 0)
		{
			Destroy(gameObject);
		}
	}


	void Appearing()
	{
		spr.enabled = true;
		Invoke("Desappearing", 0.1f);
	}
}
