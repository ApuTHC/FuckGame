using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillBar : MonoBehaviour
{
    public Image _killBar;
	private RectTransform _rt;

	private float _kp, _maxKp = 100f;

	// Use this for initialization
	void Start () 
	{
		_kp = _maxKp;
		_rt = _killBar.GetComponent<RectTransform>();
	}

	public void TakeDamage(float _amount)
	{
		_kp = Mathf.Clamp (_kp - _amount, 0f, _maxKp);
		float _relation = _kp/_maxKp;
		_rt.sizeDelta = new Vector2 (_relation, _rt.sizeDelta.y);
		float _posX = (112.3f * (_relation-1f))-704f;
		_rt.localPosition = new Vector3 (_posX, _rt.localPosition.y, _rt.localPosition.z);
	}
	public void Restart()
	{
		_kp = _maxKp;
		_killBar.transform.localScale = new Vector2 (1, 1);
	}

	public void SetKillPoints(float _killPointi)
	{
		_kp = _killPointi;
	}

	public float GetKillPoints()
	{
		return _kp;
	}
}
