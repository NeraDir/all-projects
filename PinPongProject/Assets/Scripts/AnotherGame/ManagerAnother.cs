using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAnother : MonoBehaviour
{
    public RectTransform Parrent;
    public List<RectTransform> Balls = new();
    public List<RectTransform> positions = new();

    private int counter = 0;

    private void Awake()
    {
        Physics2D.gravity = new Vector2(0, -400f);
    }

    private void Start()
    {
        StartCoroutine(SpawnBalls());
    }

    IEnumerator SpawnBalls()
    {
        while (true)
        {
            RectTransform pampam1 = positions[Random.Range(0, positions.Count)];
            RectTransform pampam = Instantiate(Balls[counter], pampam1.position, Quaternion.identity, Parrent);

            counter++;

            if(counter == Balls.Count)
            {
                StopAllCoroutines();
            }

            yield return new WaitForSeconds(1.5f);
        }
    }
}
