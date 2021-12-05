using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;

    [SerializeField]
    private float _maxSpeed = 3f;

    [SerializeField]
    private bool _grounded;

    [SerializeField]
    private float _jumpPower = 6.5f;

    [SerializeField]
    private bool _key = false;
    private bool _wall;
    private bool _wallJump;
    private bool _jump;
    private bool _doubleJump;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb2d;
    private Animator _animator;
    private GameObject _healthbar;
    private GameObject _score;
    private int _scoreNumber;
    private KeyBar _keyBar;
    private Lives _livesBar;
    private int _lives = 3;
   
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>(); 
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _healthbar = GameObject.Find("HealthBar");
        _score = GameObject.Find("ScoreNumber");
        _keyBar = FindObjectOfType<KeyBar>();
        _livesBar = FindObjectOfType<Lives>();
        _livesBar.SetLives(_lives);
    }

    
    void Update()
    {
        _animator.SetFloat("SpeedX", Mathf.Abs(_rb2d.velocity.x));
        _animator.SetFloat("SpeedY", _rb2d.velocity.y);
        _animator.SetBool("Grounded", _grounded);
        _animator.SetBool("Wall", _wall);

        if (_grounded)
        {
            _doubleJump = true;
            _wall = false;
            _wallJump = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_wallJump)
            {
                _jump = true;
                _doubleJump = false;
                _wallJump = false;
            }
            if (_grounded)
            {
                _jump = true;
                _doubleJump = true;
            }
            else if (_doubleJump)
            {
                _jump = true;
                _doubleJump = false;
            }
        }
    }

     void FixedUpdate()
    {
        Vector3 _fixedVelocity = _rb2d.velocity;
        _fixedVelocity.x *= 0.75f;
        _rb2d.velocity = _fixedVelocity;
        float _h = Input.GetAxis("Horizontal");
        _rb2d.AddForce(Vector2.right * _speed * _h);
        float _limitedSpeed = Mathf.Clamp(_rb2d.velocity.x, -_maxSpeed, _maxSpeed);
        _rb2d.velocity = new Vector2(_limitedSpeed, _rb2d.velocity.y);

        if (_h > 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);    
        }

        if (_h < -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (_jump)
        {
            if (!_grounded)
            {
                _animator.SetTrigger("DoubleJump");
            }
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, 0);
            _rb2d.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            _jump = false;
        }
    }
    public void BoxJump(Vector3 _boxPos)
    {
        if (_boxPos.z == 1f)
        {
            _doubleJump = true;
            _rb2d.velocity = Vector3.zero;
            float sidey = Mathf.Sign(Mathf.Abs(transform.position.y) - Mathf.Abs(_boxPos.y));
            _rb2d.AddForce(Vector2.up * sidey * _jumpPower, ForceMode2D.Impulse);
        }
    }
    public void SetLiveScore(Vector2 _vector) 
    {
        _healthbar.SendMessage("ModifyHealth", _vector.x);
        _scoreNumber+= Mathf.FloorToInt(_vector.y);
        _score.SendMessage("SetScore", _scoreNumber);
    }
    public void SetWall(bool _walli)
    {
        _wall=_walli;
    }
    public void SetWallJump(bool _jumpi)
    {
        _wallJump=_jumpi;
    }
    public void SetGround(bool _groundi)
    {
        _grounded=_groundi;
    }

    public bool GetKey()
    {
        return _key;
    }

    public void SetKey(bool _keyi)
    {
        _key = _keyi;
        _keyBar.HideShowKey(_keyi);
    }

    public void LiveUp(int livi)
    {
        _lives += livi;
        _livesBar.SetLives(_lives);
    }
}
