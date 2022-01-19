using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class SpikeBall : MonoBehaviour
{
    public Transform spikeBall;
    public GameObject chain;
	public float rightLimit;
	public float leftLimit;
	public float speed;
	private float _lifetime = 4.0f;
    private float _timeAlive = 0.0f;
	private bool _isCool = false;
	private float _moveSpeed= 0.0f;

	private Rigidbody2D rb2d;

	void Start()
    {
		rb2d = GetComponent<Rigidbody2D>();
		rb2d.angularVelocity = 500;

		float distanceY = Mathf.Abs(transform.position.y) - Mathf.Abs(spikeBall.position.y);
		Vector3 corregirPos = new Vector3(transform.position.x, transform.position.y, 0f);
		GameObject chains = Instantiate(chain, corregirPos, Quaternion.identity);
		int aux = Mathf.RoundToInt(distanceY / 0.25f);
		for (int i = 1; i <= aux; i++)
		{
			Vector3 corregirPos1 = new Vector3(transform.position.x , transform.position.y - (i * 0.25f), 0f);
			GameObject chainschilds = Instantiate(chain, corregirPos1, Quaternion.identity);
			chainschilds.transform.parent = transform;
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
		rb2d.freezeRotation = false;
		_isCool = false;
		Color color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        this.gameObject.GetComponentInChildren<SpriteRenderer>().color = color;
		rb2d.angularVelocity = _moveSpeed;		
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if(!_isCool)
		{
			Move();
		}
		else
		{
			rb2d.freezeRotation = true;
		}
	}

	void Move()
    {
        if (transform.rotation.z > -rightLimit && rb2d.angularVelocity > 0 && rb2d.angularVelocity < speed)
        {
			rb2d.angularVelocity = speed;
        }
        else if (transform.rotation.z < leftLimit && rb2d.angularVelocity < 0 && rb2d.angularVelocity > -speed)
        {
			rb2d.angularVelocity = -speed;
		}
		_moveSpeed = rb2d.angularVelocity;
		Debug.Log(rb2d.angularVelocity);
    }
}
