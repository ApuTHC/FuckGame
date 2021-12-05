using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillBar : MonoBehaviour
{
    public Image _killBar;

	private float _kp, _maxKp = 100f;

	// Use this for initialization
	void Start () 
	{
		_kp = 0;
		ModifyBar(_kp);
	}

	public void ModifyBar(float _amount)
	{
		_kp = Mathf.Clamp (_kp - _amount, 0f, _maxKp);
		float _relation = _kp/_maxKp;
		_killBar.fillAmount=_relation;
	}
	public void Restart()
	{
		_kp = _maxKp;
		ModifyBar(0f);
	}

	public void SetKill(float _killBari)
	{
		_kp = _killBari;
		ModifyBar(0f);
	}

	public float GetKill()
	{
		return _kp;
	}
}
