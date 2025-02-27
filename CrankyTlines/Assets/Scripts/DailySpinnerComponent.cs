using UnityEngine;
using UnityEngine.UI;

public class DailySpinnerComponent : MonoBehaviour
{
    [SerializeField] private Text _resultTxt;

    [SerializeField] private GameObject _menuScreen;
    [SerializeField] private GameObject _bonusScreen;

    public static int dailyBonusValue;

    private void LateUpdate()
    {
        _resultTxt.text = dailyBonusValue.ToString();
    }

    public void OnSpiningFinish()
    {
        TlineGameDataSaves.TlineCoins += dailyBonusValue;
        TlineMenuController.sendUpdateShop?.Invoke();
        Invoke(nameof(CloseScreen), 2);
    }

    private void CloseScreen()
    {
        _bonusScreen.SetActive(false);
        _menuScreen.SetActive(true);
    }
}
