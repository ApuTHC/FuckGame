using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombBar : MonoBehaviour
{
    public Image _bombBar1;
    public Image _bombBar2;
    public Image _bombBar3;
    public Image[] _bombs = new Image[3]; 

    void Start()
    {
        _bombs[0] = _bombBar1;
        _bombs[1] = _bombBar2;
        _bombs[2] = _bombBar3;

        for (var i = 0; i < 3; i++)
        {
            _bombs[i].enabled = false;
        }
    }
    public void SetBombs(int bombs)
    {
        for (var i = 0; i < 3; i++)
        {
            _bombs[i].enabled = false;
        }
        for (var i = 0; i < bombs; i++)
        {
            _bombs[i].enabled = true;
        }
    }
    
}
