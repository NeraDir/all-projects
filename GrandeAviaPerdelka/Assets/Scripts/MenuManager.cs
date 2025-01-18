using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject hwtMenu;
    public GameObject menuMen;

    [SerializeField]
    private TMP_Text recordShips;

    [SerializeField]
    private TMP_Text recordLife;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("hwtpOpeneds"))
        {
            hwtMenu.SetActive(true);
            menuMen.SetActive(false);
            PlayerPrefs.SetInt("hwtpOpeneds", 1);
        }
    }

    private void LateUpdate()
    {
        recordLife.text = "RECORD LIFE TIME: " + GameManager.RecordLifeTime.ToString("0") + "s";
        recordShips.text = "RECORD SHIPS: " + GameManager.DestroyedShipsRecord.ToString("0");
    }

    public void OnClickPlay() 
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickExit() 
    {
        Application.Quit();
    }
}
