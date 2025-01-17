using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AviConstructorMenuComponent : MonoBehaviour
{
    [SerializeField]
    private AviaShopLine[] _aviaShopLines;

    [SerializeField]
    private AviConstructBuyComponent[] _aviaConstructBuyComponents;

    [SerializeField]
    private Transform[] _aviConstructsSpawnPositions;

    [SerializeField]
    private GameObject _aviHowToPlayPage;

    [SerializeField]
    private TMP_Text _aviBestReachedDistanceDisplay;

    [SerializeField]
    private TMP_Text _aviStarsDisplay;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("aviHowToPlayKey"))
        {
            _aviHowToPlayPage.SetActive(true);
            PlayerPrefs.SetInt("aviHowToPlayKey", 1);
        }
        _aviBestReachedDistanceDisplay.text = AviGameComponent.aviGameBestReachedDistance.ToString("0.0") + "s";
        for (int i = 0; i < _aviaShopLines.Length; i++)
        {
            for (int j = 0; j < _aviaShopLines[i].aviaShopDatas.Length; j++)
            {
                AviConstructBuyComponent tempBuyComponent = Instantiate(_aviaConstructBuyComponents[i], _aviConstructsSpawnPositions[i]);
                tempBuyComponent.aviSellSprite = _aviaShopLines[i].aviaShopDatas[j].aviShopItem;
                tempBuyComponent.aviSellPrice = _aviaShopLines[i].aviaShopDatas[j].aviShopPrice;
                tempBuyComponent.aviConstructIndex = j;
                if (i == 0 && j == 0)
                    tempBuyComponent.AviBuy();
                if (i == 1 && j == 0)
                    tempBuyComponent.AviBuy();
            }
        }
    }

    private void SpawnShopItems(AviConstructBuyComponent buyComponent, Transform spawnPosition)
    {

    }

    private void LateUpdate()
    {
        _aviStarsDisplay.text = "x" + AviGameComponent.aviGameStarsCount.ToString();
    }

    public void OnClickedPlay()
    {
        SceneManager.LoadScene("GameConstructScene");
    }

    public void OnClickedExit()
    {
        Application.Quit();
    }
}
[Serializable]
public class AviaShopLine
{
    public AviaShopData[] aviaShopDatas;
}

[Serializable]
public class AviaShopData
{
    public Sprite aviShopItem;
    public Sprite aviUseSprite;
    public int aviShopPrice;
}
