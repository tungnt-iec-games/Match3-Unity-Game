using System;
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

    public GameObject CellBGPrefab;

    public List<NormalItemConfig> NormalItemConfigs;

    public Dictionary<NormalItem.eNormalType, GameObject> NormalItemDict = new();

    public List<BonusItemConfig> BonusItemConfigs;
    
    public Dictionary<BonusItem.eBonusType, GameObject> BonusItemDict = new();

    public void Init()
    {
        foreach (var config in NormalItemConfigs)
        {
            NormalItemDict.Add(config.type, config.prefab);
        }
        
        foreach (var config in BonusItemConfigs)
        {
            BonusItemDict.Add(config.type, config.prefab);
        }
    }
}

[Serializable]
public class NormalItemConfig
{
    public NormalItem.eNormalType type;
    public GameObject prefab;
}

[Serializable]
public class BonusItemConfig
{
    public BonusItem.eBonusType type;
    public GameObject prefab;
}
