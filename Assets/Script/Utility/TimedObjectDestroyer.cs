using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObjectDestroyer : MonoBehaviour
{
    [SerializeField]
    private float _lifetime = 5.0f;
    private float _timeAlive = 0.0f;

    [SerializeField]
    private bool _destroyChildrenOnDeath = false;
    void Update()
    {
        if (_timeAlive > _lifetime)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _timeAlive += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        if (_destroyChildrenOnDeath)
        {
            int _childCount = transform.childCount;
            for (int i = 0; i < _childCount; i++)
            {
                GameObject _childObject = transform.GetChild(i).gameObject;
                if (_childObject != null)
                {
                    Destroy(_childObject);
                }
            }
        }
        transform.DetachChildren();
    }
}
