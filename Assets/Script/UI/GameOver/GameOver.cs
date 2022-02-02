using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public bool _activa;
	private int _score;
	Canvas canvas;
	PlayerController _player;

	ScoreNumber _scoreNumber;
	// Use this for initialization
	void Start () {
		_scoreNumber = GetComponentInChildren<ScoreNumber>(); 
		_player = FindObjectOfType<PlayerController>();
		canvas = GetComponent<Canvas> ();
		canvas.enabled = false;
		_activa = false;
	}

	public void GamesOver(){
		_score = _player.GetScore();
		_scoreNumber.SetScore(_score);
		canvas.enabled = true;
		Time.timeScale = 0f;
		_activa = true;
	}

	public void Restart()
    {	
		Time.timeScale = 1f;	
		SceneManager.LoadScene("Level1");
	}
	public void Exit()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("MainMenu");
	}
}
