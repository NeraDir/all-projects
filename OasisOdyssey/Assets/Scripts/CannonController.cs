using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CannonController : MonoBehaviour
{
    public GameObject bullet;

    public TMP_Text[] showCombos;

    public static int combos;

    public static int score;

    public TMP_Text[] showSCore;

    public TMP_Text showLeftTIme;

    private float timer;

    public GameObject resultPanel;

    public static int maxScore 
    {
        get
        {
            if (PlayerPrefs.HasKey("maxScoreDataSave"))
            {
                return PlayerPrefs.GetInt("maxScoreDataSave");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("maxScoreDataSave", value);
        }
    }

    public static int maxCombos 
    {
        get 
        {
            if (PlayerPrefs.HasKey("maxCombosDataSave")) 
            {
                return PlayerPrefs.GetInt("maxCombosDataSave");
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("maxCombosDataSave", value);
        } 
    }

    private IEnumerator Start()
    {
        timer = 60;
        score = 0;
        combos = 0;
        while (true)
        {
            Instantiate(bullet,transform.parent.position, transform.rotation,transform.parent);
            yield return new WaitForSeconds(1.25f);
        }
    }

    public void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu() 
    {
        SceneManager.LoadScene("Menui");
    }

    private void LateUpdate()
    {
        foreach (var item in showCombos)
        {
            item.text = "X" + combos.ToString("0");
        }
        foreach (var item in showSCore)
        {
            item.text = score.ToString("0");
        }
        if (combos > maxCombos)
        {
            maxCombos = combos;
        }
        if (score > maxScore)
        {
            maxScore = score;
        }
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            resultPanel.SetActive(true);
            timer = 0;
        }
        showLeftTIme.text = "LEFT TIME: " + timer.ToString("0.0") + "s";
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            float needRotation = Mathf.Atan2(-mousePos.x, mousePos.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, needRotation));
        }
    }
}
