using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb2d;
    private Animator _animator;

    // Player
    [SerializeField]
    private float _speed = 2f;
    private float _realSpeed;

    [SerializeField]
    private float _maxSpeed = 3f;

    [SerializeField]
    private bool _grounded;

    [SerializeField]
    private float _jumpPower = 6.5f;
    private bool _wall;
    private bool _wallJump;
    private bool _jump;
    private bool _doubleJump;
    float _h = 0f;

    bool _pain = false;
    

    // Bars
    private HelthBar _healthbar;
    private KillBar _killbar;
    private KeyBar _keyBar;
    private SprintBar _sprintBar;
    private FloorBar _floorBar;
    private IceBar _iceBar;
    public GameObject _floor;
    public GameObject _ice;
    private bool _key = false;

    [SerializeField]
    private float _sprintValue = 2f;
    private bool _sprintAux = false;
    private bool _flooraux = false;
    private bool _iceaux = false;


    // ScoreNumber,Lives & Coins
    public ScoreNumber _scoreNumber;
    private Lives _livesBar;
    private Coins _coinsBar;
    private int _score=0;
    private int _lives = 3;
    private int _coins = 0;

    // Particles
    public GameObject _jumpParticles;
    public GameObject _jumpParticles1;
    public GameObject _fallParticles;
    public GameObject _runParticles;
    private bool _checkFall = false;
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
        _sprintBar = FindObjectOfType<SprintBar>();
        _floorBar = FindObjectOfType<FloorBar>();
        _iceBar = FindObjectOfType<IceBar>();
        
        _livesBar = FindObjectOfType<Lives>();
        _coinsBar = FindObjectOfType<Coins>();
        
        //_scoreNumber.SetScore(_score);
        _livesBar.SetLives(_lives);
        _coinsBar.SetCoins(_coins);

        _realSpeed = _speed;
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

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!_grounded && !_flooraux)
            {
                float aux = 1f;
                if (transform.localScale.x<0.1f)
                {
                    aux = -1f; 
                }
                Vector3 pos = new Vector3(transform.position.x+aux , transform.position.y - 1.5f , 1f);
                if(_floorBar.Floor())
                {
			        Instantiate(_floor , pos , Quaternion.identity);
                    _flooraux = true;
                    Invoke("FloorIn" , 2f);
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!_iceaux)
            {
                float aux = 1f;
                if (transform.localScale.x<0.1f)
                {
                    aux = -1f; 
                }
                Vector3 _shootPos = new Vector3( transform.position.x + aux/3, transform.position.y + 0.2f, 0f);
                if(_iceBar.Ice())
                {
                    GameObject _iceObject = Instantiate(_ice, _shootPos, Quaternion.identity);
                    _iceObject.GetComponent<Rigidbody2D>().velocity = new Vector3(3f*aux, 3f, 0f);
                    _iceaux = true;
                    Invoke("IceIn" , 0.5f);
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            _sprintAux = false;
            Invoke("Sprint" , 0.08f);
        }
        if (Input.GetKey(KeyCode.X))
        {
            if (_sprintAux)
            {
                _sprintAux = false;
                Invoke("Sprint" , 0.08f);
            }
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            Invoke("Run" , 0.1f); 
            _sprintAux = true;
        }
        
    }

    public void IceIn()
    {
        _iceaux = false;
    }
    public void FloorIn()
    {
        _flooraux = false;
    }

    public void Sprint()
    {
        if(_sprintBar.Sprint())
        {
            _speed = _realSpeed * _sprintValue;
            _sprintBar.IsSprint(true);
            _sprintAux = true;
        }
        else
        {
            _speed = _realSpeed;
        }
    }
    public void Run()
    {
        _speed = _realSpeed;
        _sprintBar.IsSprint(false);
    }

     void FixedUpdate()
    {
        Vector3 _fixedVelocity = _rb2d.velocity;
        if(!_pain)
        {
            _fixedVelocity.x *= 0.75f;
        }
        _h = Input.GetAxis("Horizontal");
        _rb2d.velocity = _fixedVelocity;
        if(!_pain)
        {
            _rb2d.AddForce(Vector2.right * _speed * _h);
        }
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

        if (_jump && !_pain)
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
        Vector3 corregirPos = new Vector3( transform.position.x, transform.position.y-0.17f, 0f);
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
        Vector3 corregirPos = new Vector3( transform.position.x, transform.position.y-0.17f, 0f);
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
        _doubleJump = true;
        _rb2d.velocity = Vector3.zero;
        if (_boxPos.z == 1f)
        {
            float sidey = Mathf.Sign(Mathf.Abs(transform.position.y) - Mathf.Abs(_boxPos.y));
            _rb2d.AddForce(Vector2.up * sidey * _jumpPower, ForceMode2D.Impulse);
            JumpParticles(0);
        }
        if (_boxPos.z == 0f)
        {
            float sidex = Mathf.Sign(Mathf.Abs(transform.position.x) - Mathf.Abs(_boxPos.x));
            _rb2d.AddForce(Vector2.right * sidex * _jumpPower*3f, ForceMode2D.Impulse);
        }
        _score = _score + 10;
        _scoreNumber.SetScore(_score);
    }
    public void SetLiveScore(Vector2 vector) 
    {
        _score = _score + Mathf.FloorToInt(vector.y);
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

    public void Impulse(Vector2 vector)
    {
        if (vector.x == 0 || vector.y != 0)
        {
            JumpParticles(1);
        }
        _rb2d.velocity = new Vector3(vector.x, vector.y, 0f);
    }

    public void EnemyKnockBack(Vector3 enemyPos)
    {
        Vector2 aux = new Vector2(-enemyPos.z , 10);
        SetLiveScore(aux);
        _pain = true;
        float sidey = Mathf.Sign(Mathf.Abs(transform.position.y) - Mathf.Abs(enemyPos.y));
        float sidex = Mathf.Sign(Mathf.Abs(enemyPos.x) - Mathf.Abs(transform.position.x));
        _rb2d.AddForce(Vector2.up * sidey * _jumpPower*0.6f, ForceMode2D.Impulse);
        _rb2d.AddForce(Vector2.left * sidex * _jumpPower*0.6f, ForceMode2D.Impulse);       
        Color color = new Color(143 / 255f, 0 / 255f, 0 / 255f, 255 / 255f);
        _spriteRenderer.color = color;
        Invoke("NoPain", 0.7f);
    }

    private void NoPain()
    {
        _spriteRenderer.color = Color.white;
        _pain = false;
    }

}
