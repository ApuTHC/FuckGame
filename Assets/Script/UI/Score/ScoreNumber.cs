using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreNumber : MonoBehaviour
{
    public Image _num1;
    public Image _num2;
    public Image _num3;
    public Image _num4;
    public Image _num5;
    public Image _num6;
    public Image _num7;
    public Image _num8;
    private Image[] _numbers = new Image[8];
    public Sprite _sprite1;
    public Sprite _sprite2;
    public Sprite _sprite3;
    public Sprite _sprite4;
    public Sprite _sprite5;
    public Sprite _sprite6;
    public Sprite _sprite7;
    public Sprite _sprite8;
    public Sprite _sprite9;
    public Sprite _sprite0;
    private Sprite[] _sprites = new Sprite[10];


    void Start()
    {       
        _numbers[0] = _num1;
        _numbers[1] = _num2;
        _numbers[2] = _num3;
        _numbers[3] = _num4;
        _numbers[4] = _num5;
        _numbers[5] = _num6;
        _numbers[6] = _num7;
        _numbers[7] = _num8;

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

        SetScore(10);
    }

    public void SetScore(int _score)
    {
        int[] _nums= new int[8];
        _nums[0] = _score % 10;
        _score -= _nums[0];
        _nums[1] = (_score % 100) / 10;
        _score -= _nums[1] * 10;
        _nums[2] = (_score % 1000) / 100;
        _score -= _nums[2] * 100;
        _nums[3] = (_score % 10000) / 1000;
        _score -= _nums[3] * 1000;
        _nums[4] = (_score % 100000) / 10000;
        _score -= _nums[4] * 10000;
        _nums[5] = (_score % 1000000) / 100000;
        _score -= _nums[5] * 100000;
        _nums[6] = (_score % 10000000) / 1000000;
        _score -= _nums[6] * 1000000;
        _nums[7] = (_score % 100000000) / 10000000;
        _score -= _nums[7] * 10000000;
        for (int i = 0; i < 8; i++)
        {
            _numbers[i].sprite = _sprites[_nums[i]];
        }
    }

}
