using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombBar : MonoBehaviour
{
    public Image _bombBar1;
    public Image _bombBar2;
    public Image _bombBar3;

    void Start()
    {
        _bombBar1.enabled = false;
        _bombBar2.enabled = false;
        _bombBar3.enabled = false;
    }
    public void HideShowBombs(bool _hideshowi, int _bombi)
    {
        if (_bombi == 1)
        {
            _bombBar1.enabled=_hideshowi;
        }
        if (_bombi == 2)
        {
            _bombBar2.enabled=_hideshowi;
        }
        if (_bombi == 3)
        {
            _bombBar3.enabled=_hideshowi;
        }
    }
    
}
