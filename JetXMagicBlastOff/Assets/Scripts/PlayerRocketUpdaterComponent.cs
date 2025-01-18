using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerRocketUpdaterComponent : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private PlayerRocketComponent rocket;

    private float currentExp = 0;

    private float needExpToUp = 10;

    private int currentLvl = 1;

    [SerializeField]
    private Text levelTXT;

    [SerializeField]
    private Transform arrowTransform;

    [SerializeField]
    private Transform needPositionTransform;

    [SerializeField]
    private RotateArrowComponent arrowComponent;

    private void Start()
    {
        needPositionTransform.Rotate(new Vector3(0, 0, 1), Random.Range(-360, 360));
    }

    private void LateUpdate()
    {
        arrowTransform.Rotate(new Vector3(0, 0, 1), 180 * Time.deltaTime);
        levelTXT.text = "LVL " + currentLvl;
    }

    public void UpLevel()
    {
        currentExp += 3;
        if (currentExp >= needExpToUp)
        {
            rocket.bulletDamage += 1;
            rocket.shootSpeed -= 0.1f;
            needExpToUp += 5;
            currentLvl++;
            currentExp = 0;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (arrowComponent.ArrowOnLine())
        {
            UpLevel();
            needPositionTransform.Rotate(new Vector3(0, 0, 1), Random.Range(-360, 360));
        }
        else
        {
            Handheld.Vibrate();
        }
    }
}
