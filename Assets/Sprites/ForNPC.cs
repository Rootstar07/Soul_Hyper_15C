using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForNPC : MonoBehaviour
{
    public GameObject alert;
    public TalkManager talkManager;

    private void Start()
    {
        alert.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            alert.SetActive(true);
            talkManager.canTalk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        {
            alert.SetActive(false);
            talkManager.canTalk = false;
        }
    }
}
