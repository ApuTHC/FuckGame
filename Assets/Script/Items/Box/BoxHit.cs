using UnityEngine;

public class BoxHit : MonoBehaviour
{
    private Animator _animator;
    public int hits = 3;
    public GameObject _object1;
    public GameObject _object2;
    public GameObject _object3;
    private GameObject[] _objects = new GameObject[3];
    public GameObject _breakTopLeft;
    public GameObject _breakTopRight;
    public GameObject _breakBottomLeft;
    public GameObject _breakBottomRight;
    private GameObject[] _breakBox = new GameObject[4];

    void Start()
    {
        _animator = GetComponent<Animator>();
        _objects[0] = _object1;
        _objects[1] = _object2;
        _objects[2] = _object3;
        _breakBox[0] = _breakTopLeft;
        _breakBox[1] = _breakTopRight;
        _breakBox[2] = _breakBottomLeft;
        _breakBox[3] = _breakBottomRight;
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
        for (int i = 0; i < 3; i++)
        {
            GameObject objects = Instantiate(_objects[i], corregirPos, Quaternion.identity);
            var _speedX = 0f;
            var _speedY = 0f;
            if(i == 0)
            {
                _speedX =  Random.Range(2.5f, 4.5f);
                _speedY =  Random.Range(3.5f, 5.5f);
            }
            if(i == 1)
            {
                _speedX =  Random.Range(0.5f, 1.5f);
                _speedY =  Random.Range(5.5f, 7.5f);
            }
            if(i == 2)
            {
                _speedX =  Random.Range(-1.5f, -3.5f);
                _speedY =  Random.Range(4.5f, 6.5f);
            }
            objects.GetComponent<Rigidbody2D>().velocity = new Vector3(_speedX, _speedY, 0f);
            objects.GetComponent<Rigidbody2D>().isKinematic = false;
            objects.GetComponent<CircleCollider2D>().isTrigger = false;
        }

        for (int i = 0; i < 4; i++)
        {
            Vector3 corregirPosBox = Vector3.zero;
            var _speedX = 0f;
            var _speedY = 0f;
            if(i == 0)
            {
                corregirPosBox = new Vector3(transform.position.x- 0.2f, transform.position.y+ 0.18f, 0f);
                _speedX =  Random.Range(-2.5f, -4.5f);
                _speedY =  Random.Range(3.5f, 5.5f);
            }
            if(i == 1)
            {
                corregirPosBox = new Vector3(transform.position.x+ 0.14f, transform.position.y+ 0.17f, 0f);
                _speedX =  Random.Range(3.5f, 5.5f);
                _speedY =  Random.Range(1.5f, 3.5f);
            }
            if(i == 2)
            {
                corregirPosBox = new Vector3(transform.position.x- 0.17f, transform.position.y- 0.2f, 0f);
                _speedX =  Random.Range(-0.5f, -2.5f);
                _speedY =  Random.Range(-6.5f, -8.5f);
            }
            if(i == 3)
            {
                corregirPosBox = new Vector3(transform.position.x+ 0.21f, transform.position.y- 0.2f, 0f);
                _speedX =  Random.Range(1.5f, 3.5f);
                _speedY =  Random.Range(-2.5f, -4.5f);
            }
            GameObject breakBox = Instantiate(_breakBox[i], corregirPosBox, Quaternion.identity);
            breakBox.GetComponent<Rigidbody2D>().velocity = new Vector3(_speedX, _speedY, 0f);
        }
    }
}
