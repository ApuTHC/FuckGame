using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public Sprite _sprite0;
    public Sprite _sprite1;
    public Sprite _sprite2;
    public Sprite _sprite3;
    public Sprite _sprite4;
    public Sprite _sprite5;
    public Sprite _sprite6;
    public Sprite _sprite7;
    public Sprite _sprite8;
    public Sprite _sprite9;
    public Image _num1;
    public Image _num2;

    private Sprite[] _sprites = new Sprite[10];
    private Image[] _numbers = new Image[3];

    void Start()
    {
        _numbers[0] = _num1;
        _numbers[1] = _num2;

        _sprites[0] = _sprite0;
        _sprites[1] = _sprite1;
        _sprites[2] = _sprite2;
        _sprites[3] = _sprite3;
        _sprites[4] = _sprite4;
        _sprites[5] = _sprite5;
        _sprites[6] = _sprite6;
        _sprites[7] = _sprite7;
        _sprites[8] = _sprite8;
        _sprites[9] = _sprite9;

        SetCoins(0);
    }

    public void SetCoins(int _coins)
    {
        int[] _nums= new int[3];
        _nums[0] = _coins % 10;
        _coins -= _nums[0];
        _nums[1] = (_coins % 100) / 10;
        _coins -= _nums[1] * 10;

        for (int i = 0; i < 2; i++)
        {
            _numbers[i].sprite = _sprites[_nums[i]];
        }
    }
}
