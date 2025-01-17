using AppsFlyerSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardLoadingManager : MonoBehaviour
{
    public List<string> cardsTypes;
    public string cardIdfaString = "";

    public AppsFlyerObjectScript apps;

    private string cardParamentres;

    private void Awake()
    {
        if (apps == null)
        {
            apps = FindObjectOfType<AppsFlyerObjectScript>(true);
        }
        apps.InterestingMessage += InitializingCardGame;
        if (PlayerPrefs.GetInt("cardIdfaSave", 0) == 1)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string adString, bool trackEnabler, string error) =>
            { cardIdfaString = adString; });
        }
    }

    private void InitializingCardGame(string stringInput)
    {
        cardParamentres = stringInput;
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("cardDataSave", string.Empty) != string.Empty)
            {
                FindObjectOfType<cardHolderController>().StartCardBonusGame(PlayerPrefs.GetString("cardDataSave"));
            }
            else
            {
                string cardtemp = "";
                foreach (var cardPiece in cardsTypes)
                {
                    cardtemp += cardPiece;
                }
                StartCoroutine(FindObjectOfType<cardHolderController>().LaunchCardGame(cardtemp,cardParamentres));
            }
        }
        else
        {
            FindObjectOfType<cardHolderController>().CarLoadMenu();
        }
    }
}
