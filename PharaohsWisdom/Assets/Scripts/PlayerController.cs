using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IPointerClickHandler
{
    public Transform currentPlayer;
    private GameController controller;

    public Transform SpawnParrent;
    public Transform SpawnPos;
    public Transform MaxLeft;
    public Transform MaxRight;

    public void SpawnAndMovePlayer(Transform player, GameController controller)
    {
        currentPlayer = Instantiate(player, SpawnPos.position, player.transform.rotation, SpawnParrent);

        this.controller = controller;
        StartCoroutine(MoveRight());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StopAllCoroutines();

        StartCoroutine(MoveToUp(controller.Steps[controller.CompletedSteps].StepHeight));
    }

    IEnumerator MoveRight()
    {
        while (currentPlayer.position != MaxRight.position)
        {
            currentPlayer.position = Vector3.MoveTowards(currentPlayer.position, MaxRight.position, 3f * Time.deltaTime);

            yield return null;
        }

        StartCoroutine(MoveLeft());
    }

    IEnumerator MoveLeft()
    {
        while (currentPlayer.position != MaxLeft.position)
        {
            currentPlayer.position = Vector3.MoveTowards(currentPlayer.position, MaxLeft.position, 3f * Time.deltaTime);

            yield return null;
        }

        StartCoroutine(MoveRight());
    }

    IEnumerator MoveToUp(float pos)
    {
        while(currentPlayer.position != new Vector3(currentPlayer.position.x, pos, currentPlayer.position.z))
        {
            currentPlayer.position = Vector3.MoveTowards(currentPlayer.position, new Vector3(currentPlayer.position.x, pos, currentPlayer.position.z), 15f * Time.deltaTime);

            yield return null;
        }

        controller.SetPlatform(currentPlayer);
    }
}
