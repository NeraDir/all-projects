using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CandieLevelContainer : MonoBehaviour, IPointerClickHandler
{
    public float TimeLiving;

    public int countOfCircles;

    public float clickObjectSpeedValueChanger;

    public float clickObjectChangeColorSpeed;

    public void OnPointerClick(PointerEventData eventData)
    {
        CandieGameConfig.TotalTime = TimeLiving;
        CandieGameConfig.countToSpawnCircles = countOfCircles;
        CandieGameConfig.clickObjectSpeedValueChanger = clickObjectSpeedValueChanger;
        CandieGameConfig.clickObjectChangeColorSpeed = clickObjectChangeColorSpeed;
        SceneManager.LoadScene("Game");
    }
}
