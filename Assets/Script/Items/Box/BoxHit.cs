using UnityEngine;

public class BoxHit : MonoBehaviour
{
    private Animator _animator;
    public int hits = 3;
    public GameObject _object1;
    public GameObject _object2;
    public GameObject _object3;
    public GameObject breakTopLeft;
    public GameObject breakTopRight;
    public GameObject breakBottomLeft;
    public GameObject breakBottomRight;

    void Start()
    {
        _animator = GetComponent<Animator>();
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
                _animator.SetTrigger("Hit");
 
            } else if (hits == 1)
            {
                Invoke("Destroyer", 0.25f);
                _animator.SetTrigger("Break");
                Destroy(this.gameObject, 0.25f);
                
            }
            hits--;
        }
    }


    void Destroyer()
    {
        Vector3 corregirPos = new Vector3( transform.position.x, transform.position.y, 0f);
        GameObject _object1Object = Instantiate(_object1, corregirPos, Quaternion.identity);
        GameObject _object2Object = Instantiate(_object2, corregirPos, Quaternion.identity);
        GameObject _object3Object = Instantiate(_object3, corregirPos, Quaternion.identity);
        _object1Object.GetComponent<Rigidbody2D>().velocity = new Vector3(3f, 4f, 0f);
        _object2Object.GetComponent<Rigidbody2D>().velocity = new Vector3(-2f, 5f, 0f);
        _object3Object.GetComponent<Rigidbody2D>().velocity = new Vector3(1f, 6f, 0f);
        _object1Object.GetComponent<Rigidbody2D>().isKinematic = false;
        _object2Object.GetComponent<Rigidbody2D>().isKinematic = false;
        _object3Object.GetComponent<Rigidbody2D>().isKinematic = false;
        _object1Object.GetComponent<CircleCollider2D>().isTrigger = false;
        _object2Object.GetComponent<CircleCollider2D>().isTrigger = false;
        _object3Object.GetComponent<CircleCollider2D>().isTrigger = false;

        Vector3 corregirPosTopLeft = new Vector3(transform.position.x- 0.2f, transform.position.y+ 0.18f, 0f);
        Vector3 corregirPosTopRight = new Vector3(transform.position.x+ 0.14f, transform.position.y+ 0.17f, 0f);
        Vector3 corregirPosBottomLeft = new Vector3(transform.position.x- 0.17f, transform.position.y- 0.2f, 0f);
        Vector3 corregirPosBottomRight = new Vector3(transform.position.x+ 0.21f, transform.position.y- 0.2f, 0f);
        GameObject breakTopLeftObject = Instantiate(breakTopLeft, corregirPosTopLeft, Quaternion.identity);
        GameObject breakTopRightObject = Instantiate(breakTopRight, corregirPosTopRight, Quaternion.identity);
        GameObject breakBottomLeftObject = Instantiate(breakBottomLeft, corregirPosBottomLeft, Quaternion.identity);
        GameObject breakBottomRightObject = Instantiate(breakBottomRight, corregirPosBottomRight, Quaternion.identity);
        breakTopLeftObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-3f, 4f, 0f);
        breakTopRightObject.GetComponent<Rigidbody2D>().velocity = new Vector3(4f, 2f, 0f);
        breakBottomLeftObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-1f, -7f, 0f);
        breakBottomRightObject.GetComponent<Rigidbody2D>().velocity = new Vector3(2f, -3f, 0f);

    }

}
