using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public bool activa;
	Canvas canvas;
	// Use this for initialization
	void Start () {
		canvas = GetComponent<Canvas> ();
		canvas.enabled = false;
		activa = false;
	}

	public void GamesOver(){
		canvas.enabled = true;
		Time.timeScale = 0f;
		activa = true;
	}

	public void Restart()
    {		
		SceneManager.LoadScene("Pruebas");
	}
	public void Exit()
	{
		SceneManager.LoadScene("Portada");
	}
}
