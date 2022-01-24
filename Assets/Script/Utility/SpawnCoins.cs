using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{

    public GameObject _object1;
    public GameObject _object2;
    public GameObject _object3;
    private GameObject[] _objects = new GameObject[3];
    void Start()
    {
        _objects[0] = _object1;
        _objects[1] = _object2;
        _objects[2] = _object3;
    }

public void Destroyer()
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
    }
}
