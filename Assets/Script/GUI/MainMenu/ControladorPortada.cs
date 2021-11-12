using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorPortada : MonoBehaviour
{

    public void StartPlayer()
    {
        PlayerPrefs.SetFloat("PlayerX", 0f);
        PlayerPrefs.SetFloat("PlayerY", 0f);
        SceneManager.LoadScene("Pruebas");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
