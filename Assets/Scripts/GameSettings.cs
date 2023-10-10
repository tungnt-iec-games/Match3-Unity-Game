using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : ScriptableObject
{
    public int BoardSizeX = 5;

    public int BoardSizeY = 5;

    public int MatchesMin = 3;

    public int LevelMoves = 16;

    public float LevelTime = 30f;

    public float TimeForHint = 5f;

    [SerializeField]
    private List<ItemConfig> m_itemConfigList;

    private Dictionary<NormalItem.eNormalType, ItemConfig> m_itemConfigMap;

    public void Init()
    {
        m_itemConfigMap = new Dictionary<NormalItem.eNormalType, ItemConfig>();
        foreach (var itemConfig in m_itemConfigList)
        {
            m_itemConfigMap.Add(itemConfig.Type, itemConfig);
        }
    }

    public ItemConfig GetItemConfig(NormalItem.eNormalType type)
    {
        if (m_itemConfigMap.TryGetValue(type, out var itemConfig))
        {
            return itemConfig;
        }

        return null;
    }
}

[System.Serializable]
public class ItemConfig
{
    public NormalItem.eNormalType Type;
    public string PrefabName;
}
