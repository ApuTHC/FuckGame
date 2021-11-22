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

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(false)
            {

            }
        }
    }
}
