﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DataManager : MonoBehaviour
{
    public BasicData[] basicDatas;
    [Space]
    public NPCData[] nPCDatas;

    [System.Serializable]
    public class BasicData
    {
        public bool 인물활성화여부;
        public string 이름;
        public CharacterState 상태;
        public bool 물음표여부;     
        public PlayerData[] 페이즈리스트;
    }

    public enum CharacterState { 정상, 실신, 실종 , 사망 }

    [System.Serializable]
    public class PlayerData
    {
        public int 페이즈코드;
        public bool 페이즈활성여부;
        [TextArea]
        public string 페이즈데이터;
        [TextArea]
        public string[] 코멘트데이터;
    }

    [System.Serializable]
    public class PlayerDataTest
    {
        [TextArea]
        public string 코멘트데이터;
        public bool 코멘트활성화여부;
    }

    [System.Serializable]
    public class NPCData
    {
        public int NPC코드;
        public string NPC이름;
        [TextArea]
        public string NPC기본대사;
        [Space]
        public PhaseData[] 가능한페이즈리스트;
    }

    [System.Serializable]
    public class PhaseData
    {
        public int 페이즈코드;
        public int 저장된대화위치;
        [TextArea]
        public string 저장한대화;
        public bool 새로운정보여부;
        [Space]
        public TalkData[] 대화리스트;
    }

    [System.Serializable]
    public class TalkData
    {
        public string 대화중인캐릭터이름;
        public bool 저장한대화여부;
        [TextArea]
        public string 대화데이터;
        public State 표정;
        public int 활성화할페이즈;
        public int 활상화할오브젝트;
    }

    public enum State { 기본, 놀람, 분노, 슬픔 }

    public static DataManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void ExportData()
    {
        string jsonData0 = JsonConvert.SerializeObject(basicDatas);
        File.WriteAllText(Application.persistentDataPath + "/basicData.json", jsonData0);

        string jsonData1 = JsonConvert.SerializeObject(nPCDatas);
        File.WriteAllText(Application.persistentDataPath + "/NPCData.json", jsonData1);

        Debug.Log("데이터 내보내기 완료");
    }

    public void ImportData()
    {
        string data0 = File.ReadAllText(Application.persistentDataPath + "/basicData.json");
        basicDatas = JsonConvert.DeserializeObject<BasicData[]>(data0);

        string data1 = File.ReadAllText(Application.persistentDataPath + "/NPCData.json");
        nPCDatas = JsonConvert.DeserializeObject<NPCData[]>(data1);

        Debug.Log("데이터 불러오기 완료");
    }

}