using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aviaEnemie : MonoBehaviour
{
    public GameObject aviatorPlanes;

    public List<float> positionsOfX = new List<float>();

    public static int enemieHealth;

    private IEnumerator Start()
    {
        enemieHealth = 30;
        while (true)
        {
            yield return new WaitForSeconds(aviGameController.aviatorSpawningTime);
            if (!aviGameController.isGameEnd)
            {
                if (Random.Range(0, 2) != 0)
                {

                }
                else
                {
                    foreach (var item in positionsOfX)
                    {
                        if (Random.Range(0, 2) != 0)
                            continue;
                        GameObject tempEnemieAviator = Instantiate(aviatorPlanes, new Vector3(item, 5.9f, 0), aviatorPlanes.transform.rotation);
                        tempEnemieAviator.GetComponent<aviaComponent>().enemies = true;
                    }
                }
            }
            
        }
    }
}
