using TMPro;
using UnityEngine;

public class PlaneDataContainer : MonoBehaviour
{
    public static int PlanesCoins 
    {
        get
        {
            if (PlayerPrefs.HasKey("PlaneSaveCoinsKey"))
            {
                return PlayerPrefs.GetInt("PlaneSaveCoinsKey");
            }
            return 100;
        }
        set
        {
            PlayerPrefs.SetInt("PlaneSaveCoinsKey", value);
        }
    }

    [SerializeField]
    private TMP_Text _userShowCoins;

    [SerializeField]
    private PlanerShopContainer _shopContainer;

    private void Start()
    {
        _shopContainer.OnClickBuy();
    }

    private void LateUpdate()
    {
        _userShowCoins.text = "COINS: " +PlanesCoins.ToString("0");
    }
}
