using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleButtonAni : MonoBehaviour
{
    public void MouseHover()
    {
        GetComponent<Animator>().SetBool("isHover", true);
    }

    public void MouseOut()
    {
        GetComponent<Animator>().SetBool("isHover", false);
    }
}
