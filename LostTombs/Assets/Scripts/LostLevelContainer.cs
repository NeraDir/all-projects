using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LostLevelContainer : MonoBehaviour, IPointerClickHandler
{
    public float TimeLiving;

    public int countOfCircles;

    public float clickObjectSpeedValueChanger;

    public float clickObjectChangeColorSpeed;

    public void OnPointerClick(PointerEventData eventData)
    {
        LostGameConfig.TotalTime = TimeLiving;
        LostGameConfig.countToSpawnCircles = countOfCircles;
        LostGameConfig.clickObjectSpeedValueChanger = clickObjectSpeedValueChanger;
        LostGameConfig.clickObjectChangeColorSpeed = clickObjectChangeColorSpeed;
        SceneManager.LoadScene("Gmaae");
    }
}
