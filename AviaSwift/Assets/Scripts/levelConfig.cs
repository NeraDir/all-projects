using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class levelConfig : MonoBehaviour, IPointerClickHandler
{
    public float aviatorSpawningTime;
    public float aviatorSpeed;
    public float spawnBulletsTime;
    public float multiplayMovementBullet;

    public void OnPointerClick(PointerEventData eventData)
    {
        aviGameController.aviatorSpawningTime = aviatorSpawningTime;
        aviGameController.aviatorSpeed = aviatorSpeed;
        aviGameController.multiplayMovementBullet = multiplayMovementBullet;
        aviGameController.spawnBulletsTime = spawnBulletsTime;
        SceneManager.LoadScene("game");
    }
}
