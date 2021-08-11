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
        //public int 인물코드;
        public People 이름;
        public bool 인물활성여부;
        public string 나이;
        public Identity 신분;
        public CharacterState 상태;
        public PeopleData2[] 특이사항리스트;
    }

    [System.Serializable]
    public class PeopleData2
    {
        public bool 특이사항활성여부;
        [TextArea]
        public string 특이사항내용;
    }

    //----------------------------------------------

    [System.Serializable]
    public class CaseData
    {
        //public int 사건코드;
        public Case 사건이름;
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
        [Header("활성화할 코드, 활성사건, 활성인물은 코드에 + 1을 할것")]
        public Case 활성사건;
        public int 활성페이즈;
        public int 해결페이즈;
        public People 활성인물;
    }

    //----------------------------------------------

    // 상태
    public enum CharacterState { 정상, 실신, 실종, 사망 }

    // 표정
    public enum State { 기본, 놀람, 분노, 슬픔 }

    // 신분
    public enum Identity { 백정, 노비, 농민, 기생, 무당, 양반, 행인 }

    // 인물리스트
    public enum People
    {
        없음,
        한백정,
        강기생,
        박농민,
        아기무당씨,
        하영,
        메건,
        롬쌔,
        션
    }

    // 사건리스트
    public enum Case
    {
        없음,
        돼지머리,
        정승의비밀
    }

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

        string jsonData2 = JsonConvert.SerializeObject(peopleDatas);
        File.WriteAllText(Application.persistentDataPath + "/peopleDatas.json", jsonData2);

        Debug.Log("데이터 내보내기 완료");
    }

    public void ImportData()
    {
        string data0 = File.ReadAllText(Application.persistentDataPath + "/basicData.json");
        caseDatas = JsonConvert.DeserializeObject<CaseData[]>(data0);

        string data1 = File.ReadAllText(Application.persistentDataPath + "/NPCData.json");
        nPCDatas = JsonConvert.DeserializeObject<NPCData[]>(data1);

        string data2 = File.ReadAllText(Application.persistentDataPath + "/peopleDatas.json");
        nPCDatas = JsonConvert.DeserializeObject<NPCData[]>(data2);

        Debug.Log("데이터 불러오기 완료");
    }

}
