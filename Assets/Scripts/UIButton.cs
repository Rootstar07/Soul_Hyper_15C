using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIButton : MonoBehaviour
{
    public GameObject targetCanvas;
    public PeopleManager peopleManager;
    public Animator animator;
    [Space]
    public GameObject npcData;
    public Image backGroundImage;

    public void OpenCloseUI()
    {
        if (targetCanvas.activeSelf)
        {
            // 캔버스 닫기
            peopleManager.DeletePeople();
            animator.SetBool("isOpen", false);

            
        }
        else
        {
            // 캔버스 열기
            targetCanvas.SetActive(true);
            animator.SetBool("isOpen", true);

            peopleManager.UpdatePeople();

            /*
            // npc 대화 중이라면 배경을 좀 더 어둡게
            if (npcData.activeSelf == true)
            {
                backGroundImage.enabled = true;
            }
            else
            {
                backGroundImage.enabled = false;
            }
            */
        }
    }
}
