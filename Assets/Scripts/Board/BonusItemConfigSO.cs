using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Config", menuName = "Config/Bonus Item")]
public class BonusItemConfigSO : ScriptableObject
{
    [SerializeField] private List<BonusItemConfig> configs;
    
    private Dictionary<BonusItem.eBonusType, BonusItemConfig> BonusItemDict = new();
    
    public void Init()
    {
        foreach (var config in configs)
        {
            BonusItemDict.Add(config.type, config);
        }
    }

    public BonusItemConfig GetConfig(BonusItem.eBonusType type) => BonusItemDict[type];
}

[Serializable]
public class BonusItemConfig
{
    public BonusItem.eBonusType type;
    public Sprite visual;
}
