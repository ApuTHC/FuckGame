using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprintBar : MonoBehaviour
{
    public Image _sprintBar;

	private float _sp, _maxSp = 100f;

	// Use this for initialization
	void Start () 
	{
		_sp = 100;
		ModifyBar(0f);
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

	public float GetSprint()
	{
		return _sp;
	}
}
