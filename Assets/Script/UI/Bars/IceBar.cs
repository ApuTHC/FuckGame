using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceBar : MonoBehaviour
{
    public Image _iceBar;

	private float _ip, _maxIp = 100f;

	// Use this for initialization
	void Start () 
	{
		_ip = 0;
		ModifyBar(_ip);
	}

	public void ModifyBar(float _amount)
	{
		_ip = Mathf.Clamp (_ip - _amount, 0f, _maxIp);
		float _relation = _ip/_maxIp;
		_iceBar.fillAmount=_relation;
	}
	public void Restart()
	{
		_ip = _maxIp;
		ModifyBar(0f);
	}

	public void SetIce(float _iceBari)
	{
		_ip = _iceBari;
		ModifyBar(0f);
	}

	public float GetIce()
	{
		return _ip;
	}
}
