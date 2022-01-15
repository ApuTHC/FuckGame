using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private Animator _anim;
	private Collider2D _player;

	[SerializeField]
	private float _impulseX;

	[SerializeField]
	private float _impulseY;

	bool _aux;


	void Start()
    {
        _anim = GetComponent<Animator>();
    }

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player_Ground")
		{
            if (!_aux)
            {
				_anim.SetBool("Jump", true);
				Invoke("Normal", 0.9f);
				Invoke("Jump", 0.1f);
				_player = col;
				_aux = true;
			}
		}
	}

	void Normal()
    {
		_anim.SetBool("Jump", false);
	}

	void Jump()
    {
		Vector2 vector = new Vector2(_impulseX, _impulseY);
		_player.gameObject.SendMessage("Impulse", vector);
		_aux = false;
	}
}
