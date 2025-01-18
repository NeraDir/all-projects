using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private GameObject resultPage;

    [HideInInspector]
    public int PercentResult;

    [SerializeField]
    private List<Sprite> all_sprites;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    [SerializeField] private List<AbstractPrefab> prefabs = new();
    [SerializeField] private Transform Parrent;

    private GameObject currentPref;
    private GameObject lastPref;
    private int currentIndex;
    private int lastIndex;

    private void Start()
    {
        lastIndex = 1;
        currentIndex = 0;
        Spawn();
    }

    public void Spawn()
    {
        if(currentPref != null)
            Destroy(currentPref);


        currentIndex = Random.Range(0, prefabs.Count);


        if (currentIndex == lastIndex)
        {
            while (currentIndex == lastIndex)
            {
                currentIndex = Random.Range(0, prefabs.Count);
            }
        }

       
        resultPage.SetActive(false);
        

        

        currentPref = Instantiate(prefabs[currentIndex].Object, Parrent);

        lastIndex = currentIndex;
    }

    public void ShowResult()
    {
        resultPage.SetActive(true);
    }

    public void GoHome()
    {
        int index = 0;
        SceneManager.LoadScene("MenuScene");
    }

    public Sprite GetRandomSprite()
    {
        return all_sprites[Random.Range(0, all_sprites.Count)];
    }
}
