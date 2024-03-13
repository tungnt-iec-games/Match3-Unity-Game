using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;

public class Utils
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

    public static NormalItem.eNormalType GetNormalTypeWithLeastAmountExcept(NormalItem.eNormalType[] types, Dictionary<NormalItem.eNormalType, int> collection)
    {
        collection = collection.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        NormalItem.eNormalType type = collection.Where(x => !types.Contains(x.Key)).FirstOrDefault().Key;
        return type;
    }
}
