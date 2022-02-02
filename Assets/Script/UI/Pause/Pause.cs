using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

	private bool _active;
	Canvas _canvas;
	public GameOver _gameover;

	void Start () {
		_canvas = GetComponent<Canvas> ();
		_canvas.enabled = false;
	}
	

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)){
			PlayPause();
		}
	}

	public void PlayPause()
	{
		if (_gameover._activa == false) 
		{
			_active = !_active;
			_canvas.enabled = _active;
			Time.timeScale = (_active) ? 0f : 1f;
		}
	}
	public void Restart()
    {	
		_active = !_active;
		_canvas.enabled = _active;
		Time.timeScale = (_active) ? 0f : 1f;
		SceneManager.LoadScene("Level1"); 
	}
	public void Exit()
	{
		_active = !_active;
		_canvas.enabled = _active;
		Time.timeScale = (_active) ? 0f : 1f;
		SceneManager.LoadScene("MainMenu");
	}
}
