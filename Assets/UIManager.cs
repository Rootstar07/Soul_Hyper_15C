using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject 사람;
    public GameObject[] 사람리스트;
    [Space]
    public GameObject 사건;
    public GameObject[] 사건리스트;
    [Space]
    public GameObject 페이즈오브젝트;
    [Space]
    public GameObject 배경;
    public Animator animator;
    [Space]
    public Color 정상;
    public Color 실신;
    public Color 실종;
    public Color 사망;



    // 단순히 사건 또는 인물이 관계도에 뜨는지 안뜨는지만 검사하고 표시
    // 상세정보는 CaseManager에서 진행

    private void Start()
    {
        for (int i = 0; i < 사람.transform.childCount; i++)
        {
            사람리스트[i] = 사람.transform.GetChild(i).gameObject;
            사람리스트[i].SetActive(false);
        }

        for (int i = 0; i < 사건.transform.childCount; i++)
        {
            사건리스트[i] = 사건.transform.GetChild(i).gameObject;
            사람리스트[i].SetActive(false);
        }
    }

    // ----------- UI 버튼 관리 -----------

    // 인물 초기와 배경 클릭 및 닫기 버튼 누를때 사용
    public void DeletePeople()
    {
        페이즈오브젝트.SetActive(false);
    }

    public void OpenCloseUI()
    {
        DeletePeople();

        if (배경.activeSelf)
        {
            // 캔버스 닫기
            animator.SetBool("isOpen", false);
        }
        else
        {
            // 캔버스 열기
            배경.SetActive(true);
            animator.SetBool("isOpen", true);

            UpdateList();

        }
    }

    public void UpdateList()
    {
        // 사건 표시 관리
        for (int i = 0; i < DataManager.instance.caseDatas.Length; i++)
        {
            if (DataManager.instance.caseDatas[i].사건활성여부 == true)
            {
                사건리스트[i].SetActive(true);
            }
            else
            {
                사건리스트[i].SetActive(false);
            }
        }

        // 사람 표시 관리
        for (int i = 0; i < DataManager.instance.peopleDatas.Length; i++)
        {
            if (DataManager.instance.peopleDatas[i].인물활성여부 == true)
            {
                사람리스트[i].SetActive(true);

                // 이름 넣기
                사람리스트[i].transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    DataManager.instance.peopleDatas[i].이름.ToString();

                // 상태 관리
                switch (DataManager.instance.peopleDatas[i].상태)
                {
                    case DataManager.CharacterState.실신:
                        사람리스트[i].transform.GetChild(0).transform.GetChild(2).GetComponent<Image>().color = 실신;
                        사람리스트[i].transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                            "실신";
                        break;
                    case DataManager.CharacterState.실종:
                        사람리스트[i].transform.GetChild(0).transform.GetChild(2).GetComponent<Image>().color = 실종;
                        사람리스트[i].transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                            "실종";
                        break;
                    case DataManager.CharacterState.사망:
                        사람리스트[i].transform.GetChild(0).transform.GetChild(2).GetComponent<Image>().color = 사망;
                        사람리스트[i].transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                            "사망";
                        break;
                    case DataManager.CharacterState.정상:
                        사람리스트[i].transform.GetChild(0).transform.GetChild(2).GetComponent<Image>().color = 정상;
                        사람리스트[i].transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                            "";
                        break;

                }
            }
            else
            {
                사람리스트[i].SetActive(false);
            }
        }

    }
}
