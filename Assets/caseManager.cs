using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class caseManager : MonoBehaviour
{
    public GameObject 페이즈오브젝트;
    [Header("누른 코드: 데이터 매니저의 사건 코드와 동일")]
    public int caseCode;
    [Header("페이즈 텍스트")]
    public TextMeshProUGUI 페이즈대상;
    public TextMeshProUGUI 페이즈;
    public TextMeshProUGUI 보충;
    public TextMeshProUGUI 해설;
    [Space]
    public int 페이즈최대인덱스;
    public int 페이즈인덱스;
    [Header("페이즈 버튼")]
    public Button 이전버튼;
    public Button 이후버튼;
    


    public void CaseClicked(int code)
    {      
        페이즈오브젝트.SetActive(true);
        caseCode = code;

        // 텍스트 초기화
        DeletePhase();

        // 사건이 활성화되어 있는지 검사
        if (DataManager.instance.caseDatas[caseCode].사건활성여부 == true)
        {
            // 최대 인덱스 지정
            SetIndex();

            if (페이즈최대인덱스 != -1)
            {
                // 텍스트 변경
                SetPhase();
            }
            else
            {
                // 예외처리: 활성화된 페이즈가 없을때
                페이즈.text = "활성화된 페이즈가 없습니다.";
            }
            
            // 버튼 검사
            이후버튼.interactable = false;

            if (페이즈최대인덱스 == 0)
            {
                이전버튼.interactable = false;
            }
            else
            {
                이전버튼.interactable = true;
            }
        }
        else
        {
            페이즈.text = "활성화된 사건이 아닙니다.";
        }
    }

    public void DeletePhase()
    {
        페이즈대상.text = "";
        페이즈.text = "";
        보충.text = "";
        해설.text = "";
    }

    public void SetIndex()
    {
        페이즈최대인덱스 = -1;

        for (int i = 0; i < DataManager.instance.caseDatas[caseCode].페이즈리스트.Length; i++)
        {
            if (DataManager.instance.caseDatas[caseCode].페이즈리스트[i].페이즈활성여부 == true)
            {
                페이즈최대인덱스++;
            }
        }

        페이즈인덱스 = 페이즈최대인덱스;
    }

    public void SetPhase()
    {
        Debug.Log(페이즈인덱스);

        페이즈대상.text = DataManager.instance.caseDatas[caseCode].사건이름;
        페이즈.text = DataManager.instance.caseDatas[caseCode].페이즈리스트[페이즈인덱스].페이즈이름;
        보충.text = DataManager.instance.caseDatas[caseCode].페이즈리스트[페이즈인덱스].페이즈보충;

        // 해설 여부
        if (DataManager.instance.caseDatas[caseCode].페이즈리스트[페이즈인덱스].페이즈해결여부 == true)
        {
            해설.text = DataManager.instance.caseDatas[caseCode].페이즈리스트[페이즈인덱스].페이즈해설;
        }
        
    }

    public void PhaseNext()
    {
        DeletePhase();

        이전버튼.interactable = true;
        페이즈인덱스++;

        if (페이즈인덱스 >= 페이즈최대인덱스)
        {
            이후버튼.interactable = false;
        }
        else
        {
            이후버튼.interactable = true;
        }

        SetPhase();
    }

    public void PhaseBefore()
    {
        DeletePhase();

        이후버튼.interactable = true;
        페이즈인덱스--;

        if (페이즈인덱스 <= 0)
        {
            이전버튼.interactable = false;
        }
        else
        {
            이전버튼.interactable = true;
        }

        SetPhase();
    }
}
