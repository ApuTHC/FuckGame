using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

	bool active;
	Canvas canvas;
	public GameOver gameover;
	// Use this for initialization
	void Start () {
		canvas = GetComponent<Canvas> ();
		canvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space")){
			if (gameover.activa == false) {
				active = !active;
				canvas.enabled = active;
				Time.timeScale = (active) ? 0f : 1f;
			}
		}
	}
}
