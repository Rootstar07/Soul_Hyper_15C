using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class TalkManager : MonoBehaviour
{
    public bool canTalk;
    public Flowchart flowchart;
    public Text storyText;
    public string StoredText;


    // 현재 fungus에서 실행중인데 자동화하면 좋을듯
    public void TalkEnd()
    {
        canTalk = true;
    }


    private void Update()
    {
        // 대화 첫 실행
        if (Input.GetKeyDown(KeyCode.Space) && canTalk == true)
        {
            Debug.Log("대화 실행");
            canTalk = false;
            flowchart.SendFungusMessage("TalkStart");
        }
    }

    public void GetStoryText()
    {
        StoredText = storyText.text;
    }

}
