using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBomb : MonoBehaviour
{
    public GameObject _chargingBar;
    public GameObject _bomb;
    private GameObject _chargingBarObject;
    private float _lifetime = 3.0f;
    private float _timeAlive = 0.0f;
    private bool _pulseA = false;
    private PlayerController _player;    
    private bool _dialog = false;
    private int _loser = 3;


    void Start()
    {
        _player = GetComponent<PlayerController>();
    }
    void Update()
    {
        if (_pulseA)
        {
            _timeAlive += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.V) && _player.GetBombs()>0)
        {
            Vector3 _shootPos = new Vector3( transform.position.x + (0.5f * transform.localScale.x), transform.position.y + 0.5f, 0f);
            _chargingBarObject = Instantiate(_chargingBar, _shootPos, Quaternion.identity);
            _chargingBarObject.transform.parent = this.transform;
            _pulseA = true;
            _loser = 3;
        }
        if (Input.GetKeyDown(KeyCode.V) && _player.GetBombs()==0 && _loser==0)
        {
            _player.Loser();
        }
        if (Input.GetKeyDown(KeyCode.V) && _player.GetBombs()==0 && _loser>0)
        {
            _player.No();
            _loser--;
        }
        if (Input.GetKeyUp(KeyCode.V) && _player.GetBombs()>0)
        {
            _chargingBarObject.GetComponent<Animator>().SetBool("isClosing", true);
            Destroy(_chargingBarObject, 0.25f);
            float power = Mathf.Clamp(_timeAlive , 0 , _lifetime) * 4f;
            Vector3 _shootPos = new Vector3( transform.position.x + (0.5f * transform.localScale.x), transform.position.y+0.5f, 0f);
            GameObject _bombObject = Instantiate(_bomb, _shootPos, Quaternion.identity);
            _bombObject.GetComponent<Rigidbody2D>().velocity = new Vector3(transform.localScale.x * power/2, 1f * power/2, 0f);
            _pulseA = false; 
            _timeAlive = 0;
            var aux = _player.GetBombs();
            aux--;
            _player.SetBombs(aux);
            if(!_dialog)
            {
                _dialog = true;
                Invoke("Dialog",1f);
            }
        }
    }

    private void Dialog()
    {
        _player.Boom();
        _dialog = false;
    }
}
