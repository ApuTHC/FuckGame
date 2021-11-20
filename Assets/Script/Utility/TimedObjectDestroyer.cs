using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObjectDestroyer : MonoBehaviour
{
    [SerializeField]
    private float lifetime = 5.0f;
    private float timeAlive = 0.0f;

    [SerializeField]
    private bool destroyChildrenOnDeath = false;
    void Update()
    {
        if (timeAlive > lifetime)
        {
            Destroy(this.gameObject);
        }
        else
        {
            timeAlive += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        if (destroyChildrenOnDeath)
        {
            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                GameObject childObject = transform.GetChild(i).gameObject;
                if (childObject != null)
                {
                    Destroy(childObject);
                }
            }
        }
        transform.DetachChildren();
    }
}
