using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettysGameGridComponent : MonoBehaviour
{
    [SerializeField] private int x; 
    [SerializeField] private float space;
    [SerializeField] private int maxChildCount;
    [SerializeField] private GameObject prefab;

    [SerializeField] private Material[] materials;

    private List<(int, int, int)> gridData = new List<(int, int, int)>
    {
        (1,3,2),
        (1,2,1),
        (1,4,3),
        (2,3,2),
        (2,4,3),
        (2,6,5),
        (2,8,7),
        (3,6,5),
        (3,9,8),
        (4,8,7),
        (4,12,11),
        (4,16,15)
    };

    private Material currentmat;

    private void Start()
    {
        currentmat = materials[Random.Range(0, materials.Length)];
        int index = Random.Range(0, gridData.Count);
        x = gridData[index].Item1;
        maxChildCount = gridData[index].Item2;
        int randomCount = Random.Range(1, gridData[index].Item3);
        for (int i = 0; i < randomCount; i++)
        {
            GameObject newBlock = Instantiate(prefab,transform);
            newBlock.GetComponent<MeshRenderer>().material = currentmat;
        }
    }

    private void LateUpdate()
    {
        int childCount = transform.childCount; 

        for (int i = 0; i < childCount; i++)
        {
            int row = i / x; 
            int col = i % x; 

            transform.GetChild(i).localPosition = new Vector3(col * space, -row * space, 0);
        }

        if (transform.childCount >= maxChildCount)
        {
            Destroy(gameObject);
        }
        if (BettysGameController.gameLaunched)
        {
            transform.position += new Vector3(0, -1, 0) * (1 * (ProfileData.BettysPlayerCurrentLevel + 1)) * Time.deltaTime;
        }
        
    }

    private void OnDestroy()
    {
        BettysGameController.gridPerLevel -= 1;
        BettysGameController.score += Random.Range(1, 10);
        BettysGameController.coins += Random.Range(1, 4);
        if (BettysGameController.gameType == GameType.Endless)
            return;
        if (BettysGameController.gridPerLevel <= 0)
        {
            BettysGameController.showResult?.Invoke(true);
        }
    }
}