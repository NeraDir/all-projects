using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GammaScript : AbstractPrefab
{
    public override GameObject Object { get { return gameObject; } set { } }

    public Image WhatCopy;
    public Image CopyCover;
    public TMP_Text ResultPercent;

    public Sprite sprite;

    public Slider R;
    public Slider G;
    public Slider B;

    private float min = 0;
    private float max = 1;
    private Color ff;
    Material matFFF;

    private int Percent = 0;

    [SerializeField]
    private List<GameObject> pageItems;

    [SerializeField]
    private GameObject preGamePage;

    private void OnEnable()
    {
        //StartCoroutine(EnabledItems(false));
        preGamePage.SetActive(true);
        //animator = GetComponent<Animator>();
    }

    private void Start()
    {
        sprite = GameManager.Instance.GetRandomSprite();

        WhatCopy.sprite = sprite;
        CopyCover.sprite = sprite;

        WhatCopy.material.SetTexture("_Texture2D", sprite.texture);
        CopyCover.material.SetTexture("_Texture2D", sprite.texture);

        ff = Random.ColorHSV();

        WhatCopy.material.color = ff;

        R.minValue = min;
        G.minValue = min;
        B.minValue = min;

        R.maxValue = max;
        G.maxValue = max;
        B.maxValue = max;

        R.value = min + 1; G.value = min + 1; B.value = min + 1;
        Percent = 0;

        Object = gameObject;
    }

    public void ChangeR(float value)
    {
        Color _color = CopyCover.material.GetColor("_Color");

        CopyCover.material.SetColor("_Color", new Color(value, _color.g, _color.b, _color.a));
    }

    public void ChangeG(float value)
    {
        Color _color = CopyCover.material.GetColor("_Color");

        CopyCover.material.SetColor("_Color", new Color(_color.r, value, _color.b, _color.a));
    }


    public void ChangeB(float value)
    {
        Color _color = CopyCover.material.GetColor("_Color");

        CopyCover.material.SetColor("_Color", new Color(_color.r, _color.g, value, _color.a));
    }

    public void CheckBtn()
    {
        Color buffWhatWant = WhatCopy.material.color;
        Color buffWhatResult = CopyCover.material.color;

        Percent = (int)(100 - ((((Mathf.Abs(buffWhatWant.r - buffWhatResult.r) + Mathf.Abs(buffWhatWant.g - buffWhatResult.g) + Mathf.Abs(buffWhatWant.b - buffWhatResult.b)) / 1) * 100)) / 3);
        GameManager.Instance.PercentResult = Percent;

        Invoke("Respawn", 0.8f);
    }

    public void Respawn()
    {
        for (int i = 0; i < pageItems.Count; i++)
        {
            pageItems[i].SetActive(false);
        }
        GameManager.Instance.ShowResult();
    }

    public override void ShowPageItems()
    {
        StartCoroutine(EnabledItems(true));
    }
    private IEnumerator EnabledItems(bool state)
    {
        for (int i = 0; i < pageItems.Count; i++)
        {
            StartCoroutine(changeScale(pageItems[i].transform, state));
            yield return new WaitForSeconds(0.3f);
        }
    }
    private IEnumerator changeScale(Transform item, bool enabledState)
    {
        Vector3 finalSize = Vector3.zero;


        if (enabledState)
        {

            finalSize = Vector3.one;
            item.localScale = Vector3.zero;
            item.gameObject.SetActive(true);
        }
        else
        {
            finalSize = Vector3.zero;
            item.localScale = Vector3.zero;
        }

        while (item.localScale != finalSize)
        {
            item.localScale = Vector3.Lerp(item.localScale, finalSize, 0.1f);
            yield return null;
        }

        if (!enabledState)
        {
            item.gameObject.SetActive(false);
        }

        yield return null;
    }

    public override void Init()
    {
        
    }
}
