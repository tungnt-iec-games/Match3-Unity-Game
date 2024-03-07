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
    public GameObject ItemPrefab;
    
    [HideInInspector] public NormalItemConfigSO CurrentNormalItemConfig;
    
    [SerializeField] private List<NormalItemConfigSO> NormalItemConfigList;
    
    public BonusItemConfigSO BonusItemConfig;

    private int m_configIndex = 0;
    
    
    public void Init()
    {
        foreach (var config in NormalItemConfigList)
        {
            config.Init();
        }
        
        BonusItemConfig.Init();
        
        CurrentNormalItemConfig = NormalItemConfigList[m_configIndex];
    }

    public void CycleTheme()
    {
        if (m_configIndex == NormalItemConfigList.Count - 1)
        {
            m_configIndex = 0;
        }
        else
        {
            m_configIndex++;
        }

        CurrentNormalItemConfig = NormalItemConfigList[m_configIndex]; 
    }
}
