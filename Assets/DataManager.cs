using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DataManager : MonoBehaviour
{
    [System.Serializable]
    public class SData
    {
        public bool 인물활성화여부;
        public string 이름;
        public bool 물음표여부;
        public int 현재까지활성화된페이즈수;
        public PlayerData[] 페이즈리스트;
    }

    [System.Serializable]
    public class PlayerData
    {
        public string 페이즈데이터;
        public PlayerDataTest[] 코멘트데이터리스트;
    }

    [System.Serializable]
    public class PlayerDataTest
    {
        public string 코멘트데이터;
        public bool 코멘트활성화여부;
    }

    private void Start()
    {
        ImportJson();
    }



    public void ExportJson()
    {
        // 클래스 json로 변환하게 내보내기
        string jsonData = JsonConvert.SerializeObject(sDatas);
        File.WriteAllText(Application.persistentDataPath + "/saveData.txt", jsonData);
        Debug.Log("내보내기 완료");
    }

    public void ImportJson()
    {
        string data = File.ReadAllText(Application.persistentDataPath + "/saveData.txt");
        sDatas = JsonConvert.DeserializeObject<SData[]>(data);
        Debug.Log(data);
        Debug.Log("임포트 데이터: " + sDatas[0].인물활성화여부);
        Debug.Log("불러오기 완료");
    }
