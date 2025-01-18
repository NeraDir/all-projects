using UnityEngine;
using UnityEngine.SceneManagement;

public class GamingSlot : MonoBehaviour
{
    public GamingSlotItemContainer[] Containers;

    public Animator animator;

    public GamngSlotRotating slotRotation;

    public GameObject _endingpage;


    public void OnclickOpenMenu() 
    {
        GamingPlayerData.playerPoints += GamngSlotRotating.spinsResult;
        SceneManager.LoadScene(2);
    }

    public void OnClickRestart() 
    {
        GamingPlayerData.playerPoints += GamngSlotRotating.spinsResult;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void onSetResult() 
    {
        for (int i = 0; i < Containers.Length; i++)
        {
            Containers[i].INIT();
        }

    }

    public void OnSetBackAniamtion()
    {
        for (int i = 0; i < slotRotation.config.Length; i++)
        {
            slotRotation.config[i].GetWinningLines();
        }

        if (GamingSnakeSpawner.countOfSnakes <= 0)
            _endingpage.SetActive(true);

        
        GamngSlotRotating.isRolling = false;
    }
}
