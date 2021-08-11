using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public PeopleManager peopleManager;
    public Button commentSaveButton;

    [Header("현재 진행 중인 대화 데이터")]
    public TextMeshProUGUI TalkText;
    public TextMeshProUGUI NameText;
    public Button BeforeButton;
    public Button NextButton;
    public GameObject 대화종료버튼;
    public int 현재페이즈인덱스;
    public int 현재대화인덱스;
    public int 대화최대인덱스;
    public int 현재베이직데이터인덱스;
    public int 현재베이직페이즈인덱스;
    public int 현재저장된코멘트데이터인덱스;
    public bool 기본대화여부 = false;

    /*
    [Header("오브젝트 데이터")]
    public int nowObjectCode;
    public GameObject 오브젝트대화;
    public TextMeshProUGUI TalkText2;
    public TextMeshProUGUI NameText2;
    public int 현재오브젝트데이터인덱스;
    public int 현재오브젝트대화인덱스;
    */
    public Animator peopleAnimator;

    [TextArea]
    public string 현재대화데이터;

    private void Start()
    {
        talkData.SetActive(false);
        DeletePhase();
    }

    private void Update()
    {
        // 대화 첫 실행
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerMovement.nPCCode != 0)
            {
                if (!isOpen)
                {
                    talkCanvasAnimator.SetBool("isOpen", true);
                    isOpen = true;

                    // 현재 대화중인 NPC의 코드 확인
                    nowNPCCOde = playerMovement.nPCCode;
                    playerMovement.speed = 0;

                    _UpdatePhaseData();
                }
                else
                {
                    CloseButton();
                }
            }
            /*

            if (playerMovement.objectCode != 0)
            {
                Debug.Log("오브젝트 대화 실행");  
                playerMovement.speed = 0;
                nowObjectCode = playerMovement.objectCode;

                if (오브젝트대화.activeSelf == false)
                {
                    오브젝트대화.SetActive(true);
                    SetObjectTalk();
                }
            }
            */
        }
    }

    public void _UpdatePhaseData()
    {
        DeletePhase();

        // 코드에 따른 페이즈 생성

        for (int i = 0; i < DataManager.instance.nPCDatas.Length; i++)
        {
            if (DataManager.instance.nPCDatas[i].NPC코드 == nowNPCCOde)
            {
                GeneratePhase(i);
                break;
            }
        }
    }

    /*
    public void SetObjectTalk()
    {
        for (int i = 0; i < DataManager.instance.objectDatas.Length; i++)
        {
            if (DataManager.instance.objectDatas[i].오브젝트코드 == nowObjectCode)
            {
                현재오브젝트데이터인덱스 = i;
                GenerateObjectTalk();
                CheckPhaseUpdate();

                break;
            }
        }
    }
    

    public void ObjectNextClick()
    {
        if (DataManager.instance.objectDatas[현재오브젝트데이터인덱스].오브젝트대화리스트.Length - 1 > 현재오브젝트대화인덱스)
        {
            현재오브젝트대화인덱스++;
            TalkText2.text =
                DataManager.instance.objectDatas[현재오브젝트데이터인덱스].오브젝트대화리스트[현재오브젝트대화인덱스].대화데이터;
            NameText2.text =
                DataManager.instance.objectDatas[현재오브젝트데이터인덱스].오브젝트대화리스트[현재오브젝트대화인덱스].대화중인캐릭터이름;

            CheckPhaseUpdate();
        }
        else
        {
            Debug.Log("대화종료");
            playerMovement.speed = 15;

            오브젝트대화.SetActive(false);
            현재오브젝트데이터인덱스 = 0;
            현재오브젝트대화인덱스 = 0;
        }
    }
    


    public void GenerateObjectTalk()
    {
        현재오브젝트대화인덱스 = 0;
        TalkText2.text = 
            DataManager.instance.objectDatas[현재오브젝트데이터인덱스].오브젝트대화리스트[현재오브젝트대화인덱스].대화데이터;
        NameText2.text =
            DataManager.instance.objectDatas[현재오브젝트데이터인덱스].오브젝트대화리스트[현재오브젝트대화인덱스].대화중인캐릭터이름;
    }

    public void CheckPhaseUpdate()
    {
        if (DataManager.instance.objectDatas[현재오브젝트데이터인덱스].오브젝트대화리스트[현재오브젝트대화인덱스].활성화할페이즈 != 0 &&
            DataManager.instance.objectDatas[현재오브젝트데이터인덱스].습득한정보여부 == false)
        {
            현재베이직데이터인덱스 = (DataManager.instance.objectDatas[현재오브젝트데이터인덱스].오브젝트대화리스트[현재오브젝트대화인덱스].활성화할페이즈 / 100) - 1;

            for (int j = 0; j < DataManager.instance.basicDatas[현재베이직데이터인덱스].페이즈리스트.Length; j++)
            {
                if (DataManager.instance.basicDatas[현재베이직데이터인덱스].페이즈리스트[j].페이즈코드 ==
                    DataManager.instance.objectDatas[현재오브젝트데이터인덱스].오브젝트대화리스트[현재오브젝트대화인덱스].활성화할페이즈)
                {
                    DataManager.instance.objectDatas[현재오브젝트데이터인덱스].습득한정보여부 = true;
                    DataManager.instance.basicDatas[현재베이직데이터인덱스].인물활성화여부 = true;
                    DataManager.instance.basicDatas[현재베이직데이터인덱스].페이즈리스트[j].페이즈활성여부 = true;

                    break;
                }
            }
            peopleAnimator.SetTrigger("isNewInformation");
        }
    }
    */


    // 열리자마자 실행
    public void GeneratePhase(int x)
    {
        현재NPCData배열위치 = x;

        // 기본 npc 이름 및 대사 전달
        TalkText.text = "";
        NameText.text = DataManager.instance.nPCDatas[현재NPCData배열위치].NPC이름;

        for (int i = 0; i < DataManager.instance.nPCDatas[현재NPCData배열위치].전체페이스리스트.Length; i++)
        {
            int npc페이즈코드 = DataManager.instance.nPCDatas[현재NPCData배열위치].전체페이스리스트[i].페이즈코드;
            현재베이직데이터인덱스 = (npc페이즈코드 / 100) - 1;

            for (int j = 0; j < DataManager.instance.basicDatas[현재베이직데이터인덱스].페이즈리스트.Length; j++)
            {               
                int phaseCode = DataManager.instance.basicDatas[현재베이직데이터인덱스].페이즈리스트[j].페이즈코드;

                if (npc페이즈코드 == phaseCode &&
                    DataManager.instance.basicDatas[현재베이직데이터인덱스].페이즈리스트[j].페이즈활성여부 == true)
                {
                    현재베이직페이즈인덱스 = j;

                    페이즈리스트[i].SetActive(true);

                    // 페이즈 내용 변경
                    페이즈리스트[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                        DataManager.instance.basicDatas[현재베이직데이터인덱스].페이즈리스트[j].페이즈데이터;

                    // 페이즈 이름 변경
                    페이즈리스트[i].transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
                        DataManager.instance.basicDatas[현재베이직데이터인덱스].이름;

                    // 각 페이즈에 인덱스 정보 저장
                    페이즈리스트[i].GetComponent<PhaseInfo>().페이즈인덱스 = i;

                    break;
                }
            }
        }
    }

    // 초기에 페이즈 버튼 눌렀을때
    public void PhaseButtonClicked(int clickedIndex)
    {

        if (clickedIndex != -1)
        {
            talkCanvasAnimator.SetBool("isTalk", true);
            현재페이즈인덱스 = clickedIndex;

            // 현재 진행 중인 대화의 위치
            // string x = DataManager.instance.nPCDatas[현재NPCData배열위치].가능한페이즈리스트[i].저장한대화;

            현재대화인덱스 = 0;
            대화최대인덱스 = DataManager.instance.nPCDatas[현재NPCData배열위치].전체페이스리스트[현재페이즈인덱스].대화리스트.Length;

            UpdateTalkData();

            CheckPhaseData();

            // NPC Data의 페이즈 코드를 받아서 대화 정보를 저장할 캐릭터의 위치 확인
            현재베이직데이터인덱스 = (DataManager.instance.nPCDatas[현재NPCData배열위치].전체페이스리스트[현재페이즈인덱스].페이즈코드 / 100) - 1;

            // 메모 저장 확인
            SaveButtonCheck();

            대화종료버튼.SetActive(false);
            NextButton.interactable = true;
            BeforeButton.interactable = false;
        }

        else
        {
            기본대화여부 = true;
            // 클릭 인덱스가 -1이라면 기본 대화를 누른것으로 판정
            GenerateBasicText();
        }

    }


    public void GenerateBasicText()
    {
        // 캔버스 열기 나중에 메모 버튼 없는 애니메이션 혹은 기본 대화 전용 애니메이션 만들자
        talkCanvasAnimator.SetBool("isBasicTalk", true);
        현재대화인덱스 = 0;
        대화최대인덱스 = DataManager.instance.nPCDatas[현재NPCData배열위치].기본대화데이터.Length;
        대화종료버튼.SetActive(false);

        NextButton.interactable = true;
        대화종료버튼.SetActive(false);

        UpdateTalkData2();

    }

    public void UpdateTalkData2()
    {
        TalkText.text = DataManager.instance.nPCDatas[현재NPCData배열위치].기본대화데이터[현재대화인덱스].대화데이터;
        NameText.text = DataManager.instance.nPCDatas[현재NPCData배열위치].기본대화데이터[현재대화인덱스].대화중인캐릭터이름;
    }

    public void TalkNext2()
    {
        현재대화인덱스++;

        if (현재대화인덱스 >= 대화최대인덱스 - 1)
        {
            // 다음 버튼 비활성화
            NextButton.interactable = false;

            // 대화 종료 활성화
            대화종료버튼.SetActive(true);
        }
        else
        {
            NextButton.interactable = true;

            대화종료버튼.SetActive(false);
        }

        UpdateTalkData2();

        CheckPhaseData2();
    }

    
    public void CheckPhaseData2()
    {
        if (DataManager.instance.nPCDatas[현재NPCData배열위치].기본대화데이터[현재대화인덱스].활성화할페이즈 != 0)
        {
            int index = DataManager.instance.nPCDatas[현재NPCData배열위치].기본대화데이터[현재대화인덱스].활성화할페이즈 / 100 - 1;

            for (int j = 0; j < DataManager.instance.basicDatas[index].페이즈리스트.Length; j++)
            {
                if (DataManager.instance.basicDatas[index].페이즈리스트[j].페이즈코드 ==
                    DataManager.instance.nPCDatas[현재NPCData배열위치].기본대화데이터[현재대화인덱스].활성화할페이즈
                    && DataManager.instance.basicDatas[index].페이즈리스트[j].페이즈활성여부 == false)
                {
                    DataManager.instance.basicDatas[index].인물활성화여부 = true;
                    DataManager.instance.basicDatas[index].페이즈리스트[j].페이즈활성여부 = true;

                    peopleAnimator.SetTrigger("isNewInformation");

                    break;
                }
            }
        }
    }
    

    public void AddComment()
    {
        // 대화 데이터 전달
        DataManager.instance.nPCDatas[현재NPCData배열위치].전체페이스리스트[현재페이즈인덱스].저장한대화 =
            DataManager.instance.nPCDatas[현재NPCData배열위치].전체페이스리스트[현재페이즈인덱스].대화리스트[현재대화인덱스].대화데이터;


        for (int i = 0; i < DataManager.instance.basicDatas[현재베이직데이터인덱스].페이즈리스트.Length; i++)
        {
            if (DataManager.instance.basicDatas[현재베이직데이터인덱스].페이즈리스트[i].페이즈코드 ==
                DataManager.instance.nPCDatas[현재NPCData배열위치].전체페이스리스트[현재페이즈인덱스].페이즈코드)
            {
                // 위치를 한번이라도 지정하지 않았다면 위치 지정
                if (DataManager.instance.nPCDatas[현재NPCData배열위치].전체페이스리스트[현재페이즈인덱스].저장된대화위치 == 0)
                {
                    for (int j = 0; j < DataManager.instance.basicDatas[현재베이직데이터인덱스].페이즈리스트[i].코멘트데이터.Length; j++)
                    {
                        // 빈 데이터 확인
                        if (DataManager.instance.basicDatas[현재베이직데이터인덱스].페이즈리스트[i].코멘트데이터[j] == "")
                        {
                            DataManager.instance.nPCDatas[현재NPCData배열위치].전체페이스리스트[현재페이즈인덱스].저장된대화위치 = j + 1;                          
                            break;
                        }
                    }
                }

                현재저장된코멘트데이터인덱스 = DataManager.instance.nPCDatas[현재NPCData배열위치].전체페이스리스트[현재페이즈인덱스].저장된대화위치 - 1;

                // 버튼 저장 여부 초기화
                for (int n = 0; n < DataManager.instance.nPCDatas[현재NPCData배열위치].전체페이스리스트[현재페이즈인덱스].대화리스트.Length; n ++)
                {
                    DataManager.instance.nPCDatas[현재NPCData배열위치].전체페이스리스트[현재페이즈인덱스].대화리스트[n].저장한대화여부 = false;
                }

                // 버튼 저장 여부 정보 전달
                DataManager.instance.nPCDatas[현재NPCData배열위치].전체페이스리스트[현재페이즈인덱스].대화리스트[현재대화인덱스].저장한대화여부 = true;

                DataManager.instance.basicDatas[현재베이직데이터인덱스].페이즈리스트[i].코멘트데이터[현재저장된코멘트데이터인덱스] =
                    DataManager.instance.nPCDatas[현재NPCData배열위치].전체페이스리스트[현재페이즈인덱스].대화리스트[현재대화인덱스].대화데이터;

                SaveButtonCheck();

                break;
            }
        }
    }

    public void SaveButtonCheck()
    {
        if (DataManager.instance.nPCDatas[현재NPCData배열위치].전체페이스리스트[현재페이즈인덱스].대화리스트[현재대화인덱스].저장한대화여부 == true)
        {
            commentSaveButton.interactable = false;
        }
        else
        {
            commentSaveButton.interactable = true;
        }
    }

    public void TalkNext()
    {
        if (기본대화여부 == false)
        {
            {
                현재대화인덱스++;

                if (현재대화인덱스 >= 대화최대인덱스 - 1)
                {
                    // 다음 버튼 비활성화
                    NextButton.interactable = false;

                    // 대화 종료 활성화
                    대화종료버튼.SetActive(true);
                }

                SaveButtonCheck();

                BeforeButton.interactable = true;

                UpdateTalkData();

                CheckPhaseData();
            }
        }


        else
        {
            TalkNext2();
        }

    }

    public void TalkBefore()
    {
        현재대화인덱스--;
        if (현재대화인덱스 <= 0)
        {
            // 이전 버튼 비활성화
            BeforeButton.interactable = false;           
        }

        NextButton.interactable = true;
        대화종료버튼.SetActive(false);

        SaveButtonCheck();

        TalkText.text = "";
        NameText.text = DataManager.instance.nPCDatas[현재NPCData배열위치].NPC이름;
        UpdateTalkData();
    }

    public void EndTalk()
    {
        _UpdatePhaseData();
        TalkText.text = "";
        NameText.text = DataManager.instance.nPCDatas[현재NPCData배열위치].NPC이름;
        talkCanvasAnimator.SetBool("isTalk", false);
        talkCanvasAnimator.SetBool("isBasicTalk", false);
        기본대화여부 = false;
    }

    // 새로운 페이즈와 인물이 있다면 여기서 업데이트
    public void CheckPhaseData()
    {
        if (DataManager.instance.nPCDatas[현재NPCData배열위치].전체페이스리스트[현재페이즈인덱스].대화리스트[현재대화인덱스].활성화할페이즈 != 0)
        {          
            int index = DataManager.instance.nPCDatas[현재NPCData배열위치].전체페이스리스트[현재페이즈인덱스].대화리스트[현재대화인덱스].활성화할페이즈 / 100 - 1;

            for (int j = 0; j < DataManager.instance.basicDatas[index].페이즈리스트.Length; j++)
            {
                if (DataManager.instance.basicDatas[index].페이즈리스트[j].페이즈코드 ==
                    DataManager.instance.nPCDatas[현재NPCData배열위치].전체페이스리스트[현재페이즈인덱스].대화리스트[현재대화인덱스].활성화할페이즈 
                    && DataManager.instance.basicDatas[index].페이즈리스트[j].페이즈활성여부 == false)
                {
                    DataManager.instance.basicDatas[index].인물활성화여부 = true;
                    DataManager.instance.basicDatas[index].페이즈리스트[j].페이즈활성여부 = true;

                    peopleAnimator.SetTrigger("isNewInformation");

                    break;
                }
            }
        }
    }

    public void UpdateTalkData()
    {
        현재대화데이터 = DataManager.instance.nPCDatas[현재NPCData배열위치].전체페이스리스트[현재페이즈인덱스].대화리스트[현재대화인덱스].대화데이터;
        TalkText.text = 현재대화데이터;
        NameText.text = DataManager.instance.nPCDatas[현재NPCData배열위치].전체페이스리스트[현재페이즈인덱스].대화리스트[현재대화인덱스].대화중인캐릭터이름;
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
        talkCanvasAnimator.SetBool("isTalk", false);
        isOpen = false;

        playerMovement.speed = 15;
    }

}
