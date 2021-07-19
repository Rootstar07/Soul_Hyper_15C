using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    //CapsuleCollider playerCol;
    Vector3Int cellPosition;
    public Grid grid;

    private void Start()
    {        
        cellPosition = grid.WorldToCell(transform.position);
        //transform.position = grid.GetCellCenterWorld(cellPosition);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            //Debug.Log(col.tag + "감지");
            Debug.Log("플레이어가 접근한 셀의 위치: " + cellPosition);

        
        }
        //else if (col.tag == "Range")
        //{
            //Debug.Log(col.tag + "감지");
        //}

        
    }
}
