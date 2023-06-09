using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    public SKinType Type;
    public List<Sprite> skin1;
    public List<Sprite> skin2;

    public List<Sprite> spBonus;

    public Sprite GetItemSkin(eNormalType type)
    {
        int index = (int)type;
        return Type == SKinType.SKin1 ? skin1[index] : skin2[index];
    }

    public Sprite GeteBonusTypes(BonusItem.eBonusType type)
    {
        int index = (int)type;
        return spBonus[index];
    }
}

public enum SKinType
{
    SKin1, Skin2
}
