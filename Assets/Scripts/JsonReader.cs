using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public TextAsset Data;
    //public TextAsset PhaseData;

    [System.Serializable]
    public class Index
    {
        public int 이름코드;
        public string 이름;
        public string 인물설명;
        public bool 활성화여부;
    }

    /*
    [System.Serializable]
    public class PhaseIndex
    {

    }
    */

    [System.Serializable]
    public class IndexList
    {
        public Index[] pIndex;
    }

    /*
    [System.Serializable]
    public class PhaseList
    {
        public PhaseIndex[] phaseIndex;
    }
    */

    public IndexList myIndexList = new IndexList();
    /*
    [Space]
    public PhaseList myPhaseList = new PhaseList();
    */

    void Start()
    {
        myIndexList = JsonUtility.FromJson<IndexList>(Data.text);
        //myPhaseList = JsonUtility.FromJson<PhaseList>(PhaseData.text);
    }


}
