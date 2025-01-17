using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GlobalInitialization : MonoBehaviour
{
    public List<Sprite> sprites = new();
    public List<Image> imagesForBeauty = new();

    public List<Image> FirstLine = new();
    public List<Image> SecondLine = new();
    public List<Image> ThirdLine = new();

    public List<AllVariants> allVariants = new();

    public Animator animator;
    public Combinations CombinationsScript;
    public static bool isRolling = false;

    private List<AllVariants> FallVariants = new();

    public int WinKnifes = 0;
    public int bet;

    public TMP_Text betTXT;
    public TMP_Text GlobalCoins;

    public CirclGameController NextGameController;

    private void Start()
    {
        FillFalshiveImages();
        bet = 5;
        betTXT.text = $"{bet}";
        GlobalCoins.text = $"{GlobalSave.CoinsCount}";
    }

    public void FillFalshiveImages()
    {
        foreach (var item in imagesForBeauty)
        {
            int randSP = Random.Range(0, sprites.Count);

            item.sprite = sprites[randSP];
        }
    }

    public void FillRealSlotImages()
    {
        int Random1 = Random.Range(0, allVariants.Count);
        int Random2 = Random.Range(0, allVariants.Count);
        int Random3 = Random.Range(0, allVariants.Count);

        FallVariants.Add(allVariants[Random1]);
        FallVariants.Add(allVariants[Random2]);
        FallVariants.Add(allVariants[Random3]);

        for (int i = 0; i < FirstLine.Count; i++)
        {
            FirstLine[i].sprite = allVariants[Random1].Vartiants[i].sprite;
        }

        for (int i = 0; i < SecondLine.Count; i++)
        {
            SecondLine[i].sprite = allVariants[Random2].Vartiants[i].sprite;
        }

        for (int i = 0; i < ThirdLine.Count; i++)
        {
            ThirdLine[i].sprite = allVariants[Random3].Vartiants[i].sprite;
        }
    }

    public void Role()
    {
        if (!isRolling)
        {
            FallVariants.Clear();
            animator.SetInteger("State", 0);
            Invoke("AnimateRole", 0.2f);
        }
    }

    public void AnimateRole()
    {
        animator.SetInteger("State", 1);
        GlobalSave.CoinsCount -= bet;
        GlobalCoins.text = $"{GlobalSave.CoinsCount}";
        isRolling = true;
    }

    public void SetRealLineSettings()
    {
        FillRealSlotImages();
    }

    public void CheckFallCombinations()
    {
        WinKnifes = 0;

        foreach (var it in CombinationsScript.CombinationsList)
        {
            for (int i = 0; i < FallVariants.Count; i++)
            {
                WinKnifes += it.CheckOnValid(FallVariants[i]);
            }
        }

        if (WinKnifes > 0)
        {
            Invoke(nameof(GoToAnotherGame), 0.5f);
        }
        else
        {
            isRolling = false;
        }
    }

    public void GoToAnotherGame()
    {
        isRolling = false;
        transform.parent.gameObject.SetActive(false);
        NextGameController.gameObject.SetActive(true);
        NextGameController.Init(WinKnifes, bet);
    }

    public void PlusBet()
    {
        if (bet < 56)
            bet += 5;
        betTXT.text = $"{bet}";
    }

    public void MinusBet()
    {
        if (bet > 9)
            bet -= 5;
        betTXT.text = $"{bet}";
    }
}

[System.Serializable]
public struct AllVariants
{
    public List<Variant> Vartiants;
}

[System.Serializable]
public struct Variant
{
    public int id;
    public Sprite sprite;
}
