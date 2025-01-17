using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CandieComponent : MonoBehaviour, IPointerClickHandler
{
    public Color beginColor;
    public Color endColor;

    private Image myImage;

    public Sprite[] crystals;

    public Image myCrystalImage;

    public float value = 0;

    public GameObject good;
    public GameObject bad;
    public GameObject perfect;

    private bool isDestroyed;

    private IEnumerator Start()
    {
        myImage = GetComponent<Image>();
        myCrystalImage.sprite = crystals[Random.Range(0, crystals.Length)];
        myImage.color = beginColor;
        while (value != 100)
        {
            value = Mathf.MoveTowards(value, 100, CandieGameConfig.clickObjectSpeedValueChanger * Time.deltaTime);
            myImage.color = Color.Lerp(myImage.color, endColor, CandieGameConfig.clickObjectChangeColorSpeed * Time.deltaTime);
            yield return null;
        }
        if (!isDestroyed)
        {
            CandieGameSpawner.totalValue -= 10;
            CandieGameSpawner.comboCount = 0;
            bad.SetActive(gameObject);
        }
        Destroy(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isDestroyed)
        {
            return;
        }
        isDestroyed = true;
        if (value < 40)
        {
            bad.SetActive(true);
            CandieGameSpawner.comboCount = 0;
        }
        else if (value > 40 && value < 80)
        {
            good.SetActive(gameObject);
            CandieGameSpawner.comboCount += 1;
        }
        else if (value > 80)
        {
            perfect.SetActive(gameObject);
            CandieGameSpawner.comboCount += 1;
        }
        CandieGameSpawner.totalValue += (int)value;
        CandieGameSpawner.countDestroyedCircles += 1;
        myImage.enabled = false;
        myCrystalImage.enabled = false;
        Destroy(gameObject, 1);
    }
}
