using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveClouds : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;

    public Tilemap _clouds1;
    public Tilemap _clouds2;
    public Tilemap _clouds3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _clouds1.transform.position = new Vector3(_clouds1.transform.position.x - _speed * Time.deltaTime , _clouds1.transform.position.y , _clouds1.transform.position.z);
        _clouds2.transform.position = new Vector3(_clouds2.transform.position.x - _speed * Time.deltaTime , _clouds2.transform.position.y , _clouds2.transform.position.z);
        _clouds3.transform.position = new Vector3(_clouds3.transform.position.x - _speed * Time.deltaTime , _clouds3.transform.position.y , _clouds3.transform.position.z);
        if (_clouds1.transform.position.x<-14f)
        {
            _clouds1.transform.position = new Vector3(14f , _clouds1.transform.position.y , _clouds1.transform.position.z);
        }
        if (_clouds2.transform.position.x<-14f)
        {
            _clouds2.transform.position = new Vector3(14f , _clouds2.transform.position.y , _clouds2.transform.position.z);
        }
        if (_clouds2.transform.position.x<-14f)
        {
            _clouds2.transform.position = new Vector3(14f , _clouds2.transform.position.y , _clouds2.transform.position.z);
        }
    }
}
