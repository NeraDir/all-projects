using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentCellAnimation : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private SpritesBaseSO Sprites;

    private float moveTime = .1f;
    private float appearTime = .2f;

    private Sequence sequence;

    public void Move(Cell from, Cell to, bool isMerging)
    {
        from.RemoveAnimation();
        to.SetAnimation(this);

        image.sprite = Sprites.sprites[from.Value];

        transform.position = from.transform.position;

        sequence = DOTween.Sequence();

        sequence.Append(transform.DOMove(to.transform.position, moveTime).SetEase(Ease.InOutQuad));

        if (isMerging)
        {
            sequence.AppendCallback(() =>
            {
                image.sprite = Sprites.sprites[to.Value];
            });

            sequence.Append(transform.DOScale(1.2f, appearTime));
            sequence.Append(transform.DOScale(1f, appearTime));
        }

        sequence.AppendCallback(() =>
        {
            to.UpdateCellValue();
            Destroy();
        });
    }

    public void Appear(Cell cell)
    {
        cell.RemoveAnimation();
        cell.SetAnimation(this);

        image.sprite = Sprites.sprites[cell.Value];

        transform.position = cell.transform.position;
        transform.localScale = Vector2.zero;

        sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(1.2f, appearTime * 2));
        sequence.Append(transform.DOScale(1f, appearTime * 2));

        sequence.AppendCallback(() =>
        {
            cell.UpdateCellValue();
            Destroy();
        });
    }

    public void Destroy()
    {
        sequence.Kill();
        Destroy(gameObject);
    }
}
