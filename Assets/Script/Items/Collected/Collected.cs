using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

public class Collected : MonoBehaviour
{
	[SerializeField]
	private int _lifeUp;

	[SerializeField]
	private int _score;

	private Animator _animator;
	private Rigidbody2D _rb2d;
	private CircleCollider2D _cc2d;

	void Start()
	{
		_animator = GetComponent<Animator>();
		_rb2d = GetComponentInParent<Rigidbody2D>();
		_cc2d = GetComponent<CircleCollider2D>();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			Vector2 vector = new Vector2(_lifeUp, _score);
			col.gameObject.SendMessage("SetLiveScore", vector);
			_animator.SetTrigger("Collected");
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
			Vector2 vector = new Vector2(_lifeUp, _score);
			col.gameObject.SendMessage("SetLiveScore", vector);
			_animator.SetTrigger("Collected");
			Destroy(this.gameObject, 0.35f);
		}
	}

}
