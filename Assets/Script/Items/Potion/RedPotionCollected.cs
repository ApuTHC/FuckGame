using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPotionCollected : MonoBehaviour
{
    private Animator _animator;
	private CircleCollider2D _cc2d;
    private KillBar _killBar;
    void Start()
    {
        _animator = GetComponent<Animator>();
		_cc2d = GetComponent<CircleCollider2D>();
        _killBar = FindObjectOfType<KillBar>();
    }

    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			_killBar.Restart();
			_animator.SetTrigger("Collected");
			Destroy(this.gameObject, 0.35f);
		}
	}
}
