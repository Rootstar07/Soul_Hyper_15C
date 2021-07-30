using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;


public class PeopleManager : MonoBehaviour
{
    [Header("모든 인물데이터의 리스트")]
    public GameObject[] peopleList; // 관계도 활성화 여부는 배열 데이터의 값에 따라 정해짐
    [Header("코멘트 리스트")]
    public GameObject[] commentList;
    [Space]
    public GameObject phaseObject;
    public TextMeshProUGUI phaseText;

    [Header("클릭한 인물의 정보")]
    public int 클릭한인물코드;
    public int 클릭한인물의활성화된페이즈개수;
    public int 현재페이즈코드;
    //public PhaseInfo[] 클릭한인물의전체페이즈;
    [Space]
    public Button beforeButton;
    public Button nextButton;

    public int 페이즈카운터 = 0;


    void Start()
    {
        // 리스트에 인물 데이터 넣기
        for (int i = 0; i < transform.childCount; i++)
        {
            peopleList[i] = transform.GetChild(i).gameObject;
        }

        // 시작할때 한번 업데이트
        phaseObject.SetActive(false);

        UpdatePeople();

    }

    // 관계도에 표시 관리
    public void UpdatePeople()
    {
        for (int i =0; i< DataManager.instance.basicDatas.Length; i++)
        {
            
            if (DataManager.instance.basicDatas[i].인물활성화여부 == true)
            {
                peopleList[i].SetActive(true);

                // 이름관리
                UpdateName(i);
            }

            else
            {
                peopleList[i].SetActive(false);
            }
        }
    }
    
    // 이름 혹은 물음표 관리
    void UpdateName(int code)
    {
        if (DataManager.instance.basicDatas[code].물음표여부 == true)
        {
            peopleList[code].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "?";
        }
        else
        {
            peopleList[code].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = DataManager.instance.basicDatas[code].이름;
        }
    }

    public void ClickedPeople(int code)
    {
        DeletePeople();
        클릭한인물코드 = code;

        for (int i =0; i < DataManager.instance.basicDatas[code].페이즈리스트.Length; i++)
        {
            if (DataManager.instance.basicDatas[code].페이즈리스트[i].페이즈활성여부 == true)
            {
                페이즈카운터++;
            }
        }

        클릭한인물의활성화된페이즈개수 = 페이즈카운터;
        현재페이즈코드 = 클릭한인물의활성화된페이즈개수 - 1;

        // 이 함수가 실행되었다는 것은 최소한 클릭이 되었다는 의미이므로 여기서 오브젝트의 투명도를 결정
        if (클릭한인물코드 != -1)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i != 클릭한인물코드)
                {
                    transform.GetChild(i).GetComponent<Animator>().SetBool("isFade", true);

                    // 페이즈 초기화 및 버튼 할당
                    PhaseButtonGenerator();
                }
                else
                {
                    transform.GetChild(i).GetComponent<Animator>().SetBool("isFade", false);
                }
            }

        }
    }

    // 인물 초기와 배경 클릭 및 닫기 버튼 누를때 사용
    public void DeletePeople()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Animator>().SetBool("isFade", false);
            클릭한인물코드 = 0;
            페이즈카운터 = 0;
            phaseObject.SetActive(false);
        }
    }

    public void PhaseButtonGenerator()
    {
        // 인물 클릭됌 페이즈 활성화 후 현재 페이즈 전달
        phaseObject.SetActive(true);
        phaseText.text = DataManager.instance.basicDatas[클릭한인물코드].페이즈리스트[현재페이즈코드].페이즈데이터;
        UpdateComment();

        // 버튼 기본 설정
        nextButton.interactable = false;
        if (현재페이즈코드 > 0)
        {           
            beforeButton.interactable = true;
        }
        else
        {
            beforeButton.interactable = false;
        }
    }

    public void PressPhaseNext()
    {
        현재페이즈코드 = 현재페이즈코드 + 1;
        phaseText.text = DataManager.instance.basicDatas[클릭한인물코드].페이즈리스트[현재페이즈코드].페이즈데이터;
        // 코멘트 활성화
        UpdateComment();

        beforeButton.interactable = true;
        if (현재페이즈코드 == 클릭한인물의활성화된페이즈개수-1)
        {
            nextButton.interactable = false;
        }
    }

    public void PressPhaseBefore()
    {
        현재페이즈코드 = 현재페이즈코드  - 1;
        phaseText.text = DataManager.instance.basicDatas[클릭한인물코드].페이즈리스트[현재페이즈코드].페이즈데이터;
        // 코멘트 활성화
        UpdateComment();

        nextButton.interactable = true;
        if (현재페이즈코드 < 1)
        {
            beforeButton.interactable = false;
        }
    }

    public void UpdateComment()
    {
        DeleteComment();

        for (int i = 0; i < DataManager.instance.basicDatas[클릭한인물코드].페이즈리스트[현재페이즈코드].코멘트데이터리스트.Length; i++)
        {
                if (DataManager.instance.basicDatas[클릭한인물코드].페이즈리스트[현재페이즈코드].코멘트데이터리스트[i].코멘트활성화여부 == true)
                {
                commentList[i].GetComponent<TextMeshProUGUI>().text =
                    DataManager.instance.basicDatas[클릭한인물코드].페이즈리스트[현재페이즈코드].코멘트데이터리스트[i].코멘트데이터;
                }
                else
                {
                    commentList[i].GetComponent<TextMeshProUGUI>().text = "";
                }
        }
    }
    public void DeleteComment()
    {
        for (int i =0; i< 6; i++)
        {
            commentList[i].GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
