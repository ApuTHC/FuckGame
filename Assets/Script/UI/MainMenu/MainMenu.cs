using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image _audioBtn;
    public Sprite _on;
    public Sprite _off;
    private bool _audio = true;
    void Start()
    {
        if (_audio)
        {
            _audioBtn.sprite = _on;
        }
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Continue()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Levels()
    {
        //SceneManager.LoadScene("Levels");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Audio()
    {
        if (_audio)
        {
            _audio = false;
            _audioBtn.sprite = _off;
        }
        else
        {
            _audio = true;
            _audioBtn.sprite = _on;
        }
    }
    public void GlobalScore()
    {
        //SceneManager.LoadScene("GlobalScore");
    }
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
