using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet : MonoBehaviour
{
    public GameObject _bulletPiece1;
    public GameObject _bulletPiece2;

    private bool _destroyAux = true;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Vector3 vector = new Vector3(transform.position.x, transform.position.y, 25f);
            col.gameObject.SendMessage("EnemyKnockBack", vector);
        }
        if (col.gameObject.tag != "Enemy")
        {
            if (_destroyAux)
            {
                Destroyer();
                _destroyAux = false;
            }
        }
        
    }

    private void Destroyer()
    {
        Vector3 corregirPosBox = new Vector3(transform.position.x - 0.1f, transform.position.y + 0.2f, 0f);
        var _speedX =  Random.Range(-1.5f, -2.5f);
        var _speedY =  Random.Range(2.5f, 3.5f);
        GameObject bulletPiece1Object = Instantiate(_bulletPiece1, corregirPosBox, Quaternion.identity);
        bulletPiece1Object.GetComponent<Rigidbody2D>().velocity = new Vector3(_speedX, _speedY, 0f);
    
        corregirPosBox = new Vector3(transform.position.x + 0.1f, transform.position.y - 0.2f, 0f);
        _speedX =  Random.Range(1.5f, 2.5f);
        _speedY =  Random.Range(-1.5f, -2.5f);
        GameObject bulletPiece2Object = Instantiate(_bulletPiece2, corregirPosBox, Quaternion.identity);
        bulletPiece2Object.GetComponent<Rigidbody2D>().velocity = new Vector3(_speedX, _speedY, 0f);

        Destroy(this.gameObject);
    }
}
