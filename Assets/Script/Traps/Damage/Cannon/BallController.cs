using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb2d;
    private ExplotionArea _explotionArea;
    
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rb2d = GetComponent<Rigidbody2D>();
        _explotionArea = GetComponentInChildren<ExplotionArea>();
    }

    void OnCollisionEnter2D(Collision2D _col)
    {
        if (_col.gameObject.tag == "Ground" || _col.gameObject.tag == "Player")
        {
            _explotionArea.IsBoom(); 
            _animator.SetBool("isBoom", true);
            _rb2d.velocity = Vector3.zero;
            Destroy(this.gameObject, 0.50f);
        }
    }
}
