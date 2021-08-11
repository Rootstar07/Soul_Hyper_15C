using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalkManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Animator talkCanvasAnimator;
    public GameObject talkData;

    int nowNPCCode;
    bool isOpen = false;
    [Space]
    public GameObject[] 페이즈리스트;
    public PeopleManager peopleManager;

    [Header("현재 진행 중인 대화 데이터")]
    public TextMeshProUGUI TalkText;
    public TextMeshProUGUI NameText;
    public Button BeforeButton;
    public Button NextButton;
    public GameObject 대화종료버튼;

    int 현재대화인덱스;
    int 대화최대인덱스;
    bool 기본대화여부 = false;

    // npc코드를 통해 npcData에서의 배열 위치 확인
    int npc인덱스;
    int 케이스인덱스;
    int 페이즈버튼인덱스;
    PhaseInfo 페이즈정보;

    public Animator peopleAnimator;

    [Header("피드백 버블")]
    public GameObject 사건버블;
    public GameObject 페이즈버블;
    public GameObject 인물버블;

    private void Start()
    {
        talkData.SetActive(false);
        DeletePhases();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerMovement.nPCCode != 0)
            {
                if (!isOpen)
                {
                    talkCanvasAnimator.SetBool("isOpen", true);
                    isOpen = true;

                    // 현재 대화중인 NPC의 코드 확인
                    nowNPCCode = playerMovement.nPCCode;

                    playerMovement.speed = 0;

                    FindNPCIndex();

                    FindPhase();

                }
                else
                {
                    DeletePhases();

                    CloseButton();
                }
            }
        }
    }

    // 해당 대화내용에 활성화할 페이즈나 해결할 페이즈를 확인
    public void ScanPhase()
    {
        // 사건 활성
        if (DataManager.instance.nPCDatas[npc인덱스].전체페이스리스트[페이즈정보.페이즈인덱스].대화리스트[현재대화인덱스].활성사건 != 0)
        {
            // 인덱스 추출
            int index =
                (int)DataManager.instance.nPCDatas[npc인덱스].전체페이스리스트[페이즈정보.페이즈인덱스].대화리스트[현재대화인덱스].활성사건 - 1;

            DataManager.instance.caseDatas[index].사건활성여부 = true;

            사건버블.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                DataManager.instance.caseDatas[index].사건이름.ToString();

            사건버블.GetComponent<Animator>().SetTrigger("Alert");
            
        }

        // 페이즈 활성
        if (DataManager.instance.nPCDatas[npc인덱스].전체페이스리스트[페이즈정보.페이즈인덱스].대화리스트[현재대화인덱스].활성페이즈 != 0)
        {
            int index =
                (DataManager.instance.nPCDatas[npc인덱스].전체페이스리스트[페이즈정보.페이즈인덱스].대화리스트[현재대화인덱스].활성페이즈 / 100) - 1;

            for (int i = 0; i < DataManager.instance.caseDatas[index].페이즈리스트.Length; i++)
            {
                if (DataManager.instance.caseDatas[index].페이즈리스트[i].페이즈코드 ==
                    DataManager.instance.nPCDatas[npc인덱스].전체페이스리스트[페이즈정보.페이즈인덱스].대화리스트[현재대화인덱스].활성페이즈)
                {
                    DataManager.instance.caseDatas[index].페이즈리스트[i].페이즈활성여부 = true;

                    페이즈버블.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                        DataManager.instance.caseDatas[index].페이즈리스트[i].페이즈이름;

                    페이즈버블.GetComponent<Animator>().SetTrigger("Alert");

                    break;
                }
            }
        }

        // 페이즈 해결
        if (DataManager.instance.nPCDatas[npc인덱스].전체페이스리스트[페이즈정보.페이즈인덱스].대화리스트[현재대화인덱스].해결페이즈 != 0)
        {
            int index =
                (DataManager.instance.nPCDatas[npc인덱스].전체페이스리스트[페이즈정보.페이즈인덱스].대화리스트[현재대화인덱스].해결페이즈 / 100) - 1;

            for (int i = 0; i < DataManager.instance.caseDatas[index].페이즈리스트.Length; i++)
            {
                if (DataManager.instance.caseDatas[index].페이즈리스트[i].페이즈코드 ==
                    DataManager.instance.nPCDatas[npc인덱스].전체페이스리스트[페이즈정보.페이즈인덱스].대화리스트[현재대화인덱스].해결페이즈)
                {
                    DataManager.instance.caseDatas[index].페이즈리스트[i].페이즈해결여부 = true;
                    break;
                }
            }
        }

        // 인물 활성
        if (DataManager.instance.nPCDatas[npc인덱스].전체페이스리스트[페이즈정보.페이즈인덱스].대화리스트[현재대화인덱스].활성인물 != 0)
        {
            // enum int 변환

            // 인덱스 추출
            int index =
                (int)DataManager.instance.nPCDatas[npc인덱스].전체페이스리스트[페이즈정보.페이즈인덱스].대화리스트[현재대화인덱스].활성인물 - 1;

            DataManager.instance.peopleDatas[index].인물활성여부 = true;

            인물버블.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                DataManager.instance.peopleDatas[index].이름.ToString();

            인물버블.GetComponent<Animator>().SetTrigger("Alert");
        }
    }

    // npcData의 배열 위치 확인
    public void FindNPCIndex()
    {
        for (int i = 0; i < DataManager.instance.nPCDatas.Length; i++)
        {
            if (DataManager.instance.nPCDatas[i].NPC코드 == nowNPCCode)
            {
                npc인덱스 = i;

                break;
            }
        }
    }

    // 배열 위치를 통해 전체페이스 리스트에 참조하여 가능한 페이즈들로 페이즈 오브젝트를 생성
    public void FindPhase()
    {
        for (int i = 0; i < DataManager.instance.nPCDatas[npc인덱스].전체페이스리스트.Length; i++)
        {
            // CaseData에 참조하기 위해 변환
            케이스인덱스 =
                (DataManager.instance.nPCDatas[npc인덱스].전체페이스리스트[i].페이즈코드 / 100) - 1;

            for (int j = 0; j < DataManager.instance.caseDatas[케이스인덱스].페이즈리스트.Length; j++)
            {
                if (DataManager.instance.caseDatas[케이스인덱스].페이즈리스트[j].페이즈코드 ==
                    DataManager.instance.nPCDatas[npc인덱스].전체페이스리스트[i].페이즈코드 &&
                    DataManager.instance.caseDatas[케이스인덱스].페이즈리스트[j].페이즈활성여부 == true)
                {
                    // 페이즈 오브젝트 생성
                    GeneratePhase(j, i);
                }
            }
        }
    }

    public void GeneratePhase(int j, int i)
    {
        페이즈리스트[j].SetActive(true);

        페이즈리스트[j].GetComponent<PhaseInfo>().페이즈인덱스 = i;

        // 페이즈 설명 전달
        페이즈리스트[j].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
            DataManager.instance.caseDatas[케이스인덱스].페이즈리스트[j].페이즈이름;

        페이즈리스트[j].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
            DataManager.instance.caseDatas[케이스인덱스].사건이름.ToString();

    }

    public void DeletePhases()
    {
        npc인덱스 = 0;

        for (int i = 0; i < 페이즈리스트.Length; i++)
        {
            if (페이즈리스트[i].activeSelf == true)
            {
                페이즈리스트[i].GetComponent<PhaseInfo>().페이즈인덱스 = 0;

                페이즈리스트[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";

                페이즈리스트[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";

                페이즈리스트[i].SetActive(false);
            }
        }
    }

    // 페이즈 눌렀을때
    public void PhaseButtonClicked(PhaseInfo phaseinfo)
    {
        페이즈정보 = phaseinfo;
        talkCanvasAnimator.SetBool("isTalk", true);

        // 버튼 초기화
        대화종료버튼.SetActive(false);
        BeforeButton.interactable = false;
        NextButton.interactable = true;

        if (페이즈정보.버튼인덱스 != -1)
        {
            
            페이즈버튼인덱스 = 페이즈정보.버튼인덱스;

            현재대화인덱스 = 0;
            대화최대인덱스 = 
                DataManager.instance.nPCDatas[npc인덱스].전체페이스리스트[페이즈정보.페이즈인덱스].대화리스트.Length;

            // 대화 출력
            UpdateTalk();
        }

        else
        {
            기본대화여부 = true;

            현재대화인덱스 = 0;
            대화최대인덱스 = 
                DataManager.instance.nPCDatas[npc인덱스].기본대화데이터.Length;

            // 클릭 인덱스가 -1이라면 기본 대화를 누른것으로 판정
            UpdateBasicText();
        }

    }

    public void UpdateTalk()
    {
        TalkText.text =
            DataManager.instance.nPCDatas[npc인덱스].전체페이스리스트[페이즈정보.페이즈인덱스].대화리스트[현재대화인덱스].대화데이터;

        NameText.text =
            DataManager.instance.nPCDatas[npc인덱스].전체페이스리스트[페이즈정보.페이즈인덱스].대화리스트[현재대화인덱스].대화중인캐릭터이름;

        ScanPhase();
    }


    public void UpdateBasicText()
    {
        TalkText.text = DataManager.instance.nPCDatas[npc인덱스].기본대화데이터[현재대화인덱스].대화데이터;
        NameText.text = DataManager.instance.nPCDatas[npc인덱스].기본대화데이터[현재대화인덱스].대화중인캐릭터이름;
    }

    public void TalkNext()
    {
        현재대화인덱스++;

        if (현재대화인덱스 >= 대화최대인덱스 - 1)
        {
            NextButton.interactable = false;
            대화종료버튼.SetActive(true);
        }

        BeforeButton.interactable = true;

        if (기본대화여부 == false)
            UpdateTalk();
        else
            UpdateBasicText();

    }

    public void TalkBefore()
    {
        현재대화인덱스--;

        if (현재대화인덱스 <= 0)
        {
            BeforeButton.interactable = false;           
        }

        NextButton.interactable = true;

        대화종료버튼.SetActive(false);

        if (기본대화여부 == false)
            UpdateTalk();
        else
            UpdateBasicText();
    }

    public void EndTalk()
    {
        TalkText.text = "";
        NameText.text = DataManager.instance.nPCDatas[npc인덱스].NPC이름;
        talkCanvasAnimator.SetBool("isTalk", false);
        기본대화여부 = false;
    }

    public void CloseButton()
    {
        talkCanvasAnimator.SetBool("isOpen", false);
        talkCanvasAnimator.SetBool("isTalk", false);
        isOpen = false;

        playerMovement.speed = 15;
    }

}
