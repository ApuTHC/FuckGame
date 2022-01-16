using UnityEngine;

public class BlockHit : MonoBehaviour
{
    private Animator anim;
    public GameObject blockPart1;
    public GameObject blockPart2;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Vector3 vector = new Vector3(transform.position.x, transform.position.y, 0f);
            col.SendMessage("BoxJump", vector);
            Invoke("Destroyer", 0.25f);
            anim.SetTrigger("HitSide");
            Destroy(gameObject, 0.25f);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Player_Ground")
        {
            Vector3 vector = new Vector3(transform.position.x, transform.position.y, 1f);
            col.gameObject.SendMessage("BoxJump", vector);
            Invoke("Destroyer", 0.25f);
            anim.SetTrigger("HitTop");
            Destroy(gameObject, 0.25f);
        }
    }

    void Destroyer()
    {
        Vector3 corregirPosblockPart1 = new Vector3(transform.position.x, transform.position.y - 0.21f, 0f);
        Vector3 corregirPosblockPart2 = new Vector3(transform.position.x, transform.position.y + 0.12f, 0f);
        
        GameObject blockPart1Object = Instantiate(blockPart1, corregirPosblockPart1, Quaternion.identity);
        GameObject blockPart2Object = Instantiate(blockPart2, corregirPosblockPart2, Quaternion.identity);

        blockPart1Object.GetComponent<Rigidbody2D>().velocity = new Vector3(-0.3f, -5f, 0f);
        blockPart2Object.GetComponent<Rigidbody2D>().velocity = new Vector3(0.5f, 3f, 0f);
        
    }

}
