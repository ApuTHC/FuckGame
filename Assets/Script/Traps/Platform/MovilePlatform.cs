using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovilePlatform : MonoBehaviour {

	public Transform _target;
	public float _speed;
	public GameObject _chain;
	public bool _isRect;
	public bool _isCircle;
	public bool _isEllipse;
	public float _curvature;

	private float _lifetime = 4.0f;
    private float _timeAlive = 0.0f;

	float _distanceX;
	float _distanceY;

	int _aux;
	bool _stop = false;
	int _iteration=0;

	private bool _isCool = false;

	private Vector3 _start, _end;

	// Use this for initialization
	void Start () {

		_distanceX = Mathf.Abs(_target.position.x) - Mathf.Abs(transform.position.x);
		_distanceY = Mathf.Abs(_target.position.y) - Mathf.Abs(transform.position.y);

		if (_isRect)
        {
			if (_target != null)
			{
				_target.parent = null;
				_start = transform.position;
				_end = _target.position;
			}
			float angle = Mathf.Atan2(_distanceY, _distanceX);
			float distance = _distanceY / Mathf.Sin(angle);
			if (distance == 0f)
			{
				distance = _distanceX / Mathf.Cos(angle);
			}
			float increaseY = System.Convert.ToSingle(0.2 * Mathf.Sin(angle));
			float increaseX = System.Convert.ToSingle(0.2 * Mathf.Cos(angle));
			_aux = Mathf.RoundToInt(distance / 0.2f);
			Vector3 corregirPos = new Vector3(transform.position.x, transform.position.y, 3f);
			GameObject chains = Instantiate(_chain, corregirPos, Quaternion.identity);
			for (int i = 1; i <= _aux; i++)
			{
				Vector3 corregirPos1 = new Vector3(transform.position.x + (i * increaseX), transform.position.y + (i * increaseY), 3f);
				GameObject chainschilds = Instantiate(_chain, corregirPos1, Quaternion.identity);
				chainschilds.transform.parent = chains.transform;
			}
		}
		if (_isCircle)
		{
			_target.parent = null;

			_distanceX = Mathf.Abs(transform.position.x) - Mathf.Abs(_target.position.x);			
			_aux = Mathf.RoundToInt(_distanceX * 2 * Mathf.PI / 0.2f);
			Vector3 corregirPos = new Vector3(transform.position.x, transform.position.y, 3f);
			GameObject chains = Instantiate(_chain, corregirPos, Quaternion.identity);
			for (int i = 1; i <= _aux; i++)
			{
				float beta = (0.2f * i) / _distanceX;
				float x = _distanceX * Mathf.Cos(beta);
				float y = _distanceX * Mathf.Sin(beta);
				Vector3 corregirPos1 = new Vector3(_target.position.x + (x), _target.position.y + (y), 3f);
				GameObject chainschilds = Instantiate(_chain, corregirPos1, Quaternion.identity);
				chainschilds.transform.parent = chains.transform;
			}
		}
		if (_isEllipse)
		{
			_target.parent = null;

			_distanceX = Mathf.Abs(transform.position.x) - Mathf.Abs(_target.position.x);
			//int aux = Mathf.RoundToInt(distanceX * 2 * Mathf.PI / 0.2f);
			Vector3 corregirPos = new Vector3(transform.position.x, transform.position.y, 3f);
			GameObject chains = Instantiate(_chain, corregirPos, Quaternion.identity);
			_aux = Mathf.RoundToInt(360 / 3.7f);
			for (int i = 1; i <= _aux; i++)
			{
				float beta = Mathf.Deg2Rad * 3.7f * i;
				float x = _distanceX * Mathf.Cos(beta);
				float y = _curvature * Mathf.Sin(beta);
				Vector3 corregirPos1 = new Vector3(_target.position.x + (x), _target.position.y + (y), 3f);
				GameObject chainschilds = Instantiate(_chain, corregirPos1, Quaternion.identity);
				chainschilds.transform.parent = chains.transform;
                beta = 3.7f * i;
                if (_distanceX > _curvature)
                {
					if (beta < 30f)
					{
						i++;
					}
					if (beta > 150f && beta < 210f)
					{
						i++;
					}
					if (beta > 330f)
					{
						i++;
					}
				}
				if (_distanceX < _curvature)
				{
					if (beta > 60f && beta < 120f)
					{
						i++;
					}
					if (beta > 240f && beta < 300f)
					{
						i++;
					}
				}
			}
		}
	}

	public void SetCool(bool cool)
	{
		_timeAlive = 0;
		_isCool = cool;
		
	}

	void Update()
	{
		if (_timeAlive > _lifetime && _isCool)
        {
			MoveOn();
        }
        if (_isCool)
        {
            _timeAlive += Time.deltaTime;
        }
	}

	private void MoveOn(){
		Color color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        this.gameObject.GetComponent<SpriteRenderer>().color = color;
		_isCool = false;
	}

	void FixedUpdate () {
        if (_isRect && !_isCool)
        {
			if (_target != null)
			{
				float fixedSpeed = _speed * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, _target.position, fixedSpeed);
			}

			if (transform.position == _target.position)
			{
				_target.position = (_target.position == _start) ? _end : _start;
			}
		}

		if (_isCircle && !_isCool)
		{
            if (!_stop)
            {
				_aux = Mathf.RoundToInt(_distanceX * 2 * Mathf.PI / (0.02f * _speed));
				_stop = true;
            }
			float beta = ((0.02f * _speed) * _iteration) / _distanceX;
			float x = _distanceX * Mathf.Cos(beta);
			float y = _distanceX * Mathf.Sin(beta);
			transform.position = new Vector3(_target.position.x + (x), _target.position.y + (y), 0f);
			_iteration++;
			if (_iteration == _aux)
            {
				_iteration = 0;
            }
		}

		if (_isEllipse && !_isCool)
		{
			if (!_stop)
			{
				_aux = Mathf.RoundToInt(360 / (0.5f * _speed));
				_stop = true;
			}
			float beta = Mathf.Deg2Rad * (0.5f * _speed) * _iteration;
			float x = _distanceX * Mathf.Cos(beta);
			float y = _curvature * Mathf.Sin(beta);
			transform.position = new Vector3(_target.position.x + (x), _target.position.y + (y), 0f);
			_iteration++;
			if (_iteration == _aux)
			{
				_iteration = 0;
			}
		}
	}

}
