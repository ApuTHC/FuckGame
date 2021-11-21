using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb2d;
	private CircleCollider2D _cc2d;

    private ExplotionArea _explotionArea;

    [SerializeField]
    private float _lifetime = 3.0f;
    private float _timeAlive = 0.0f;
 
    void Start()
    {
        _animator = GetComponentInChildren<Animator>(); 
        _rb2d = GetComponentInParent<Rigidbody2D>();
	    _cc2d = GetComponent<CircleCollider2D>();
        _animator.SetBool("isOn", true); 
        _explotionArea = GetComponentInChildren<ExplotionArea>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeAlive > _lifetime)
        {
            _explotionArea.IsBoom(); 
            _animator.SetBool("isBoom", true);
            Destroy(this.gameObject, 0.7f);
        }
        else
        {
            _timeAlive += Time.deltaTime;
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
	}
}
