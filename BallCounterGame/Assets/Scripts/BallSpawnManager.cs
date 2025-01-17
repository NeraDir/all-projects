using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BallSpawnManager : MonoBehaviour
{
    [SerializeField]
    private float xSpawnRange;
    [SerializeField]
    private float ySpawnRange;

    [SerializeField]
    private List<Ball> ballPrefabs;

    private Ball redBall;
    private Ball greenBall;
    private Ball blueBall;

    [SerializeField]
    private int ballCountToSpawn;


    private void Start()
    {

    }


    public void Init()
    {
        for (int i = 0; i < ballPrefabs.Count; i++)
        {
            if (ballPrefabs[i].GetColorType() == ColorType.Red)
            {
                redBall = ballPrefabs[i];
            }
            else if (ballPrefabs[i].GetColorType() == ColorType.Green)
            {
                greenBall = ballPrefabs[i];
            }
            else if (ballPrefabs[i].GetColorType() == ColorType.Blue)
            {
                blueBall = ballPrefabs[i];
            }


        }

        GenerateBallsCount();
    }


    public void StartSpawn()
    {
        Debug.Log("CALL");
        SpawnBalls();
    }

    public void SpawnBalls()
    {
        List<Ball> newBalls = new();

        newBalls.AddItems(redBall, GamePlayController.redBallCountInScene);
        newBalls.AddItems(greenBall, GamePlayController.greenBallCountInScene);
        newBalls.AddItems(blueBall, GamePlayController.blueBallCountInScene);


        StartCoroutine(spawnBalls(newBalls));

    }

    private IEnumerator spawnBalls(List<Ball> newBalls)
    {
        for (int i = 0; i < newBalls.Count; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));

            Vector3 newBallPos = new Vector3(Random.Range(transform.position.x - xSpawnRange, transform.position.x + xSpawnRange), Random.Range(transform.position.y - ySpawnRange, transform.position.y + ySpawnRange), transform.position.z);
            Ball newBall = Instantiate(newBalls[i], newBallPos, newBalls[i].transform.rotation);
            newBall.GetComponent<Rigidbody2D>().gravityScale = Random.Range(0.15f, 0.25f);
        }
    }





    private void GenerateBallsCount()
    {
        List<int> numbersList = new(3);
        int number = 0;



        if (GamePlayController.levelNumber < 4)
            ballCountToSpawn = Random.Range(GamePlayController.levelNumber + 2, (GamePlayController.levelNumber + 6));
        else
            ballCountToSpawn = Random.Range(GamePlayController.levelNumber * 2, (GamePlayController.levelNumber * 2) + 5);
        




        number = Random.Range(1, ballCountToSpawn - 2);
        numbersList.Add(number);

        number = Random.Range(numbersList[0] + 1, ballCountToSpawn);
        numbersList.Add(number);

        number = ballCountToSpawn - numbersList[1];
        numbersList.Add(number);



        /*
        numbersList[0] = Random.Range(1, ballCountToSpawn - 2);
        numbersList[1] = Random.Range(numbersList[0] + 1, ballCountToSpawn);
        numbersList[2] = ballCountToSpawn - numbersList[1];
        */

        numbersList[1] -= numbersList[0];


        //List<int> numbersLis = new();
        //numbersLis.Shuffle();




        Debug.Log("sum: " + ballCountToSpawn);
        //Debug.Log(numbersList[0] + ", " + numbersList[1] + ", " + numbersList[2]);
        numbersList.Shuffle();
        //Debug.Log(numbersList[0] + ", " + numbersList[1] + ", " + numbersList[2]);

        GamePlayController.SetBallNumbersCount(numbersList);

        

    }


}


public static class ExtensionMethod
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;

        while (n > 1)
        {
            n--;

            int randIndex = Random.Range(0, n + 1);

            T buff = list[randIndex];
            list[randIndex] = list[n];
            list[n] = buff;
        }
    }
    public static void AddItems<T>(this IList<T> list, T item, int count)
    {
        int n = 0;

        while(n < count)
        {
            n++;
            list.Add(item);
        }
    }

}

/*
public static void Shufle<T>(this IList<T> list)
{
    int n = list.Count;

    while (n > 1)
    {
        n--;

    }
}
*/

