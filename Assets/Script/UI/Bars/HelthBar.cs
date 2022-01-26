using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HelthBar : MonoBehaviour {

	public Image _healthBar;
	private PlayerController _player;
	public GameObject _gameover;
	private float _hp, _maxHp = 100f;

	// Use this for initialization
	void Start () 
	{
		_player = FindObjectOfType<PlayerController>();
		_hp = _maxHp;
	}

	public void ModifyHealth(float _amount)
	{
		_hp = Mathf.Clamp (_hp + _amount, 0f, _maxHp);
		float _relation = _hp/_maxHp;
		_healthBar.fillAmount=_relation;
		if (_hp == 0f) { 
			var aux =_player.GetLive();
			if (_gameover != null && aux==0) {
				_gameover.SendMessage ("GamesOver");
			}
			if (aux>0) {
				_player.LiveUp(-1);
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
