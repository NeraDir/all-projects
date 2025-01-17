using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellAnimate : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private GameObject particleDestroy;

    private GameObject buffParticle;
    private float moveTime = .1f;
    private float appearTime = .2f;

    private Sequence sequence;

    public void Move(CellScript from, CellScript to, bool isMerging)
    {
        from.RemoveAnimation();
        to.SetAnimation(this);

        image.sprite = SpritesManagerController.Instance.CEllSprites[from.Value];

        transform.position = from.transform.position;

        sequence = DOTween.Sequence();

        sequence.Append(transform.DOMove(to.transform.position, moveTime).SetEase(Ease.InOutQuad));

        if(isMerging)
        {
            buffParticle = Instantiate(particleDestroy, transform);
            buffParticle.transform.position = new Vector3(transform.position.x, transform.position.y, 1f);

            sequence.AppendCallback(() =>
            {
                image.sprite = SpritesManagerController.Instance.CEllSprites[to.Value];
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

    public void Appear(CellScript cell)
    {
        cell.RemoveAnimation();
        cell.SetAnimation(this);

        image.sprite = SpritesManagerController.Instance.CEllSprites[cell.Value];

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
        Destroy(buffParticle);
        Destroy(gameObject);
    }
}
