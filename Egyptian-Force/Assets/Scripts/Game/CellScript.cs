using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class CellScript : MonoBehaviour
{
    public int X { get; set; }
    public int Y { get; set; }

    public int Value { get; set; }
    public int Points => IsEmpty ? 0 : (int)Mathf.Pow(2, Value);

    public bool IsEmpty => Value == 0;
    public bool HasMerged { get; set; }

    [SerializeField] private Image image;

    private CellAnimate currentAnimation;

    public void SetCellValue(int x, int y, int value, bool update = true)
    {
        X = x;
        Y = y;
        Value = value;

        if (update)
            UpdateCellValue();
    }

    public void IncreaseValue()
    {
        Value++;
        HasMerged = true;

        GameControllerManager.Instance.AddPoints(Points);
    }

    public void ResetFlgas()
    {
        HasMerged = false;
    }

    public void MergeWithCell(CellScript otherCell)
    {
        AnimationController.Instance.SmoothTransition(this, otherCell, true);

        otherCell.IncreaseValue();
        SetCellValue(X, Y, 0);
    }

    public void MoveToCell(CellScript target)
    {
        AnimationController.Instance.SmoothTransition(this, target, false);

        target.SetCellValue(target.X, target.Y, Value, false);
        SetCellValue(X, Y, 0);
    }

    public void UpdateCellValue()
    {
        image.sprite = SpritesManagerController.Instance.CEllSprites[Value];
    }

    public void SetAnimation(CellAnimate animation)
    {
        currentAnimation = animation;
    }

    public void RemoveAnimation()
    {
        if (currentAnimation != null)
            currentAnimation.Destroy();
    }
}
