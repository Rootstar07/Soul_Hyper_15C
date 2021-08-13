using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
//using NaughtyAttributes;

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
        public Identity 신분;
        public bool 인물활성여부;
        [Range(1,100)]
        public int 나이;
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
        //[HorizontalLine(color: EColor.Red)]
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
        //[HorizontalLine(color: EColor.Yellow)]
        public int NPC코드;
        public NPC NPC이름;
        public NPCData3[] 기본대화데이터;
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
        //[HorizontalLine(color: EColor.Blue)]
        public NPC 대화중인캐릭터이름;
        public State 표정;
        [TextArea]
        public string 대화데이터;
        [Space]
        public bool 활성가능;
        [Space]
        public Case 활성사건;
        public People 활성인물;
        public int 활성페이즈;
        public int 해결페이즈;
    }

    //----------------------------------------------

    // 상태
    public enum CharacterState { 정상, 실신, 실종, 사망 }

    // 표정
    public enum State { 기본, 놀람, 분노, 슬픔 }

    // 신분
    public enum Identity { 백정, 노비, 농민, 기생, 무당, 양반, 행인, 의원 }

    // 인물리스트
    public enum People
    {
        없음,
        하진,
        하설,
        사월,
        막개,
        아람,
        임대혁,
        임홍결,
        오산,
        사비울,
        사혜,
        설룡,
        두루,
    }

    public enum NPC
    {
        설룡,
        두루,
        하진,
        하설,
        사월,
        막개,
        아람,
        임대혁,
        임홍결,
        오산,
        사비울,
        사혜,
        농민,
        노비,
        의원,
        선비
    }

    // 사건리스트
    public enum Case
    {
        없음,
        돼지머리,
        정승의비밀,
        무당의죽음,
        외발귀신의비밀
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
