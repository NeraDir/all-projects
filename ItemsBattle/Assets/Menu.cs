using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject rulesPage;


    private void OnEnable()
    {

        if (!PlayerPrefs.HasKey("CanShowRulles"))
        {

            float waitTimeToShowRules = GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
            Invoke(nameof(Rulles), waitTimeToShowRules + 0.5f);


            PlayerPrefs.SetInt("CanShowRulles", 0);
        }

    }


    public static int BattleParticipantScore
    {
        get
        {
            if (PlayerPrefs.HasKey("BattleParticipantScoreSave"))
                return PlayerPrefs.GetInt("BattleParticipantScoreSave");
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("BattleParticipantScoreSave", value);
        }
    }

    public static int BattleParticipantEnemiesCount
    {
        get
        {
            if (PlayerPrefs.HasKey("BattleParticipantEnemiesCountSave"))
                return PlayerPrefs.GetInt("BattleParticipantEnemiesCountSave");
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("BattleParticipantEnemiesCountSave", value);
        }
    }

    public static string BattleParticipantEnemieName;


    public void StartBattle()
    {
        SceneManager.LoadScene("BattleScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Rulles()
    {
        rulesPage.SetActive(true);
    }
}
