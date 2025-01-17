
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource au;
    [SerializeField] private AudioSource lose;
    enum GameState 
    {
        StartToPlay, 
        GameItself,
        GameOver,
    }
public static float BestScore{
	get{
	if(PlayerPrefs.HasKey("AviGlideBestDistanceKey"))
return PlayerPrefs.GetFloat("AviGlideBestDistanceKey");
	return 0;
}
set{
	PlayerPrefs.SetFloat("AviGlideBestDistanceKey",value);
}
}
    GameState actialState;
    [SerializeField] private GameObject enemy1;
    [SerializeField] private float timeSpawn1 = 3f;
     private float actualTime1 = 1f;
    [SerializeField] private GameObject[] collectables;
    [SerializeField] private float collSpawm = 15f;
    [SerializeField] private GameObject shopButton;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject guide1;
    [SerializeField] private GameObject guide2;

    [SerializeField] private TMP_Text score;

    private float actualCollSpawm = 10f;
    private float timeGone = 0f;

    public void Start() 
    {
        actialState = GameState.StartToPlay;
        if (PlayerPrefs.GetInt("first",0)==0) 
        {
            shopButton.SetActive(false);
            guide1.SetActive(true);
            guide2.SetActive(true);
        }
    }
    public void Update() 
    {
        switch(actialState) 
        {
            case GameState.StartToPlay:
            break;
            case GameState.GameItself:
            actualTime1 -= Time.deltaTime;
            actualCollSpawm -= Time.deltaTime;
            timeGone += Time.deltaTime;
if(timeGone > BestScore)
BestScore = timeGone;
            if (actualTime1<0) 
            {
                float time = Random.Range(0.5f,timeSpawn1- timeGone/120);
                actualTime1 = time;
                if (time<0.2f) 
                {
                    actualTime1 = 0.2f; 
                }
                var clone = Instantiate(enemy1, new Vector3(Random.Range(1.8f,-1.8f),Random.Range(6f,8f),0),Quaternion.identity); 
                clone.GetComponent<Kamikaze>().Initialize(2+timeGone/90, 1.5f+timeGone/70);
                Destroy(clone,10f);

                clone = Instantiate(enemy1, new Vector3(Random.Range(1.8f,-1.8f),Random.Range(6f,8f),0),Quaternion.identity); 
                clone.GetComponent<Kamikaze>().Initialize(2+timeGone/90, 1.5f+timeGone/70);
                Destroy(clone,10f);

            }
            if (actualCollSpawm<0) 
            {
                actualCollSpawm = collSpawm;
                int i = Random.Range(0,collectables.Length); 
                Instantiate(collectables[i],new Vector3(Random.Range(1.8f,-1.8f),6,0), Quaternion.identity);
            }
            break;
            case GameState.GameOver:

            break;
        }
    }
    
    public void StartGame() 
    {
        if (actialState==GameState.GameOver || actialState==GameState.GameItself) return; 
        timeGone = 0;
        au.Play();
        FindObjectOfType<Player>().Initialize();
        shopButton.SetActive(false);
        guide1.SetActive(false);
        guide2.SetActive(false);
        actialState = GameState.GameItself;
    }
    public void EndGame() 
    {
        if (actialState==GameState.StartToPlay) return; 
        actialState = GameState.GameOver;
        au.Stop();
        lose.Play();
        PlayerPrefs.SetInt("first",1);
        score.text = "distance: "+Mathf.RoundToInt(timeGone)*3+"m";
        gameOverCanvas.SetActive(true);

    }
    public void ReloadScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void Pause() 
    {
        Time.timeScale = 0;
    }
     public void Resume() 
    {
        Time.timeScale = 1;
    }
}
