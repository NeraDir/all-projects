using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    private void OnEnable()
    {
        Game.lastLevelGameOverStateIndex = 1;
        PlayerPrefs.SetInt("LasrTargetPlaneColorSave", Game.currentLevelColorIndex);
        Time.timeScale = 0;
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
