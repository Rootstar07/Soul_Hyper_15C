using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
using Newtonsoft.Json;
using System.IO;

public class TalkManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public int nowNPCCOde;
    public Animator talkCanvasAnimator;
    bool isOpen = false;
    public int 현재NPCData배열위치;
    public GameObject talkData;
    [Space]
    public GameObject[] 페이즈리스트;

    private void Start()
    {
        talkData.SetActive(false);
        DeletePhase();

    }

    private void Update()
    {
        // 대화 첫 실행
        if (Input.GetKeyDown(KeyCode.Space) && playerMovement.nPCCode != 0)
        {
            if (!isOpen)
            {
                talkCanvasAnimator.SetBool("isOpen", true);
                isOpen = true;

                // 현재 대화중인 NPC의 코드 확인
                nowNPCCOde = playerMovement.nPCCode;
                playerMovement.speed = 0;
                DeletePhase();

                // 코드에 따른 페이즈 생성

            for (int i =0; i< DataManager.instance.nPCDatas.Length; i++)
                {
                    if (DataManager.instance.nPCDatas[i].NPC코드 == nowNPCCOde)
                    {
                        GeneratePhase(i);
                        break;
                    }
                }
            }
            else
            {
                talkCanvasAnimator.SetBool("isOpen", false);
                isOpen = false;
                playerMovement.speed = 20;
            }
        }
    }

    public void GeneratePhase(int x)
    {
        Debug.Log("NPC 코드 확인");
        현재NPCData배열위치 = x;

        for (int i = 0; i < DataManager.instance.nPCDatas[x].가능한페이즈리스트.Length; i++)
        {
            int npc페이즈코드 = DataManager.instance.nPCDatas[x].가능한페이즈리스트[i];
            int Basicdatas_Index = (npc페이즈코드 / 100) - 1;

            for (int j = 0; j < DataManager.instance.basicDatas[Basicdatas_Index].페이즈리스트.Length; j++)
            {
                int phaseCode = DataManager.instance.basicDatas[Basicdatas_Index].페이즈리스트[j].페이즈코드;

                if (npc페이즈코드 == phaseCode &&
                    DataManager.instance.basicDatas[Basicdatas_Index].페이즈리스트[j].페이즈활성여부 == true)
                {
                    Debug.Log("출력!");
                    페이즈리스트[i].SetActive(true);
                    break;
                }

            }



        }
    }

    public void DeletePhase()
    {
        현재NPCData배열위치 = 0;

        for (int i = 0; i < 페이즈리스트.Length; i++)
        {
            페이즈리스트[i].SetActive(false);
        }
    }

    public void CloseButton()
    {
        talkCanvasAnimator.SetBool("isOpen", false);
        isOpen = false;
    }

}
