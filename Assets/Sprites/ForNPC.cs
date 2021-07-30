using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForNPC : MonoBehaviour
{
    public GameObject alert;
    public TalkManager talkManager;
    public int NPC코드;

    private void Start()
    {
        alert.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            alert.SetActive(true);
            other.GetComponent<PlayerMovement>().nPCCode = NPC코드;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        {
            alert.SetActive(false);
            other.GetComponent<PlayerMovement>().nPCCode = 0;
        }
    }
}
