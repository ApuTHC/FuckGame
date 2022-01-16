using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : MonoBehaviour
{
    public int damage = 5;
    public bool vertical;
    public bool horizontal;
    public bool square;

    private Rigidbody2D rb2d;
    private Animator anim;
    //private BoxCollider2D bc2d;

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
        if (rb2d.velocity.y > 1f) { up = true; up1 = true; }
        if (rb2d.velocity.y < -1f) { down = true; down1 = true; }
        if (rb2d.velocity.x > 1f) { right = true; right1 = true; }
        if (rb2d.velocity.x < -1f) { left = true; left1 = true; }

        if (up1)
        {
            rb2d.velocity = new Vector3(0f, 10f, 0f);
        }
        if (down1)
        {
            rb2d.velocity = new Vector3(0f, -10f, 0f);
        }
        if (right1)
        {
            rb2d.velocity = new Vector3(10f, 0f, 0f);
        }
        if (left1)
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

        if (col.gameObject.tag == "Player")
        {
            Vector3 vector = new Vector3(transform.position.x, transform.position.y, damage);
            col.gameObject.SendMessage("EnemyKnockBack", vector);
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
            transform.position = new Vector3(transform.position.x - 0.025f, transform.position.y, 0f);
        }
        if (left)
        {
            anim.SetBool("Left", false);
            up1 = false;
            down1 = false;
            right1 = false;
            left1 = false;
            transform.position = new Vector3(transform.position.x + 0.025f, transform.position.y, 0f);
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
