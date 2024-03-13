using UnityEngine;
using static NormalItem;

public class SCR_NormalItem : MonoBehaviour
{
    public void InitGUI(eNormalType type)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = GameDataManager.Instance.GetSprite(type);
    }
}
