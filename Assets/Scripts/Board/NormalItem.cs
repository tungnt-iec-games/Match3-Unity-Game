using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalItem : Item
{
    public enum eNormalType
    {
        TYPE_ONE,
        TYPE_TWO,
        TYPE_THREE,
        TYPE_FOUR,
        TYPE_FIVE,
        TYPE_SIX,
        TYPE_SEVEN
    }

    public eNormalType ItemType;

    private ItemConfig m_itemConfig;

    public void SetItemConfig(ItemConfig itemConfig)
    {
        m_itemConfig = itemConfig;
        ItemType = itemConfig.Type;
    }

    protected override string GetPrefabName()
    {
        string prefabname = m_itemConfig?.PrefabName;
        return prefabname;
    }

    internal override bool IsSameType(Item other)
    {
        NormalItem it = other as NormalItem;

        return it != null && it.ItemType == this.ItemType;
    }
}
