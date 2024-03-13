using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
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
    public Sprite m_Sprite;

    public void SetType(eNormalType type)
    {
        ItemType = type;
    }
    public override void SetView()
    {
        GameObject prefab = Resources.Load<GameObject>(Constants.PREFAB_NORMAL_ITEM);
        prefab.GetComponent<SCR_NormalItem>().InitGUI(ItemType);
        if (prefab)
        {
            View = GameObject.Instantiate(prefab).transform;
        }
    }

    protected override string GetPrefabName()
    {
        string prefabname = string.Empty;
        switch (ItemType)
        {
            case eNormalType.TYPE_ONE:
                prefabname = Constants.PREFAB_NORMAL_TYPE_ONE;
                break;
            case eNormalType.TYPE_TWO:
                prefabname = Constants.PREFAB_NORMAL_TYPE_TWO;
                break;
            case eNormalType.TYPE_THREE:
                prefabname = Constants.PREFAB_NORMAL_TYPE_THREE;
                break;
            case eNormalType.TYPE_FOUR:
                prefabname = Constants.PREFAB_NORMAL_TYPE_FOUR;
                break;
            case eNormalType.TYPE_FIVE:
                prefabname = Constants.PREFAB_NORMAL_TYPE_FIVE;
                break;
            case eNormalType.TYPE_SIX:
                prefabname = Constants.PREFAB_NORMAL_TYPE_SIX;
                break;
            case eNormalType.TYPE_SEVEN:
                prefabname = Constants.PREFAB_NORMAL_TYPE_SEVEN;
                break;
        }

        return prefabname;
    }

    internal override bool IsSameType(Item other)
    {
        NormalItem it = other as NormalItem;

        return it != null && it.ItemType == this.ItemType;
    }
}

[CreateAssetMenu(fileName = "NormalItemInfo", menuName = "ScriptableObjects/NormalItemInfo", order = 1)]
public class NormalItemInfo : ScriptableObject
{
    public List<NormalItem> m_ListNormalItem;
}