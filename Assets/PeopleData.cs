using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PeopleData : MonoBehaviour
{
    public PhaseData phasedata;
    public string 이름;
    public bool 물음표여부;
    [TextArea]
    public string 인물설명;
    [Header("인물 설명 타켓")]
    public TextMeshProUGUI targetText;
    [Space]
    [Header("페이즈 관리 최대 8개")]
    public int 현재페이즈;
    [Space]
    [TextArea]
    public string[] 페이즈;

    GameObject[] list;

    private void Start()
    {
        deletePhase();
    }

    public void PhaseOn()
    {     
        deletePhase();
        list = phasedata.phaseList;

        //설명 변경
        targetText.text = 인물설명;

        for (int i =0; i< 현재페이즈; i++)
        {
            // 활성화 + 존재하는 페이즈를 활성화하고 텍스트 전달
            list[i].SetActive(true);
            list[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 페이즈[i];
        }
    }

    public void deletePhase()
    {
        list = phasedata.phaseList;
        for (int i = 0; i < list.Length; i++)
        {
            list[i].SetActive(false);
        }
    }
}
