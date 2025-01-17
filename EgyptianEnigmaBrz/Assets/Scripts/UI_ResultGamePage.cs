using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_ResultGamePage : MonoBehaviour
{
    [SerializeField]
    private GameObject nextVirtualCamera;
    [SerializeField]
    private GameObject lastVirtualCamera;

    [SerializeField]
    private List<GameObject> gameUIPages;

    [SerializeField]
    private TMP_Text scoreCountText;
    private float scoreCountLerp;
    

    private void OnEnable()
    {
        lastVirtualCamera.SetActive(false);
        nextVirtualCamera.SetActive(true);

        for (int i = 0; i < gameUIPages.Count; i++)
            gameUIPages[i].SetActive(false);


        if (GameSceneController.scoreCount > ScoreData.bestScore)
        {
            ScoreData.bestScore = GameSceneController.scoreCount;
        }

        scoreCountLerp = 0;

    }

    private void FixedUpdate()
    {
        scoreCountLerp = Mathf.Lerp(scoreCountLerp, GameSceneController.scoreCount, 0.1f);

        if(scoreCountLerp > 1)
            scoreCountText.text = scoreCountLerp.ToString("#");
        else
            scoreCountText.text = "0";
    }

    public void TapRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void TapMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

}
