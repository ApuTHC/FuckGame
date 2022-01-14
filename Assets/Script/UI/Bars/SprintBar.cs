using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprintBar : MonoBehaviour
{
    public Image _sprintBar;

	private float _sp, _maxSp = 100f;
	private float _time = 1f;
	private float _timefast = 0.03f;

	private bool _recharge = false;

	private bool _isSprint = false;

	// Use this for initialization
	void Start () 
	{
		_sp = 100;
		ModifyBar(0f);
	}

	void Update()
	{
		if (_sp<100 && !_isSprint && !_recharge)
		{
			_recharge = true;
			Invoke("Recharge" , _time); 
		}
	}

	public void ModifyBar(float _amount)
	{
		_sp = Mathf.Clamp (_sp - _amount, 0f, _maxSp);
		float _relation = _sp/_maxSp;
		_sprintBar.fillAmount=_relation;
	}
	public void Restart()
	{
		_sp = _maxSp;
		ModifyBar(0f);
	}

	public void SetSprint(float _sprintBari)
	{
		_sp = _sprintBari;
		ModifyBar(0f);
	}
	public void IsSprint(bool sprint)
	{
		_isSprint=sprint;
	}
	public float GetSprint()
	{
		return _sp;
	}

	public bool Sprint()
	{
		bool aux = false;
		if (_sp>0)
		{
			aux = true;
			ModifyBar(3f);
			_time = 1f;
		}
		if (_sp<=0)
		{
			aux = false;
		}
		return aux;
	}

	public void Recharge()
	{
		ModifyBar(-2f);
		_recharge = false;
		_time = Mathf.Clamp (_time * 0.2f, _timefast, _maxSp);
		
	}
}
