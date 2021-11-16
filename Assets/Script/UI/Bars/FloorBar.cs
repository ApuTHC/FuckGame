using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorBar : MonoBehaviour
{
    public Image _floorBar;
	private RectTransform _rt;

	private float _fp, _maxFp = 100f;

	// Use this for initialization
	void Start () 
	{
		_fp = _maxFp;
		_rt = _floorBar.GetComponent<RectTransform>();
	}

	public void TakeDamage(float _amount)
	{
		_fp = Mathf.Clamp (_fp - _amount, 0f, _maxFp);
		float _relation = _fp/_maxFp;
		_rt.sizeDelta = new Vector2 (_relation, _rt.sizeDelta.y);
		float _posX = (76.1f * (_relation-1f))-1080f;
		_rt.localPosition = new Vector3 (_posX, _rt.localPosition.y, _rt.localPosition.z);
	}
	public void Restart()
	{
		_fp = _maxFp;
		_floorBar.transform.localScale = new Vector2 (1, 1);
	}

	public void SetKillPoints(float _floorPointi)
	{
		_fp = _floorPointi;
	}

	public float GetKillPoints()
	{
		return _fp;
	}
}
