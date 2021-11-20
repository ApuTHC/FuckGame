using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField]
    private float _maxSpeed = 8f;

    [SerializeField]
    private float _minSpeed = 4f;

    [SerializeField]
    private float _boomPeriod = 3f;

    public GameObject _cannonBall;
    public GameObject _cannonChild;

    private Animator _animator;

    private float _actualTime;
    void Start()
    {
        _actualTime = 0;
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        _actualTime += Time.deltaTime;
        if (_actualTime > _boomPeriod)
        {
            _actualTime = 0;
            Vector3 _shootPos = new Vector3( transform.position.x, transform.position.y, 0f);
            GameObject _cannonBallObject = Instantiate(_cannonBall, _shootPos, Quaternion.identity);
            _cannonBallObject.transform.parent = _cannonChild.transform;
            var _speed =  Random.Range(_minSpeed, _maxSpeed);
            _cannonBallObject.GetComponent<Rigidbody2D>().velocity = new Vector3( -transform.localScale.x *_speed, 0f, 0f);
            _animator.SetBool("IsShoot", true);
        }else{
            _animator.SetBool("IsShoot", false);
        }
    }
    
}
