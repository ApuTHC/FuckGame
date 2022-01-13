using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPotionCollected : MonoBehaviour
{
    private Animator _animator;
	private BoxCollider2D _bc2d;
    private FloorBar _floorBar;
    void Start()
    {
        _animator = GetComponent<Animator>();
		_bc2d = GetComponent<BoxCollider2D>();
        _floorBar = FindObjectOfType<FloorBar>();
    }

    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			_floorBar.Restart();
			_animator.SetTrigger("Collected");
			Destroy(this.gameObject, 0.35f);
		}
	}
}
