using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NormalItem;

public class GameDataManager : Singleton<GameDataManager>
{
    private readonly NormalItemInfo m_NormalItemInfo;

    public GameDataManager()
    {
        m_NormalItemInfo = Resources.Load<NormalItemInfo>("NormalItemInfo");
    }

    public Sprite GetSprite(eNormalType type)
    {
        return m_NormalItemInfo.m_ListNormalItem.Find((x) => x.ItemType == type).m_Sprite;
    }
}
