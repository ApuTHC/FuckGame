using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    private Animator anim;
	public ParticleSystem dust;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			anim.SetTrigger("Active");
			dust.Play();
		}
	}
}
