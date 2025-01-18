using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private TextMeshProUGUI _textBullets;
    [SerializeField] private RectTransform _HPBAR;
    private int shots;
    public int hp;
    private int dmg;
    private WorldClockSteps clock;
    public static Player instance;
    private void Awake()
    {
        instance = this;
        shots = PrefsControl.GetUpgrade(0) + 1;
        hp = PrefsControl.GetUpgrade(1) + 1;
        dmg = PrefsControl.GetUpgrade(2) + 1;

        Hp(0);

        _textBullets.text = "Bullets: " + shots.ToString();
    }
    private void Start()
    {
        clock = WorldClockSteps.instance;
    }
    public void Shot(Vector2Int v2i)
    {
        //CharacterControlling.instance.goal = new Vector2Int(9999, 9999);
        if (shots <= 0)
            return;
        shots--;
        
        GameObject g = Instantiate(_bulletPrefab,transform.position, transform.rotation);
        g.GetComponent<Bullet>().SetTarget(v2i, dmg);

        clock.TryStep();
        _textBullets.text = "Bullets: " + shots.ToString();
    }
    public void Hp(int val)
    {
        hp += val;
        for (int i = 0; i < _HPBAR.childCount; i++)
        {
            if(i >= hp)
            _HPBAR.GetChild(i).GetComponent<Image>().color = Color.clear;
            else
                _HPBAR.GetChild(i).GetComponent<Image>().color = Color.white;
        }
        if(hp <= 0)
        {
            MainRoundManager.instance.Lose();
        }
    }
    public void Recharge()
    {
        if (shots == PrefsControl.GetUpgrade(0) + 1)
            return;

        shots = PrefsControl.GetUpgrade(0) + 1;
        _textBullets.text = "Bullets: " + shots.ToString();
        clock.TryStep();
    }
}
