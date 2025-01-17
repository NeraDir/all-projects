using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanelInit : MonoBehaviour
{
    [SerializeField] private TMP_Text Stars;
    [SerializeField] private TMP_Text Metres;

    public void Init(int stars, int metres)
    {
        Stars.text = $"x{stars}";
        Metres.text = $"{metres} m";
    }

    public void GoMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
