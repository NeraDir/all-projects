using UnityEngine;
using System.Collections;
using TMPro;

public class GamingSnakeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _snakes;

    public static float timeing 
    {
        get 
        {
            if (PlayerPrefs.HasKey("PantheringTimingSaveKey"))
            {
                return PlayerPrefs.GetFloat("PantheringTimingSaveKey");
            }
            return 25;
        }
        set 
        {
            PlayerPrefs.SetFloat("PantheringTimingSaveKey", value);
        }
    }

    [SerializeField]
    private GameObject _spiner;

    [SerializeField]
    private GameObject _texer;

    [SerializeField]
    private GameObject _loosed;

    private float currentTime;

    [SerializeField]
    private TMP_Text _showCurrentTime;

    [SerializeField]
    private TMP_Text[] _showCurrentSnaked;

    public static int countOfSnakes;

    private void Start()
    {
        countOfSnakes = 0;
        INIT();
    }

    public void INIT() 
    {
        currentTime = timeing;
        StartCoroutine(Spawning());
    }

    private void LateUpdate()
    {
        _showCurrentTime.text = currentTime.ToString("0") +"s";
        foreach (var snake in _showCurrentSnaked) 
        {
            snake.text = countOfSnakes.ToString();
        }
    }

    private IEnumerator Spawning() 
    {
        while (currentTime > 0) 
        {
            yield return new WaitForSeconds(1);
            int randomingIndex = Random.Range(0, _snakes.Length);
            currentTime--;
            GameObject spawnedSnake = Instantiate(_snakes[randomingIndex], _snakes[randomingIndex].transform.position, _snakes[randomingIndex].transform.rotation);
            spawnedSnake.SetActive(true);
        }
        yield return new WaitForSeconds(1);
        if (countOfSnakes > 0)
        {
            _texer.SetActive(true);
            yield return new WaitForSeconds(2);
            _spiner.SetActive(true);
        }
        else
        {
            _loosed.SetActive(true);
        }
       
    }
}
