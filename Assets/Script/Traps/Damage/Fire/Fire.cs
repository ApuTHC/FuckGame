using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Animator anim;
	public int damage = 10;

	void Start()
    {
        anim = GetComponent<Animator>();
    }

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			anim.SetBool("Pressed",true);
			Invoke("Press", 0.35f);
		}
	}
	
	void Press()
    {
		anim.SetBool("Pressed", false);
		Invoke("On", 1f);
    }

	void On()
	{
		Invoke("Off", 1f);
		anim.SetBool("On", true);
	}

	void Off()
    {
		anim.SetBool("On", false);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			Vector3 vector = new Vector3(transform.position.x, transform.position.y, damage);
			col.gameObject.SendMessage("EnemyKnockBack", vector);
		}
	}
}
