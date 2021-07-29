using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIButton : MonoBehaviour
{
    public GameObject targetCanvas;
    public PeopleManager peopleManager;
    public Animator animator;

    public void OpenCloseUI()
    {
        if (targetCanvas.activeSelf)
        {
            // 캔버스 닫기
            peopleManager.DeletePeople();
            animator.SetBool("isOpen", false);

            //Invoke("WaitForAni", 0.5f);
            
        }
        else
        {
            // 캔버스 열기
            targetCanvas.SetActive(true);
            animator.SetBool("isOpen", true);
        }
    }

    public void WaitForAni()
    {
        targetCanvas.SetActive(false);
    }



        /*
        for (int i =0; i < peopleList.Length; i++)
        {
            if (json.myIndexList.pIndex[i].활성화여부 == true)
            {
                peopleList[i].SetActive(true);
            }
        }
        */

}
