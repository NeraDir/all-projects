using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCreator : MonoBehaviour
{
    public delegate void Delegate(bool state);
    public event Delegate event_IsReloaded;

    public delegate void DelegateResult(bool isWin);
    public event DelegateResult event_IsResult;

    public delegate void DelegateEndResult(bool isWin);
    public event DelegateResult event_IsEndResult;

    [SerializeField] private PiramidController piramidController;
    [SerializeField] private CatapultController catapultController;

    [SerializeField] private Button buttonShoot;
    [SerializeField] private RawImage imageForClick;
    [SerializeField] private List<Texture2D> texturesForCard = new List<Texture2D>();

    [SerializeField] private AnimControllerCatapult animControllerCatapult;
    [SerializeField] private List<PiramidAnimator> piramidAnimator;

    public void CreateGame(int level)
    {
        boxes = 0;
        piramids = 0;

        piramidAnimator = piramidController.SetPiramid(level);
        animControllerCatapult = catapultController.SetCatapult(level);

        ReadyToNextShoot();
        Wallet.instance.ReloadWallet();
        StatisticsGame.instant.ReloadStats();
    }

    public void OnResutOfShoot(string id) // subscribe
    {
        buttonShoot.interactable = false;
        StartCoroutine(CheckResult(id));
    }

    private int boxes;
    private int piramids;

    private IEnumerator CheckResult(string id)
    {
        yield return new WaitForSeconds(0.1f);

        event_IsResult?.Invoke(id == needId);

        if (id == needId)
        {
            animControllerCatapult.SetWin();
            piramidAnimator[piramids].GoAnim();

            Wallet.instance.AddCoin(1);
            StatisticsGame.instant.AddStats();

            boxes++;

            yield return new WaitForSeconds(3f);

            ReadyToNextShoot();
            print(" - Win");
        }
        else
        {
            animControllerCatapult.SetLose();
            print(" - Lose");

            if (Wallet.instance.SubstractHealth())
            {
                yield return new WaitForSeconds(3f);
                ReadyToNextShoot();
            }
            else
            {
                yield return new WaitForSeconds(3f);
                // event_IsEndResult?.Invoke(false);
                StatisticsGame.instant.OpenStats();

                print("EndGame Lose");
                ClearAll();
            }
        }
    }

    public void ReadyToNextShoot()
    {
        if (boxes % 10 == 0 && boxes != 0)
        {
            piramids++;

            if (piramids >= piramidAnimator.Count)
            {
                // event_IsEndResult?.Invoke(true);

                StatisticsGame.instant.OpenStats();

                print("EndGame Win");
                ClearAll();
                return;
            }
        }

        needId = SetColorForCard();

        event_IsReloaded?.Invoke(true);

        buttonShoot.interactable = true;
    }

    [SerializeField] private string needId;
    private string SetColorForCard()
    {
        int id = Random.Range(0, texturesForCard.Count);
        imageForClick.texture = texturesForCard[id];

        return texturesForCard[id].name; // id card 
    }

    private void ClearAll()
    {
        Destroy(animControllerCatapult.gameObject);

        for (int i = 0; i < piramidAnimator.Count; i++)
        {
            Destroy(piramidAnimator[i].gameObject);
        }
    }
}
