using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;

public class Utils
{
    public static eNormalType GetRandomNormalType()
    {
        Array values = Enum.GetValues(typeof(eNormalType));
        eNormalType result = (eNormalType)values.GetValue(URandom.Range(0, values.Length));

        return result;
    }

    public static eNormalType GetRandomNormalTypeExcept(eNormalType[] types)
    {
        List<eNormalType> list = Enum.GetValues(typeof(eNormalType)).Cast<eNormalType>().Except(types).ToList();

        int rnd = URandom.Range(0, list.Count);
        eNormalType result = list[rnd];

        return result;
    }

    public static eNormalType GetRandomNormalType(List<eNormalType> types)
    {
        eNormalType result = GameManager.Instance.ItemCounter.GetMinType(types);
        return result;
    }
}
