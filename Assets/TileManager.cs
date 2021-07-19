using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    Vector3Int cellPosition;
    Grid grid;

    private void Start()
    {
        grid = GetComponent<Grid>();

        cellPosition = grid.WorldToCell(transform.position);
        //transform.position = grid.GetCellCenterWorld(cellPosition);
    }
}
