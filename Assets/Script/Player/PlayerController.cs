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
    private HelthBar _healthbar;
    private KillBar _killbar;
    private ScoreNumber _scoreNumber;
    private int _score=0;
    private KeyBar _keyBar;
    private Lives _livesBar;
    private Coins _coinsBar;
    private int _lives = 3;
    private int _coins = 0;
    public GameObject _jumpParticles;
    public GameObject _jumpParticles1;
    public GameObject _fallParticles;
    private bool _checkFall = false;
    public GameObject _runParticles;
    private bool _checkRun = true;
    private bool _auxRun = true;
   
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>(); 
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        _healthbar = FindObjectOfType<HelthBar>();
        _killbar = FindObjectOfType<KillBar>();
        _keyBar = FindObjectOfType<KeyBar>();
        _scoreNumber = FindObjectOfType<ScoreNumber>();
        _livesBar = FindObjectOfType<Lives>();
        _coinsBar = FindObjectOfType<Coins>();
        
        _scoreNumber.SetScore(_score);
        _livesBar.SetLives(_lives);
        _coinsBar.SetCoins(_coins);
    }

    
    void Update()
    {
        _animator.SetFloat("SpeedX", Mathf.Abs(_rb2d.velocity.x));
        _animator.SetFloat("SpeedY", _rb2d.velocity.y);
        _animator.SetBool("Grounded", _grounded);
        _animator.SetBool("Wall", _wall);

        if (!_grounded)
        {
            _checkFall=true;
        }
        if (_grounded)
        {
            if (_checkFall)
            {
                FallParticles();
            }
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
            _checkRun = true; 
            if (_grounded && _auxRun)
            {
                _auxRun = false;
                RunParticles();
            }
        }

        if (_h < -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            _checkRun = false; 
            if (_grounded && _auxRun)
            {
                _auxRun = false;
                RunParticles();
            }
        }

        if (_jump)
        {
            int aux=0;

            if (!_grounded)
            {
                aux=1;
                _animator.SetTrigger("DoubleJump");
            }
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, 0);
            _rb2d.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            JumpParticles(aux);
            _jump = false;
        }
    }
    public void JumpParticles(int aux){
        Vector3 corregirPos = new Vector3( transform.position.x, transform.position.y-0.2f, 0f);
        if (aux==1)
        {
            GameObject objects = Instantiate(_jumpParticles, corregirPos, Quaternion.identity);
        }
        else
        {
            GameObject objects = Instantiate(_jumpParticles1, corregirPos, Quaternion.identity);
        }
    }
    public void FallParticles(){
        _checkFall=false;
        Vector3 corregirPos = new Vector3( transform.position.x, transform.position.y-0.2f, 0f);
        GameObject objects = Instantiate(_fallParticles, corregirPos, Quaternion.identity);
    }
    public void RunParticles(){
        Vector3 corregirPos;
        if (_checkRun)
        {
            _runParticles.transform.localScale = new Vector3(1f, 1f, 1f);
            corregirPos = new Vector3( transform.position.x-0.1f, transform.position.y-0.17f, 0f);
        }
        else
        {
            _runParticles.transform.localScale = new Vector3(-1f, 1f, 1f);
            corregirPos = new Vector3( transform.position.x+0.1f, transform.position.y-0.17f, 0f);
        }
        GameObject objects = Instantiate(_runParticles, corregirPos, Quaternion.identity);
        Invoke("RunDelay", 0.2f);
    }
    private void RunDelay(){
        _auxRun=true;
    }
    public void BoxJump(Vector3 _boxPos)
    {
        if (_boxPos.z == 1f)
        {
            _doubleJump = true;
            _rb2d.velocity = Vector3.zero;
            float sidey = Mathf.Sign(Mathf.Abs(transform.position.y) - Mathf.Abs(_boxPos.y));
            _rb2d.AddForce(Vector2.up * sidey * _jumpPower, ForceMode2D.Impulse);
            JumpParticles(0);
        }
    }
    public void SetLiveScore(Vector2 vector) 
    {
        _score+= Mathf.FloorToInt(vector.y);
        _scoreNumber.SetScore(_score);
        
        if (vector.x<0)
        {
            float kp = _killbar.GetKill();
            if ((kp+vector.x)<0)
            {
                _killbar.ModifyBar(vector.x);
                _healthbar.ModifyHealth(vector.x+kp);
            }
            else
            {
                _killbar.ModifyBar(vector.x);
            }
        }
        else
        {
            _healthbar.ModifyHealth(vector.x);
        }
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
    public int GetScore()
    {
        return _score;
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
    public void CoinUp(int coini)
    {
        _coins += coini;
        if(_coins>=100)
        {
            LiveUp(1);
            _coins=0;
        }
        _coinsBar.SetCoins(_coins);
    }
}
