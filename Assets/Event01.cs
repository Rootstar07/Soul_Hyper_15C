using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event01 : MonoBehaviour
{
    public GameObject 돼지머리;

    private void Start()
    {
        if (돼지머리.activeSelf == true)
        {
            돼지머리.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            돼지머리.SetActive(true);
            Debug.Log("이벤트 01 실행");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("부딪힘");
    }
}
