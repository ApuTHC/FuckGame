using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPig : MonoBehaviour
{
    public float visionRadius;
    public float attackRadius;
    public float speed;
    public int maxHp = 3;
    public int damage = 25;

    float initialSpeed;
    int hp;
    bool pain;
    bool aux;

    Vector3 initialPosition, target;
    GameObject player;
    Animator anim;
    Rigidbody2D rb2d;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        hp = maxHp;
        initialSpeed = speed;
    }

    void Update()
    {
        target = initialPosition;

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            player.transform.position - transform.position,
            visionRadius,
            1 << LayerMask.NameToLayer("Player")
        );
        Vector3 forward = transform.TransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                target = player.transform.position;
            }
        }

        Debug.DrawLine(transform.position, target, Color.green);
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(target, transform.position);

        if (target != initialPosition && distance < attackRadius)
        {
            rb2d.velocity = Vector3.zero;
            anim.SetBool("Walk", false);
        }
        else
        {
            if (hp == maxHp && !pain)
            {
                anim.SetBool("Walk", true);
            }
            if (hp < maxHp && !pain)
            {
                anim.SetBool("Run", true);
            }

            float h = target.x - transform.position.x;

            if (h < -0.05f && !pain)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                rb2d.velocity = new Vector3(-speed, rb2d.velocity.y, 0f);
            }

            if (h > 0.05f && !pain)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                rb2d.velocity = new Vector3(speed, rb2d.velocity.y, 0f);
            }

            if (h >= -0.05f && h <= 0.05f && !pain)
            {
                if (hp == maxHp)
                {
                    anim.SetBool("Walk", false);
                }
                rb2d.velocity = new Vector3(0f, rb2d.velocity.y, 0f);
            }

        }

        if (target == initialPosition && transform.position.x < target.x + 0.06f && transform.position.x > target.x - 0.06f)
        {
            transform.position = initialPosition;
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            hp = maxHp;
            aux = false;
            speed = initialSpeed;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            float yOffset = 0.1f;
            if (transform.position.y + yOffset < col.transform.position.y && transform.position.x < col.transform.position.x + 0.5f && transform.position.x > col.transform.position.x - 0.5f)
            {
                pain = true;
                player.SendMessage("EnemyJump");
                hp--;
                if (hp>0)
                {
                    if (!aux)
                    {
                        speed = 2 * initialSpeed;
                        aux = true;
                    }
                    anim.SetBool("Walk", false);
                    anim.SetBool("Hit", true);
                    anim.SetBool("Run", false);
                    float sidex = Mathf.Sign(Mathf.Abs(target.x) - Mathf.Abs(transform.position.x));
                    rb2d.AddForce(Vector2.left * sidex * 3f, ForceMode2D.Impulse);
                    Invoke("Hit", 1f);
                }
                if (hp == 0)
                {
                    anim.SetBool("Hit2", true);
                    Destroy(gameObject, 1f);
                }
            }
            else
            {
                Vector3 vector = new Vector3(transform.position.x, transform.position.y, damage);
                player.SendMessage("EnemyKnockBack", vector);
            }


        }
    }
    void Hit()
    {
        if (hp>0)
        {
            anim.SetBool("Hit", false);
            anim.SetBool("Run", true);
            pain = false;
        }
    }  
}
