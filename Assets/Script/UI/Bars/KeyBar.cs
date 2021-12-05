using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBar : MonoBehaviour
{
    public Image _key;

    private PlayerController _player;

    
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        HideShowKey(_player.GetKey());
    }

    public void HideShowKey(bool _keyi)
    {
        _key.enabled=_keyi; 
    }
}
