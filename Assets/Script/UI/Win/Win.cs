using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
   private bool _active;
	Canvas _canvas;

	void Start () {
		_canvas = GetComponent<Canvas> ();
		_canvas.enabled = false;
	}
	
    public void Winn()
    {	
        Invoke("Wini", 2f);
	}

    private void Wini()
    {
        _active = !_active;
        _canvas.enabled = _active;
        Time.timeScale = (_active) ? 0f : 1f;
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
