using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManagerTemper : MonoBehaviour
{
    public List<Balls> startBalls = new List<Balls>();
    public List<Transform> holesTomake = new List<Transform>();
    public float radius;
    public TouchController cam;
    public List<Balls> activeBalls;

    public TMP_Text showLevel;

    public Transform camTransform;
    public GameObject[] levelPrefabs;



    public static int levelIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("PinoSorceyLevelIndexdofgodofghdhSve"))
            {
                return PlayerPrefs.GetInt("PinoSorceyLevelIndexdofgodofghdhSve");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("PinoSorceyLevelIndexdofgodofghdhSve", value);
        }
    }


    void Awake()
    {
        startBalls.Clear();
        holesTomake.Clear();
        levelPrefabs[levelIndex].SetActive(true);
        foreach (var item in levelPrefabs[levelIndex].GetComponentsInChildren<Transform>())
        {
            if (item.name == "camstartPos")
            {
                camTransform.position = item.position;
            }
        }
        foreach (var item in levelPrefabs[levelIndex].GetComponent<LevelPrefabManager>().startBalls)
        {
            startBalls.Add(item);
        }
        foreach (var item in levelPrefabs[levelIndex].GetComponent<LevelPrefabManager>().holesMaker)
        {
            holesTomake.Add(item);
        }
        foreach (Balls ball in startBalls)
        {
            ball.TransitionToState(ball.activeState);
        }

        foreach (Transform tf in holesTomake)
        {
            cam.defromToHoles(tf.position, radius);
        }
        FindObjectOfType<TouchController>().touchable = true;
        showLevel.text = "LEVEL " + (levelIndex+1).ToString();
    }

    private void OnApplicationQuit()
    {
        levelIndex = 0;
    }

    public void OnClickNext()
    {
        levelIndex++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickMenu()
    {
        levelIndex = 0;
        SceneManager.LoadScene("MenuScene");
    }
}
