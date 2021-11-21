using UnityEngine;

public class BoxHit : MonoBehaviour
{
    private Animator anim;
    public int hits = 3;
    public GameObject fruit1;
    public GameObject fruit2;
    public GameObject fruit3;
    public GameObject breakUpLeft;
    public GameObject breakUpRight;
    public GameObject breakDownLeft;
    public GameObject breakDownRight;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Player_Ground")
        {
            Vector3 vector = new Vector3(transform.position.x, transform.position.y, 1f);
            if (col.gameObject.tag == "Player_Ground")
            {
                col.SendMessage("BoxJump", vector);
            }
            if (hits > 1)
            {
                anim.SetTrigger("Hit");
 
            } else if (hits == 1)
            {
                Invoke("Destroyer", 0.25f);
                anim.SetTrigger("Break");
                Destroy(gameObject, 0.25f);
                
            }
            hits--;
        }
    }


    void Destroyer()
    {
        Vector3 corregirPos = new Vector3( transform.position.x, transform.position.y, 0f);
        GameObject fruit1Object = Instantiate(fruit1, corregirPos, Quaternion.identity);
        GameObject fruit2Object = Instantiate(fruit2, corregirPos, Quaternion.identity);
        GameObject fruit3Object = Instantiate(fruit3, corregirPos, Quaternion.identity);
        fruit1Object.GetComponent<Rigidbody2D>().velocity = new Vector3(3f, 4f, 0f);
        fruit2Object.GetComponent<Rigidbody2D>().velocity = new Vector3(-2f, 5f, 0f);
        fruit3Object.GetComponent<Rigidbody2D>().velocity = new Vector3(1f, 6f, 0f);
        fruit1Object.GetComponent<Rigidbody2D>().isKinematic = false;
        fruit2Object.GetComponent<Rigidbody2D>().isKinematic = false;
        fruit3Object.GetComponent<Rigidbody2D>().isKinematic = false;
        fruit1Object.GetComponent<CircleCollider2D>().isTrigger = false;
        fruit2Object.GetComponent<CircleCollider2D>().isTrigger = false;
        fruit3Object.GetComponent<CircleCollider2D>().isTrigger = false;

        Vector3 corregirPosUpLeft = new Vector3(transform.position.x- 0.2f, transform.position.y+ 0.18f, 0f);
        Vector3 corregirPosUpRight = new Vector3(transform.position.x+ 0.14f, transform.position.y+ 0.17f, 0f);
        Vector3 corregirPosDownLeft = new Vector3(transform.position.x- 0.17f, transform.position.y- 0.2f, 0f);
        Vector3 corregirPosDownRight = new Vector3(transform.position.x+ 0.21f, transform.position.y- 0.2f, 0f);
        GameObject breakUpLeftObject = Instantiate(breakUpLeft, corregirPosUpLeft, Quaternion.identity);
        GameObject breakUpRightObject = Instantiate(breakUpRight, corregirPosUpRight, Quaternion.identity);
        GameObject breakDownLeftObject = Instantiate(breakDownLeft, corregirPosDownLeft, Quaternion.identity);
        GameObject breakDownRightObject = Instantiate(breakDownRight, corregirPosDownRight, Quaternion.identity);
        breakUpLeftObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-3f, 4f, 0f);
        breakUpRightObject.GetComponent<Rigidbody2D>().velocity = new Vector3(4f, 2f, 0f);
        breakDownLeftObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-1f, -7f, 0f);
        breakDownRightObject.GetComponent<Rigidbody2D>().velocity = new Vector3(2f, -3f, 0f);

    }

}
