using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HelthBar : MonoBehaviour {

	public Image _health;
	public GameObject _gameover;
	private float _hp, _maxHp = 100f;

	// Use this for initialization
	void Start () {
		_hp = _maxHp;
	}

	public void TakeDamage(float _amount){
		_hp = Mathf.Clamp (_hp - _amount, 0f, _maxHp);
		_health.transform.localScale = new Vector2 (_hp / _maxHp, 1);
		if (_hp == 0f) {
			if (_gameover != null) {
				_gameover.SendMessage ("GamesOver");
			}
		}
	}
	public void Restart(){
		_hp = _maxHp;
		_health.transform.localScale = new Vector2 (1, 1);
	}

}
