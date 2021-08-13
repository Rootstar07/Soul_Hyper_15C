using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PeopleData : MonoBehaviour
{
    public int 인물코드;
    public TextMeshProUGUI 이름;
    public Image 상태배경;
    public TextMeshProUGUI 상태;

    // 클릭해서 부모 컴포넌트에 정보전달
    public void PeopleClicked()
    {
        // transform.parent.GetComponent<PeopleManager>().ClickedPeople(인물코드);
    }

}
