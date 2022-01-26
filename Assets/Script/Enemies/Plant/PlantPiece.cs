using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPiece : MonoBehaviour
{
    private SpriteRenderer _spr;
	private int _cycle = 4;
    void Start()
    {
        Invoke("Desappearing", 0.8f);
        _spr = GetComponent<SpriteRenderer>();
    }

    void Desappearing()
    {
		_cycle--;
		_spr.enabled = false;
		Invoke("Appearing", 0.1f);
        if (_cycle <= 0)
        {
			Destroy(this.gameObject);
		}
    }


	void Appearing()
    {
		_spr.enabled = true;
		Invoke("Desappearing", 0.1f);
	}
}
