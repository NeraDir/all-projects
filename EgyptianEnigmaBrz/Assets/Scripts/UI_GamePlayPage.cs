using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_GamePlayPage : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text levelNumberText;

    [SerializeField]
    private Slider expSlider;
    [SerializeField]
    private Slider healthSlider;

    private float expValueLerp;
    private float healthValueLerp;

    [SerializeField]
    private Transform aimIconTransform;
    [SerializeField]
    private Transform aimIconFollowPoint;

    [SerializeField]
    private UI_PausePage uI_PausePage;


    void Start()
    {
        expValueLerp = healthValueLerp = 0;
    }


    private void FixedUpdate()
    {
        UpdateSliders();

        scoreText.text = GameSceneController.scoreCount.ToString();
        levelNumberText.text = "level " + GameSceneController.levelNumber;

        aimIconTransform.position = Camera.main.WorldToScreenPoint(aimIconFollowPoint.position);
        //aimIconTransform.position = (Vector2)Camera.main.WorldToViewportPoint(aimIconFollowPoint.position);
    }

    private void UpdateSliders()
    {
        expValueLerp = Mathf.Lerp(expValueLerp, GameSceneController.currentExp, 0.2f);
        healthValueLerp = Mathf.Lerp(healthValueLerp, GunPlatformHealth.currentHealth, 0.2f);

        expSlider.value = expValueLerp;
        healthSlider.value = healthValueLerp;

        expSlider.maxValue = GameSceneController.maxExp;
        healthSlider.maxValue = GunPlatformHealth.maxHealth;
    }

    public void TapPauseButton()
    {
        uI_PausePage.gameObject.SetActive(true);
    }
}
