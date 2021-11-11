using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprintBar : MonoBehaviour
{
    public Image _sprintBar;
	private RectTransform _rt;

	private float _sp, _maxSp = 100f;

	// Use this for initialization
	void Start () 
	{
		_sp = _maxSp;
		_rt = _sprintBar.GetComponent<RectTransform>();
	}

	public void TakeDamage(float _amount)
	{
		_sp = Mathf.Clamp (_sp - _amount, 0f, _maxSp);
		float _relation = _sp/_maxSp;
		_rt.sizeDelta = new Vector2 (_relation, _rt.sizeDelta.y);
		float _posX = (112.3f * (_relation-1f))-704f;
		_rt.localPosition = new Vector3 (_posX, _rt.localPosition.y, _rt.localPosition.z);
	}
	public void Restart()
	{
		_sp = _maxSp;
		_sprintBar.transform.localScale = new Vector2 (1, 1);
	}

	public void SetKillPoints(float _sprintBari)
	{
		_sp = _sprintBari;
	}

	public float GetKillPoints()
	{
		return _sp;
	}
}
