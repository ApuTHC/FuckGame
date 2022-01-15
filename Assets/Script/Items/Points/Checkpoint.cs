using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            anim.SetTrigger("Active");
            PlayerPrefs.SetFloat("PlayerX", transform.position.x);
            PlayerPrefs.SetFloat("PlayerY", transform.position.y-0.5f);
        }
    }
}
