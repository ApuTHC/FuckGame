using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceController : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private Animator _animator; 
    private CircleCollider2D _cc2d;
    private SpriteRenderer _spr;

    private bool _colli = false;
    private float _posX = 0f;
    private float _posY = 0f;
    private float _lifetime = 5.0f;
    private float _timeAlive = 0.0f;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _cc2d = GetComponent<CircleCollider2D>();
        _spr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (_timeAlive > _lifetime && !_colli)
        {
             Destroyer();
        }
        else
        {
            _timeAlive += Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            
            _colli = true;
            _posX = col.transform.position.x;
            _posY = col.transform.position.y;
            col.transform.position = new Vector3(_posX,_posY,0f);
            Color color = new Color(81 / 255f, 231 / 255f, 255 / 255f, 255 / 255f);
            col.gameObject.GetComponent<SpriteRenderer>().color = color;
            _spr.enabled = false;
            _rb2d.velocity = Vector2.zero;
            _rb2d.isKinematic = true;
			_cc2d.isTrigger = true;
            transform.position = new Vector3(_posX,_posY,0f);
            col.gameObject.SendMessage("SetCool", true);
            Invoke("Destroyer", 4f);
        }
    }


    public void Destroyer()
    {
        Destroy(this.gameObject);
    }
}
