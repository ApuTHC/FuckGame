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
                Vector3 vector = new Vector3(transform.position.x, transform.position.y, _damage);
                col.SendMessage("EnemyKnockBack", vector);
                _isDamage = true;
            }
		}

		if (col.gameObject.tag == "Enemy")
		{
			if(_isBoom && !_isDamage)
            {
                col.SendMessage("Dead");
                _isDamage = true;
            }
		}
	}
}
