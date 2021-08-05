using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForObject : MonoBehaviour
{
    public int 오브젝트코드;
    public TalkManager talkManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().objectCode = 오브젝트코드;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().objectCode = 0;
        }
    }

}
