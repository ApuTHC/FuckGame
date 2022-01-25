using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogs : MonoBehaviour
{
    public GameObject _attack;
    public GameObject _boom;
    public GameObject _dead;
    public GameObject _exclamation;
    public GameObject _hello;
    public GameObject _interrogation;
    public GameObject _loser;
    public GameObject _no;
    public GameObject _wtf;

    public void Dialog(string dialog)
    {
        GameObject action = _loser;
        switch (dialog)
        {
            case "attack":
                action = _attack;
                break;
            case "boom":
                action = _boom;
                break;
            case "dead":
                action = _dead;
                break;
            case "exclamation":
                action = _exclamation;
                break;
            case "hello":
                action = _hello;
                break;
            case "interrogation":
                action = _interrogation;
                break;
            case "loser":
                action = _loser;
                break;
            case "no":
                action = _no;
                break;
            case "wtf":
                action = _wtf;
                break;
            default:
                action = _loser;
                break;
        }
        
        Vector3 _shootPos = new Vector3( transform.position.x + (0.6f * transform.localScale.x), transform.position.y+0.5f, 0f);
        GameObject dialoges = Instantiate(action, _shootPos, Quaternion.identity);
        dialoges.transform.parent = this.transform;
    }
}
