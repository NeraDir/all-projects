using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrostingLevelComponent : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPositions;

    [SerializeField]
    private GameObject[] candysPrefabs;

    [SerializeField]
    private GameObject jarOfCandys;

    [SerializeField]
    private Image jarFillBar;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(FrostingGameManager.candysSpawnTime);
            if (FrostingGameManager.candyGameStarted)
            {
                GameObject tempCandy = Instantiate(candysPrefabs[Random.Range(0, candysPrefabs.Length)], spawnPositions[Random.Range(0, spawnPositions.Length)].position, Quaternion.identity, spawnPositions[0].parent);
                tempCandy.transform.DOMove(jarOfCandys.transform.position, FrostingGameManager.candysMoveSpeed).OnComplete(() => tempCandy.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => {
                    if (tempCandy.GetComponent<FrostingCandyComponent>().mySprite == FrostingGameManager.needCandySprite)
                    {
                        FrostingGameManager.currentCount++;
                        Destroy(tempCandy.gameObject);
                    }
                    else
                    {
                        FrostingGameManager.candysHeartsCount--;
                        Destroy(tempCandy.gameObject);
                    }
                }));
            }
        }
    }

    private void LateUpdate()
    {
        jarFillBar.fillAmount = Mathf.Lerp(jarFillBar.fillAmount, (FrostingGameManager.currentCount / FrostingGameManager.needCount), 8 * Time.deltaTime);
    }
}
