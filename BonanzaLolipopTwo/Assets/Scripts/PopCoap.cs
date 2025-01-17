using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum PopFruitsType 
{
    Orange,
    Melon,
    Lemon,
    Pear,
    Cherry
}

public class PopCoap : MonoBehaviour
{
    public List<FruitTask> fruitTasks = new List<FruitTask>();

    public List<FruitTask> currentFruitsTasks = new List<FruitTask>();

    [SerializeField]
    private PopTaskDisplayer _popTaskDisplayerPrefab;

    [SerializeField]
    private Transform _popTasksSpawnPosition;

    private void Start()
    {
        currentFruitsTasks.Clear();
        int rndCount = Random.Range(1,fruitTasks.Count);
        for (int i = 0; i < rndCount; i++)
        {
            int rndIndex = Random.Range(0, fruitTasks.Count);
            fruitTasks[rndIndex].fruitCount = Random.Range(2, 10);
            currentFruitsTasks.Add(fruitTasks[rndIndex]);
            fruitTasks.Remove(fruitTasks[rndIndex]);
        }
        foreach (var item in currentFruitsTasks)
        {
            PopTaskDisplayer tempDisplayer = Instantiate(_popTaskDisplayerPrefab, _popTasksSpawnPosition);
            tempDisplayer.SetDataOfTask(item.fruitCount, item.fruitSprite);
            item.fruitDataDispalyer = tempDisplayer;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PopFruit popFruit))
        {
            FruitTask tempTask = currentFruitsTasks.Find(x => popFruit.popFruitType == x.FruitsType);
            int indexOfTask = currentFruitsTasks.IndexOf(tempTask);
            if (tempTask != null)
            {
                currentFruitsTasks[indexOfTask].fruitCount--;
                currentFruitsTasks[indexOfTask].fruitDataDispalyer.SetDataOfTask(currentFruitsTasks[indexOfTask].fruitCount, currentFruitsTasks[indexOfTask].fruitSprite);
                popFruit.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(popFruit.gameObject));
                PopGameManager.popScore += Random.Range(10, 35);
                if (currentFruitsTasks[indexOfTask].fruitCount <= 0)
                {
                    
                    currentFruitsTasks.Remove(currentFruitsTasks[indexOfTask]);
                }
            }
            else
            {
                int indexofTaskert = Random.Range(0, currentFruitsTasks.Count);
                currentFruitsTasks[indexofTaskert].fruitCount++;
                currentFruitsTasks[indexofTaskert].fruitDataDispalyer.SetDataOfTask(currentFruitsTasks[indexofTaskert].fruitCount, currentFruitsTasks[indexofTaskert].fruitSprite);
                popFruit.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(popFruit.gameObject));
                PopGameManager.popScore -= Random.Range(3, 6);
                if (PopGameManager.popScore <=0)
                {
                    PopGameManager.popScore = 0;
                }
            }
        }
    }

    public bool GetCoapFillState() 
    {
        if (currentFruitsTasks.Count <= 0)
        {
            return true;
        }
        return false;
    }
}

[Serializable]
public class FruitTask 
{
    public PopFruitsType FruitsType;
    public int fruitCount;
    public Sprite fruitSprite;
    public PopTaskDisplayer fruitDataDispalyer;
}
