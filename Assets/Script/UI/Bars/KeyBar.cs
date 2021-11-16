using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBar : MonoBehaviour
{
    public Image _key;

    
    void Start()
    {
        HideShowKey(false);
    }

    public void HideShowKey(bool _keyi)
    {
        _key.enabled=_keyi; 
    }
}
