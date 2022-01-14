using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorBar : MonoBehaviour
{
    public Image _floorBar;

	private float _fp, _maxFp = 100f;

	// Use this for initialization
	void Start () 
	{
		_fp = 0;
		ModifyBar(_fp);
	}

	public void ModifyBar(float _amount)
	{
		_fp = Mathf.Clamp (_fp - _amount, 0f, _maxFp);
		float _relation = _fp/_maxFp;
		_floorBar.fillAmount=_relation;
	}
	public void Restart()
	{
		_fp = _maxFp;
		ModifyBar(0f);
	}

	public void SetFloor(float _floorBari)
	{
		_fp = _floorBari;
		ModifyBar(0f);
	}

	public float GetFloor()
	{
		return _fp;
	}

	public bool Floor()
	{
		bool aux = false;

		if (_fp>=30)
		{
			ModifyBar(34f);
			aux =true;
		}

		return aux;
	}
}
