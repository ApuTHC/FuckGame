using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HelthBar : MonoBehaviour {

	public Image _healthBar;

	private RectTransform _rt;
	public GameObject _gameover;
	private float _hp, _maxHp = 100f;

	// Use this for initialization
	void Start () 
	{
		_hp = _maxHp;
		_rt = _healthBar.GetComponent<RectTransform>();
	}

	public void TakeDamage(float _amount)
	{
		_hp = Mathf.Clamp (_hp - _amount, 0f, _maxHp);
		float _relation = _hp/_maxHp;
		_rt.sizeDelta = new Vector2 (_relation, _rt.sizeDelta.y);
		float _posX = (162.63f * (_relation-1f))-544f;
		_rt.localPosition = new Vector3 (_posX, _rt.localPosition.y, _rt.localPosition.z);
		if (_hp == 0f) {
			if (_gameover != null) {
				_gameover.SendMessage ("GamesOver");
			}
		}
	}
	public void Restart()
	{
		_hp = _maxHp;
		_healthBar.transform.localScale = new Vector2 (1, 1);
	}

	public void SetHealth(float _healthBari)
	{
		_hp = _healthBari;
	}

	public float GetHealth()
	{
		return _hp;
	}

}
