using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePotionCollected : MonoBehaviour
{
    private Animator _animator;
	private BoxCollider2D _bc2d;
    private IceBar _iceBar;
    void Start()
    {
        _animator = GetComponent<Animator>();
		_bc2d = GetComponent<BoxCollider2D>();
        _iceBar = FindObjectOfType<IceBar>();
    }

    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			_iceBar.Restart();
			_animator.SetTrigger("Collected");
			Destroy(this.gameObject, 0.35f);
		}
	}
}
