using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemCounter : MonoBehaviour
{
    public Dictionary<eNormalType, int> itemQuantity = new Dictionary<eNormalType, int>();

    private void Start()
    {
        ResetAll();
    }

    public void ResetAll()
    {
        itemQuantity.Clear();
        for (int i = 0; i <= 6; i++)
        {
            var type = (eNormalType)i;
            itemQuantity.Add(type, 0);
        }
    }

    public void OnChange(eNormalType type, int value)
    {
        itemQuantity[type] += value;
    }

    public eNormalType GetMinType(List<eNormalType> types)
    {
        var sortedDict = itemQuantity.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        //Debug.LogWarning("GetMinType/" + sortedDict.Keys.First());
        if (types.Count > 0)
            for (int i = 0; i < types.Count; i++)
            {
                if (sortedDict.ContainsKey(types[i]))
                    sortedDict.Remove(types[i]);
            }
        if (sortedDict.Count <= 0)
        {
            var intenum = Random.Range(0, 7);
            return (eNormalType)intenum;
        }
        //Debug.LogWarning("Return First Type/" + sortedDict.Keys.First());
        return sortedDict.Keys.First();
    }
}
