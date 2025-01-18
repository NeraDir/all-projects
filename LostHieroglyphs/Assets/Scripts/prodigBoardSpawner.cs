using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class prodigBoardSpawner : MonoBehaviour
{
    [SerializeField]
    private SpawnType[] spawnTypes;

    [SerializeField]
    private Image[] crystallsPastePlaces;

    [SerializeField]
    private Sprite[] crystallsSprites;

    [SerializeField]
    private Image[] showCombination;

    private int currentIndex;

    private void Start()
    {
        currentIndex = 0;
        int rndSpawn = Random.Range(0, spawnTypes.Length);
        spawnTypes[rndSpawn].SetList();
        for (int i = 0; i < crystallsPastePlaces.Length; i++)
        {
            if (spawnTypes[rndSpawn].spawningtype[i] == "$")
            {
                crystallsPastePlaces[i].sprite = spawnTypes[rndSpawn].needCombination[currentIndex];
                showCombination[currentIndex].sprite = spawnTypes[rndSpawn].needCombination[currentIndex];
                prodigGameManager.needSpritesCombination.Add(spawnTypes[rndSpawn].needCombination[currentIndex]);
                crystallsPastePlaces[i].gameObject.GetComponent<prodigCellComponent>().INIT();
                currentIndex++;
            }
            else
            {
                crystallsPastePlaces[i].sprite = crystallsSprites[Random.Range(0, crystallsSprites.Length)];
                crystallsPastePlaces[i].gameObject.GetComponent<prodigCellComponent>().INIT();
            }
        }
        currentIndex = 0;
    }
}

[Serializable]
public class SpawnType 
{
    public List<string> spawningtype = new List<string>();

    public Sprite[] needCombination;

    public string tempType;

    public void SetList() 
    {
        tempType = tempType.Replace(" ", "");
        string[] list = tempType.Split(',');
        spawningtype = list.ToList();
    }
}
