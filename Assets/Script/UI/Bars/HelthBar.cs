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

	public void ModifyHealth(float _amount)
	{
		_hp = Mathf.Clamp (_hp + _amount, 0f, _maxHp);
		float _relation = _hp/_maxHp;
		_rt.sizeDelta = new Vector2 (_relation, _rt.sizeDelta.y);
		float _posX = (162.63f * (_relation-1f))-991.8f;
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
		ModifyHealth(0f);
	}

	public void SetHealth(float _healthBari)
	{
		_hp = _healthBari;
		ModifyHealth(0f);
	}

	public float GetHealth()
	{
		return _hp;
	}

}
