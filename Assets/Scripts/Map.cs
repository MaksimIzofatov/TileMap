using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Map
{
    private bool[,] _map;

    public Map()
    {
		_map = new bool[10, 10];
	}

    public bool IsBusyCell(Vector3Int cellPosition)
    {
        return IsBorderOut(cellPosition) || _map[cellPosition.x, cellPosition.z];
    }

    public void SetCellAsBusy(Vector3Int cellPosition)
    {
        _map[cellPosition.x, cellPosition.z] = true;
    }

    private bool IsBorderOut(Vector3Int cellPosition)
    {
        return cellPosition.x < 0
            || cellPosition.z < 0
            || cellPosition.x > _map.GetLength(0)
            || cellPosition.z > _map.GetLength(0);
    } 
}
