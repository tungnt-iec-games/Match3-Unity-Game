﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int BoardX { get; private set; }

    public int BoardY { get; private set; }

    public Item Item { get; private set; }

    public Cell NeighbourUp { get; set; }

    public Cell NeighbourRight { get; set; }

    public Cell NeighbourBottom { get; set; }

    public Cell NeighbourLeft { get; set; }


    public bool IsEmpty => Item == null;

    public void Setup(int cellX, int cellY)
    {
        this.BoardX = cellX;
        this.BoardY = cellY;
    }

    public bool IsNeighbour(Cell other)
    {
        return BoardX == other.BoardX && Mathf.Abs(BoardY - other.BoardY) == 1 ||
            BoardY == other.BoardY && Mathf.Abs(BoardX - other.BoardX) == 1;
    }


    public void Free()
    {
        Item = null;
    }

    public void Assign(Item item)
    {
        Item = item;
        Item.SetCell(this);
    }

    public void ApplyItemPosition(bool withAppearAnimation)
    {
        Item.SetViewPosition(this.transform.position);

        if (withAppearAnimation)
        {
            Item.ShowAppearAnimation();
        }
    }

    internal void Clear()
    {
        if (Item != null)
        {
            Item.Clear();
            Item = null;
        }
    }

    internal bool IsSameType(Cell other)
    {
        return Item != null && other.Item != null && Item.IsSameType(other.Item);
    }

    internal void ExplodeItem()
    {
        if (Item == null) return;

        Item.ExplodeView();
        Item = null;
    }

    internal void AnimateItemForHint()
    {
        if (Item == null) return;
        Item.AnimateForHint();
    }

    internal void StopHintAnimation()
    {
        if (Item == null) return;
        Item.StopAnimateForHint();
    }

    internal void ApplyItemMoveToPosition()
    {
        if (Item == null) return;
        Item.AnimationMoveToPosition();
    }

    internal List<eNormalType> GetTypeNeighbourCell()
    {
        var neighbour = new List<eNormalType>();

        var upItem = GetNearNormalItem(NeighbourUp);
        if (upItem != null) neighbour.Add(upItem.ItemType);

        var bottomItem = GetNearNormalItem(NeighbourBottom);
        if (bottomItem != null) neighbour.Add(bottomItem.ItemType);

        var leftItem = GetNearNormalItem(NeighbourLeft);
        if (leftItem != null) neighbour.Add(leftItem.ItemType);

        var rightItem = GetNearNormalItem(NeighbourRight);
        if (rightItem != null) neighbour.Add(rightItem.ItemType);
        return neighbour;
    }

    NormalItem GetNearNormalItem(Cell cell)
    {
        if (cell != null && cell.Item != null && !cell.Item.isBonusItem)
        {
            var item = (NormalItem)cell.Item;
            return item;
        }
        return null;
    }
}
