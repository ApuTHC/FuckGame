using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Animator _anim;

    [SerializeField]
	private float _impulseX;

	[SerializeField]
	private float _impulseY;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player_Ground")
        {
            Vector2 vector = new Vector2(_impulseX, _impulseY);
            col.SendMessage("Impulse", vector);
            _anim.SetTrigger("Collected");
            Destroy(gameObject, 0.25f);
        }
    }
    
}
