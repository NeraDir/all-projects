using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_UpgradePage : MonoBehaviour
{
    [SerializeField]
    private UI_MenuPage uI_MenuPage;

    [SerializeField]
    private TMP_Text coinCountText;

    public static int coinCount;
    private float coinCountLerp;
   

    private void OnEnable()
    {
        uI_MenuPage.gameObject.SetActive(false);

        coinCount = GamePlayConfigs.coinsCount;
        //Debug.Log("coin count: " + coinCount);
        coinCountLerp = 0;
    }
    private void OnDisable()
    {
        uI_MenuPage.gameObject.SetActive(true);
    }


    private void Update()
    {
        coinCountLerp = Mathf.Lerp(coinCountLerp, coinCount, 0.3f);

        if (coinCountLerp < 1)
        {
            coinCountText.text = "0";
        }
        else
        {
            coinCountText.text = coinCountLerp.ToString("#");
        }



    }

    public static void IncrementCoins(int value)
    {
        coinCount += value;
        GamePlayConfigs.coinsCount = coinCount;
    }

    public void TapClosePage()
    {
        uI_MenuPage.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
