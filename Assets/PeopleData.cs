using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct PhaseInfo
{
    [SerializeField]
    [TextArea]
    public string 페이즈내용;

    [SerializeField]
    public CommentInfo[] 코멘트데이터;
}

[System.Serializable]
public struct CommentInfo
{
    [TextArea]
    public string 코멘트;
    public bool 활성여부;
}

public class PeopleData : MonoBehaviour
{
    public int 인물코드;
    [Header("인물이 물음표든 이름이 있든 관계도에 있는지")]
    public bool 인물활성화여부;
    public bool 물음표여부;
    public string 이름;

    [SerializeField]
    PhaseInfo[] phaseInfo;
    [Space]
    [Header("페이즈 관리")]
    public int 활성화된페이즈개수;


    // 클릭해서 부모 컴포넌트에 정보전달
    public void PeopleClicked()
    {
        transform.parent.GetComponent<PeopleManager>().ClickedPeople(인물코드, 활성화된페이즈개수, phaseInfo);
    }

}
