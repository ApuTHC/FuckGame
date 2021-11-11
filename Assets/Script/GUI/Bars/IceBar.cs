using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceBar : MonoBehaviour
{
    public Image _iceBar;
	private RectTransform _rt;

	private float _ip, _maxIp = 100f;

	// Use this for initialization
	void Start () 
	{
		_ip = _maxIp;
		_rt = _iceBar.GetComponent<RectTransform>();
	}

	public void TakeDamage(float _amount)
	{
		_ip = Mathf.Clamp (_ip - _amount, 0f, _maxIp);
		float _relation = _ip/_maxIp;
		_rt.sizeDelta = new Vector2 (_relation, _rt.sizeDelta.y);
		float _posX = (112.3f * (_relation-1f))-704f;
		_rt.localPosition = new Vector3 (_posX, _rt.localPosition.y, _rt.localPosition.z);
	}
	public void Restart()
	{
		_ip = _maxIp;
		_iceBar.transform.localScale = new Vector2 (1, 1);
	}

	public void SetKillPoints(float _iceBari)
	{
		_ip = _iceBari;
	}

	public float GetKillPoints()
	{
		return _ip;
	}
}
