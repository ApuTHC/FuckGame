using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlarform : MonoBehaviour
{
    public float fallDelay = 0.5f;
    public float respawnDelay = 3f;
    public int damage = 5;

    private Animator anim;
    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;

    float star;
    bool aux;
    bool golpe;
    bool aux1;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        star = transform.position.y;
    }

    private void FixedUpdate()
    {
        if (transform.position.y + 0.02f >star && transform.position.y-0.02f< star && aux)
        {
            rb2d.velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, star, 0f);
            bc2d.isTrigger = false;
            aux = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!golpe && !aux1)
            {
                aux = false;
                transform.position = new Vector3(transform.position.x, transform.position.y-0.1f, 0f);
                Invoke("Falli", 0.15f);
                Invoke("Fall", fallDelay + 0.15f);
                aux1 = true; 
            } 
            golpe = false;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            golpe = true;
            
            if (!aux)
            {
                Vector3 vector = new Vector3(transform.position.x, transform.position.y, damage);
                col.gameObject.SendMessage("EnemyKnockBack", vector);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            golpe = false;
        }
    }

    void Falli()
    {
        Vector3 vector = new Vector3(transform.position.x, star, 0f);
        transform.position = Vector3.MoveTowards(transform.position, vector, 3f * Time.deltaTime);
    }

    void Fall()
    {
        anim.SetBool("Fall", true);
        rb2d.isKinematic = false;
        bc2d.isTrigger = true;
    }

    void Respawn()
    {
        rb2d.velocity = new Vector3(0f, 1f, 0f);
        aux = true;
        aux1 = false;
        anim.SetBool("Fall", false);
    }

    void OnBecameInvisible()
    {
        rb2d.isKinematic = true;
        rb2d.velocity = Vector3.zero;
        Invoke("Respawn", respawnDelay);
    }
}
