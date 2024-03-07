using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;

public static class Utils
{
    public static NormalItem.eNormalType GetRandomNormalType()
    {
        Array values = Enum.GetValues(typeof(NormalItem.eNormalType));
        NormalItem.eNormalType result = (NormalItem.eNormalType)values.GetValue(URandom.Range(0, values.Length));

        return result;
    }

    public static NormalItem.eNormalType GetRandomNormalTypeExcept(NormalItem.eNormalType[] types)
    {
        List<NormalItem.eNormalType> list = Enum.GetValues(typeof(NormalItem.eNormalType)).Cast<NormalItem.eNormalType>().Except(types).ToList();

        int rnd = URandom.Range(0, list.Count);
        NormalItem.eNormalType result = list[rnd];

        return result;
    }

    public static List<NormalItem.eNormalType> GetAllAdjacentItemTypes(this Cell cell)
    {
        return GetItemTypesFromCells(new List<Cell>
        {
            cell.NeighbourBottom, 
            cell.NeighbourLeft, 
            cell.NeighbourRight, 
            cell.NeighbourUp
        });
    }

    public static List<NormalItem.eNormalType> GetItemTypesFromCells(List<Cell> cellsToCheck)
    {
        HashSet<NormalItem.eNormalType> types = new();

        foreach (Cell cellToCheck in cellsToCheck)
        {
            if (cellToCheck != null)
            {
                if (cellToCheck.Item is NormalItem nitem)
                {
                    types.Add(nitem.ItemType);
                }
            }
        }
        
        return types.ToList();
    }
    
    public static List<NormalItem.eNormalType> GetNormalTypesExcept(NormalItem.eNormalType[] types)
    {
        return Enum.GetValues(typeof(NormalItem.eNormalType)).Cast<NormalItem.eNormalType>().Except(types).ToList();
    }

    public static NormalItem.eNormalType GetLeastAmountExcept(Cell[,] cells, List<NormalItem.eNormalType> types)
    {
        Dictionary<NormalItem.eNormalType, int> itemCount = new();

        var filteredTypes = GetNormalTypesExcept(types.ToArray());
        
        foreach (var type in filteredTypes)
        {
            itemCount.Add(type, 0);
        }

        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                Cell cell = cells[x, y];

                if (cell.Item is NormalItem item)
                {
                    if (itemCount.ContainsKey(item.ItemType))
                    {
                        itemCount[item.ItemType]++;
                    }
                }
            }
        }
        
        return itemCount.OrderBy(kvp => kvp.Value).First().Key;
    }
}
