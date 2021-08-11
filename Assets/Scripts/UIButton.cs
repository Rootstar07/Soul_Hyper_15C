using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIButton : MonoBehaviour
{
    public GameObject targetCanvas;
    public Animator animator;
    [Space]
    public Image backGroundImage;

    public void OpenCloseUI()
    {
        if (targetCanvas.activeSelf)
        {
            // 캔버스 닫기
            animator.SetBool("isOpen", false);            
        }
        else
        {
            // 캔버스 열기
            targetCanvas.SetActive(true);
            animator.SetBool("isOpen", true);

        }
    }
}
