using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHead : MonoBehaviour
{
    public int damage = 5;
    public bool vertical;
    public bool horizontal;
    public bool square;

    private Rigidbody2D rb2d;
    private Animator anim;
    //private BoxCollider2D bc2d;

    private float _lifetime = 4.0f;
    private float _timeAlive = 0.0f;
	private bool _isCool = false;

    private Vector2 _velocity;

    bool up;
    bool down;
    bool right;
    bool left;
    bool up1;
    bool down1;
    bool right1;
    bool left1;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //bc2d = GetComponent<BoxCollider2D>();
        Invoke("Go", 1f);
    }

    private void FixedUpdate()
    {
        if (rb2d.velocity.y > 1f && !_isCool) { up = true; up1 = true; }
        if (rb2d.velocity.y < -1f && !_isCool) { down = true; down1 = true; }
        if (rb2d.velocity.x > 1f && !_isCool) { right = true; right1 = true; }
        if (rb2d.velocity.x < -1f && !_isCool) { left = true; left1 = true; }

        if (up1 && !_isCool)
        {
            rb2d.velocity = new Vector3(0f, 10f, 0f);
        }
        if (down1 && !_isCool)
        {
            rb2d.velocity = new Vector3(0f, -10f, 0f);
        }
        if (right1 && !_isCool)
        {
            rb2d.velocity = new Vector3(10f, 0f, 0f);
        }
        if (left1 && !_isCool)
        {
            rb2d.velocity = new Vector3(-10f, 0f, 0f);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            rb2d.velocity = Vector3.zero;
            if (up)
            {
                anim.SetBool("Top", true);
                Invoke("Hit", 0.25f);
            }
            if (down)
            {
                anim.SetBool("Bottom", true);
                Invoke("Hit", 0.25f);
            }
        }

        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            rb2d.velocity = Vector3.zero;
            if (right)
            {
                anim.SetBool("Right", true);
                Invoke("Hit", 0.25f);
            }
            if (left)
            {
                anim.SetBool("Left", true);
                Invoke("Hit", 0.25f);
            }
        }
    }

    public void SetCool(bool cool)
	{
		_timeAlive = 0;
		_isCool = cool;
		_velocity = rb2d.velocity;
	}

	void Update()
	{
		if (_timeAlive > _lifetime && _isCool)
        {
			MoveOn();
        }
        if (_isCool)
        {
            _timeAlive += Time.deltaTime;
            rb2d.velocity = Vector2.zero;
        }
	}

	private void MoveOn(){
		Color color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        this.gameObject.GetComponent<SpriteRenderer>().color = color;
		_isCool = false;
        rb2d.velocity = _velocity;
	}

    void Go()
    {
        if (vertical)
        {
            rb2d.velocity = new Vector3(0f, 10f, 0f);
        }
        if (horizontal)
        {
            rb2d.velocity = new Vector3(10f, 0f, 0f);
        }
        if (square)
        {
            rb2d.velocity = new Vector3(0f, 10f, 0f);
        }
    }

    void BlinkOn()
    {
        anim.SetBool("Blink", true);
        Invoke("BlinkOff", 0.25f);
    }

    void BlinkOff()
    {
        anim.SetBool("Blink", false);
    }

    void Hit()
    {
        if (up)
        {
            anim.SetBool("Top", false);
            up1 = false;
            down1 = false;
            right1 = false;
            left1 = false;
        }
        if (down)
        {
            anim.SetBool("Bottom", false);
            up1 = false;
            down1 = false;
            right1 = false;
            left1 = false;
        }
        if (right)
        {
            anim.SetBool("Right", false);
            up1 = false;
            down1 = false;
            right1 = false;
            left1 = false;
            transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y, 0f);
        }
        if (left)
        {
            anim.SetBool("Left", false);
            up1 = false;
            down1 = false;
            right1 = false;
            left1 = false;
            transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y, 0f);
        }
        Invoke("BlinkOn", 0.2f);
        Invoke("Again", 1.45f);
    }

    void Again()
    {
        if (vertical)
        {
            if (up)
            {
                rb2d.velocity = new Vector3(0f, -10f, 0f);
            }
            if (down)
            {
                rb2d.velocity = new Vector3(0f, 10f, 0f);
            }
            up = false;
            down = false;
            right = false;
            left = false;
        }
        if (horizontal)
        {
            if (right)
            {
                rb2d.velocity = new Vector3(-10f, 0f, 0f);
            }
            if (left)
            {
                rb2d.velocity = new Vector3(10f, 0f, 0f);
            }
            up = false;
            down = false;
            right = false;
            left = false;
        }
        if (square)
        {
            if (up)
            {
                rb2d.velocity = new Vector3(10f, 0f, 0f);
            }
            if (down)
            {
                rb2d.velocity = new Vector3(-10f, 0f, 0f);
            }
            if (right)
            {
                rb2d.velocity = new Vector3(0f, -10f, 0f);
            }
            if (left)
            {
                rb2d.velocity = new Vector3(0f, 10f, 0f);
            }
            up = false;
            down = false;
            right = false;
            left = false;
        }
    }
}
