using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static string symphonyGameDatasKey;

    public static int currentScore 
    {
        get 
        {
            if (PlayerPrefs.HasKey("JokerCurrentLevelSaveKey"))
                return PlayerPrefs.GetInt("JokerCurrentLevelSaveKey");
            return 1;
        }
        set 
        {
            PlayerPrefs.SetInt("JokerCurrentLevelSaveKey", value);
        }
    }

    public static int needFindNotes;

    public static int currentFindNotes;

    public List<AudioClip> noteClips;

    public Text showCurrentState;

    public AudioSource AudioSource;

    public Text[] showCurrentScore;

    public Text showResulters;

    public GameObject resultPage;

    public GameObject nextButton;

    public AudioClip winSound;

    [SerializeField]
    private GameObject[] labyrinth;

    [SerializeField]
    private Transform labaSpawnPos;

    private bool loose;

    public static float time 
    {
        get 
        {
            if (PlayerPrefs.HasKey("JokerCurrentTimeSaveKey"))
                return PlayerPrefs.GetFloat("JokerCurrentTimeSaveKey");
            return 35f;
        }
        set 
        {
            PlayerPrefs.SetFloat("JokerCurrentTimeSaveKey", value);
        }
    }

    public Text timeShow;

    private bool isGameStarted;

    private IEnumerator Start()
    {
        Instantiate(labyrinth[Random.Range(0, labyrinth.Length)], labaSpawnPos.position, labaSpawnPos.rotation);
        currentFindNotes = 0;
        
        yield return new WaitForSeconds(1f);
        needFindNotes = FindObjectsOfType<NoteComponent>().Length;
        isGameStarted = true;
    }

    public AudioClip GetRandomClip() 
    {
        int rndIndex = Random.Range(0, noteClips.Count);
        AudioClip clip = noteClips[rndIndex];
        noteClips.Remove(clip);
        return clip;
    }

    private void LateUpdate()
    {
        if (!isGameStarted)
            return;

        if (currentFindNotes >= needFindNotes)
        {
            showResulters.text = "YOU WIN";
            nextButton.SetActive(true);
            resultPage.SetActive(true);
            AudioSource.PlayOneShot(winSound);
            loose = false;
            isGameStarted = false;
            return;
        }
        time -= Time.deltaTime;
        if (time <= 0)
        {
            showResulters.text = "YOU LOOSE";
            nextButton.SetActive(false);
            resultPage.SetActive(true);
            loose = true;
            isGameStarted = false;
            return;
        }
        timeShow.text = time.ToString("0.0") + "s";
        if (currentScore > MenuComponent.SymphonyBestieScore)
        {
            MenuComponent.SymphonyBestieScore = currentScore;
        }
        showCurrentState.text = currentFindNotes.ToString() + "/" + needFindNotes.ToString();
        foreach (var item in showCurrentScore)
        {
            item.text = currentScore.ToString();
        }
    }

    private void OnApplicationQuit()
    {
        currentScore = 1;
        time = 35;
    }

    public void OnClickNext() 
    {
        currentScore += 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void OnClickRestart() 
    {
        currentScore = 1;
        time = 35;
        SceneManager.LoadScene("SampleScene");
    }

    public void OnClickMenu() 
    {
        currentScore = 1;
        time = 35;
        SceneManager.LoadScene("Menu");
    }
}
