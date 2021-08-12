using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseButtonAni : MonoBehaviour
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
