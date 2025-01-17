using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;


public class MenuController : MonoBehaviour
{
    public TMP_Text showRecordd;

    private void LateUpdate()
    {
        showRecordd.text = GameController.RecordOfTruth.ToString();
    }

    public void OnClickClose() 
    {
        Application.Quit();
    }

    public void OnClickLoadGame(int SceneIndex) 
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
