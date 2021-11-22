using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionArea : MonoBehaviour
{
    private bool _isBoom = false;
    private bool _isDamage = false;

    [SerializeField]
    private float _damage = 33f;
    public void IsBoom()
    {
        _isBoom = true;
    }
    

    void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			if(_isBoom && !_isDamage)
            {
                Vector2 vector = new Vector2(-_damage, 100f);
                col.gameObject.SendMessage("SetLiveScore", vector);
                _isDamage = true;
            }
		}
	}
}
