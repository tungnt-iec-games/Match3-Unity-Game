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

    public eThemeType ThemeType;

    [SerializeField]
    private List<ThemeConfig> m_themeConfigList;

    private Dictionary<string, ItemConfig> m_itemConfigMap;

    public void Init()
    {
        var themeConfig = m_themeConfigList.Find(c => c.Type == ThemeType);

        m_itemConfigMap = new Dictionary<string, ItemConfig>();
        foreach (var itemConfig in themeConfig.ItemConfigList)
        {
            m_itemConfigMap.Add(itemConfig.Type, itemConfig);
        }
    }

    public ItemConfig GetItemConfig(string type)
    {
        if (m_itemConfigMap.TryGetValue(type, out var itemConfig))
        {
            return itemConfig;
        }

        return null;
    }
}

[System.Serializable]
public class ThemeConfig
{
    public eThemeType Type;
    public List<ItemConfig> ItemConfigList;
}

[System.Serializable]
public class ItemConfig
{
    public string Type;
    public Sprite Sprite;
}

public enum eThemeType
{
    CHARACTER,
    FISH,
}
