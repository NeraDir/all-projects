using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] private Text _resultTxt;
    [SerializeField] private Text _resultAddTxt;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _nextButton;

    public void SetupData(bool value)
    {
        _restartButton.SetActive(!value);
        _nextButton.SetActive(value);
        _resultTxt.text = value ? $"LEVEL {TlineGameDataSaves.TlineCurrentLevel + 1} COMPLETED" : $"LEVEL {TlineGameDataSaves.TlineCurrentLevel + 1} NOT COMPLETED";
        _resultAddTxt.text = value ? "VICTORY" : "LOOSE";
    }

    public void OnClickNext()
    {
        TlineGameDataSaves.TlineCurrentLevel += 1;
        if (TlineGameDataSaves.TlineCurrentLevel >= TlineGameDataSaves.TlineMaxReachedLevel)
        {
            TlineGameDataSaves.TlineMaxReachedLevel = TlineGameDataSaves.TlineCurrentLevel;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
