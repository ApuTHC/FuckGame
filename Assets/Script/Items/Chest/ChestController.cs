using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    private Animator _animator;
    public GameObject _object1;
    public GameObject _object2;
    public GameObject _object3;
    public GameObject _object4;
    public GameObject _object5;
    public GameObject _padLock;
    private GameObject[] _objects = new GameObject[5];
    private PlayerController _player;
    private bool isClose = true ;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _player = FindObjectOfType<PlayerController>();
        _objects[0] = _object1;
        _objects[1] = _object2;
        _objects[2] = _object3;
        _objects[3] = _object4;
        _objects[4] = _object5;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(_player.GetKey() && isClose)
            {
                isClose = false;
                _animator.SetTrigger("IsOpen");
                Invoke("OpenPadlock",0.2f);
                Invoke("OpenTreasure",0.8f);
                _player.SetKey(false);
            }
        }
    }

    private void OpenPadlock()
    {
        Vector3 corregirPos = new Vector3( transform.position.x, transform.position.y, -1f);
        GameObject _objectPadlock = Instantiate(_padLock, corregirPos, Quaternion.identity);
        _objectPadlock.GetComponent<Rigidbody2D>().velocity = new Vector3(3f, 4f, 0f);
    }

    private void OpenTreasure()
    {
        Vector3 corregirPosObj = new Vector3( transform.position.x, transform.position.y+0.6f, -1f);
        for (int i = 0; i < 5; i++)
        {
            GameObject objects = Instantiate(_objects[i], corregirPosObj, Quaternion.identity);
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
                _speedX =  Random.Range(-0.5f, -1.5f);
                _speedY =  Random.Range(4.5f, 6.5f);
            }
            if(i == 3)
            {
                _speedX =  Random.Range(-1.5f, -3.5f);
                _speedY =  Random.Range(4.5f, 6.5f);
            }
            if(i == 4)
            {
                _speedX =  Random.Range(-3.5f, -5.5f);
                _speedY =  Random.Range(4.5f, 6.5f);
            }
            objects.GetComponent<Rigidbody2D>().velocity = new Vector3(_speedX, _speedY, 0f);
            objects.GetComponent<Rigidbody2D>().isKinematic = false;
            objects.GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }
}
