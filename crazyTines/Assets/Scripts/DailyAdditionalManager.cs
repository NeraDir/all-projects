using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyAdditionalManager : MonoBehaviour
{
    private float _speed = 3600;

    [SerializeField]
    private DailyArrowManager _dailyArrow;

    private DailyPlacemanager[] _dailyPlacemanager;

    [SerializeField]
    private GameObject _winPage;

    [SerializeField]
    private TMP_Text _winValueTxt;

    public static int winValue;

    private bool _isLaunched;

    private void Start()
    {
        _dailyPlacemanager = GetComponentsInChildren<DailyPlacemanager>();
        winValue = 0;
    }

    public void Launch()
    {
        _isLaunched = true;
    }

    private void LateUpdate()
    {
        if (!_isLaunched)
            return;
        _speed = Mathf.MoveTowards(_speed, 0, 1000 * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, 1), _speed * Time.deltaTime);
        if (_speed == 0)
        {
            UiCustomButton._buttonIsClicked = false;
            _speed = 0;
            _isLaunched = false;
            GameSavesData.PlayerGCoinsCount += winValue;
            _winValueTxt.text = winValue.ToString();
            MenuManager.onChangeStateOfShop?.Invoke();
            _winPage.SetActive(true);
        }
    }
}
