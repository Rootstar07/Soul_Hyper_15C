using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DataManager : MonoBehaviour
{
    [Header("사건 데이터")]
    public CaseData[] caseDatas;
    [Header("인물 데이터")]
    public PeopleData[] peopleDatas;
    [Header("필드 상의 npc와 대화 관리")]
    public NPCData[] nPCDatas;

    [System.Serializable]
    public class PeopleData
    {
        public string 이름;
        public string 나이;
        public string 신분;
        public bool 물음표여부;
        public bool 인물활성화여부;
        public CharacterState 상태;
        public PeopleData2[] 특이사항리스트;
    }

    [System.Serializable]
    public class PeopleData2
    {
        // 기존의 페이즈 정보는 case에서 관리
        // 여기는 특이사항등을 기술
    }

    //----------------------------------------------

    [System.Serializable]
    public class CaseData
    {
        public int 사건코드;
        public string 사건이름;
        public bool 사건활성여부;
        public CaseData2[] 페이즈리스트;
    }

    [System.Serializable]
    public class CaseData2
    {
        public int 페이즈코드;
        public string 페이즈이름;
        public bool 페이즈활성여부;
        [TextArea]
        public string 페이즈보충;
        public bool 페이즈해결여부;
        [TextArea]
        public string 페이즈해설;
    }

    //----------------------------------------------

    [System.Serializable]
    public class NPCData
    {
        public int NPC코드;
        public string NPC이름;
        public NPCData3[] 기본대화데이터;
        [Space]
        public NPCData2[] 전체페이스리스트;
    }

    [System.Serializable]
    public class NPCData2
    {
        public int 페이즈코드;
        public NPCData3[] 대화리스트;
    }

    [System.Serializable]
    public class NPCData3
    {
        public string 대화중인캐릭터이름;
        public State 표정;
        [TextArea]
        public string 대화데이터;
        public int 활성페이즈;
        public int 해결페이즈;
    }

    //----------------------------------------------

    // 상태
    public enum CharacterState { 정상, 실신, 실종, 사망 }

    // 표정
    public enum State { 기본, 놀람, 분노, 슬픔 }

    public static DataManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void ExportData()
    {
        string jsonData0 = JsonConvert.SerializeObject(caseDatas);
        File.WriteAllText(Application.persistentDataPath + "/basicData.json", jsonData0);

        string jsonData1 = JsonConvert.SerializeObject(nPCDatas);
        File.WriteAllText(Application.persistentDataPath + "/NPCData.json", jsonData1);

        Debug.Log("데이터 내보내기 완료");
    }

    public void ImportData()
    {
        string data0 = File.ReadAllText(Application.persistentDataPath + "/basicData.json");
        caseDatas = JsonConvert.DeserializeObject<CaseData[]>(data0);

        string data1 = File.ReadAllText(Application.persistentDataPath + "/NPCData.json");
        nPCDatas = JsonConvert.DeserializeObject<NPCData[]>(data1);

        Debug.Log("데이터 불러오기 완료");
    }

}
