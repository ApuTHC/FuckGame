using UnityEngine.SceneManagement;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player" )
		{
			anim.SetBool("Active", true);
			string scene = SceneManager.GetActiveScene().name;
			PlayerPrefs.SetString("PlayerScene",scene);
		}
	}

	void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player" )
		{
			anim.SetBool("Active", false);
		}
	}

}
