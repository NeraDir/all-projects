using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;

public class CandyGenerator : MonoBehaviour
{
    public static Action TaskComplete;

    [SerializeField] private int lastCandy;
    [SerializeField] private Candy prefabCandy;
    [SerializeField] private MultyCandy prefabMultyCandy;
    [SerializeField] private BombCandy prefabBombCandy;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private List<Sprite> spriteSpecial;
    [SerializeField] private float ScaleFactor;
    private int id;

    public Candy candy;
    public MultyCandy multyCandy;
    [SerializeField] private float cooldown;
    private float timer;
    [SerializeField] private TaskManager taskManager;

    private void Start()
    {
    }

    private void Update()
    {
        if (timer <= 0)
        {
            Spawn();
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    [ContextMenu("Spawn")]
    public void Spawn()
    {   
        
        if (candy != null)
        {
            if (candy.falled == true)
            {
                candy = null;
                Debug.Log("safasfsdf");
                timer = cooldown;

            }
        }
        else
        {
            int spawnIndex = UnityEngine.Random.Range(0, 10);
            if (spawnIndex == 0 || spawnIndex == 1)
            {
                SpawnMultyCandy();
            }
            else if (spawnIndex == 3)
            {
                SpawnBomb();
            }
            else
            {
                id = UnityEngine.Random.Range(0, 3);
                candy = Instantiate(prefabCandy, transform); ;
                candy.GetComponent<SpriteRenderer>().sprite = sprites[id];
                candy.transform.localScale += Vector3.one * ScaleFactor * id;
                candy.Id = id;
                candy.transform.position = transform.position;
            }
        }
    }

    public void Merge(GameObject object1, GameObject object2, int Id)
    {
        if (Id < 8)
        {
            Debug.Log(object2);
            Debug.Log(object1);

            Vector3 newPosition = (object1.transform.position + object2.transform.position) / 2;

            object1.transform.localScale += Vector3.one * ScaleFactor;
            object1.GetComponent<SpriteRenderer>().sprite = sprites[Id + 1];
            Candy candyComponent = object1.GetComponent<Candy>();
            candyComponent.Id = Id + 1;
            candyComponent.hasMerged = false;

            Destroy(object2);
            
            if (taskManager.candyTypeId == candyComponent.Id)
            {
                StartCoroutine(DestroyCandy(candyComponent.gameObject));
            }
        }

    }

    public void SpawnMultyCandy()
    {
        candy = Instantiate(prefabMultyCandy, transform); ;
        candy.GetComponent<SpriteRenderer>().sprite = spriteSpecial[0];
        candy.transform.localScale += Vector3.one * ScaleFactor * 1;
        candy.Id = 100;
        candy.transform.position = transform.position;
    }

    public void SpawnBomb()
    {
        candy = Instantiate(prefabBombCandy, transform); ;
        candy.GetComponent<SpriteRenderer>().sprite = spriteSpecial[1];
        candy.Id = 200;
        candy.transform.position = transform.position;
    }

    private IEnumerator DestroyCandy(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        Destroy(obj);
        taskManager.candyAmountComplete++;
        if (taskManager.candyAmountComplete == taskManager.candyAmount)
        {
            TaskComplete.Invoke();
        }

    }
}
