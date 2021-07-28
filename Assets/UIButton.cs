using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIButton : MonoBehaviour
{
    public GameObject targetCanvas;
    public JsonReader json;
    public GameObject[] peopleList;
    public bool[] activeList;

    public void OpenCloseUI()
    {
        if (targetCanvas.activeSelf)
        {
            // 캔버스 닫기
            targetCanvas.SetActive(false);
        }
        else
        {
            // 캔버스 열기
            targetCanvas.SetActive(true);

            // 일단 ui창을 열때마다 업데이트 나중에 대화에 맡기자
            UpdatePeople();
        }
    }

    // 관계도 사람 업데이트
    public void UpdatePeople()
    {
        for (int i = 0; i< activeList.Length; i++)
        {
            if (activeList[i] == true)
            {
                peopleList[i].SetActive(true);

                // 이름 전달
                if (peopleList[i].GetComponent<PeopleData>().물음표여부 == true)
                {
                    peopleList[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "?";
                }
                else
                {
                    peopleList[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                        peopleList[i].GetComponent<PeopleData>().이름;
                }


            }
            else
            {
                peopleList[i].SetActive(false);
            }
        }

        // 이름 업데이트



        /*
        for (int i =0; i < peopleList.Length; i++)
        {
            if (json.myIndexList.pIndex[i].활성화여부 == true)
            {
                peopleList[i].SetActive(true);
            }
        }
        */
    }

}
