using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _TimerText;
    [SerializeField] private TextMeshProUGUI _end1Text;
    [SerializeField] private TextMeshProUGUI _end2Text;

    [SerializeField] private GameObject _panaleFinish;
    [SerializeField] private GameObject _panaleControl;

    [SerializeField] private float[] _timers_bronse;
    [SerializeField] private float[] _timers_silver;
    [SerializeField] private float[] _timers_gold;
    [SerializeField] private int _price_gold;
    [SerializeField] private int _price_silver;
    [SerializeField] private int _price_bronse;

    [SerializeField] private Color[] _colors;

    [SerializeField] private GameObject[] _lvls;
    [SerializeField] private GameObject[] _cars;

    [SerializeField] private Transform _car;
    public static GameManager instance;

    int cones = 0;

    bool finish = false;
    bool startRound = false;

    private float timer = 0;
    private void Awake()
    {
        instance = this;
        _panaleFinish.SetActive(false);
        _panaleControl.SetActive(true);

        foreach (var x in _lvls)
        {
            x.SetActive(false);
        }
        _lvls[PrefsControl.GetLvlNum()].SetActive(true);
        _TimerText.color = _colors[0];

        Instantiate(_cars[PrefsControl.GetSceenNum()], _car);

    }
    private void FixedUpdate()
    {
        if (!startRound)
            return;
        if (finish)
            return;


        timer += Time.fixedDeltaTime;
        _TimerText.text = "Time: " + Mathf.Round(timer).ToString();
    }
    public void StartRound()
    {
        startRound = true;
    }
    public void Finish()
    {
        finish = true;
        _panaleFinish.SetActive(true);
        _panaleControl.SetActive(false);

        if (Mathf.Round(timer + cones) > _timers_gold[PrefsControl.GetLvlNum()])
        {
            if (Mathf.Round(timer + cones) > _timers_silver[PrefsControl.GetLvlNum()])
            {
                if (Mathf.Round(timer + cones) > _timers_bronse[PrefsControl.GetLvlNum()])
                {
                    Lose();
                }
                Win(2);
            }
            Win(1);
        }
        Win(0);

    }
    public void Win(int num)
    {
        switch (num)
        {
            case 0:
                _end1Text.color = _colors[2];
                _TimerText.text = "gold";

                _end2Text.text = "you hit " + cones.ToString() + " cones";
                _end1Text.text = "time " + Mathf.Round(timer) + " + " + cones + " cones " + " \n" +
                    "bronse you got " + _price_gold * (PrefsControl.GetLvlNum() + 1) + " gold";
                PrefsControl.ChageGoald(_price_gold * (PrefsControl.GetLvlNum() + 1));
                break;
            case 1:
                _end1Text.color = _colors[1];
                _TimerText.text = "silver";

                _end2Text.text = "you hit " + cones.ToString() + " cones";
                _end1Text.text = "time " + Mathf.Round(timer) + " + " + cones + " cones " + " \n" +
                    "bronse you got " + _price_silver * (PrefsControl.GetLvlNum() + 1) + " gold";
                PrefsControl.ChageGoald(_price_silver * (PrefsControl.GetLvlNum() + 1));
                break;
            case 2:
                _end1Text.color = _colors[0];

                _end2Text.text = "you hit " + cones.ToString() + " cones";
                _end1Text.text = "time " + Mathf.Round(timer) + " + " + cones + " cones " + " \n" +
                    "bronse you got " + _price_bronse * (PrefsControl.GetLvlNum() + 1) + " gold";
                PrefsControl.ChageGoald(_price_bronse * (PrefsControl.GetLvlNum() + 1));
                break;
        }
        PrefsControl.FinisLvl(PrefsControl.GetLvlNum());
    }
    public void Lose()
    {
        _TimerText.color = _colors[2];

        _end2Text.text = "you hit " + cones.ToString() + " cones";
        _end1Text.text = "you drove too long and did not take any place";
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void conesTrigger()
    {
        cones++;
    }
}
