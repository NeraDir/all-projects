using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultController : MonoBehaviour
{
    [SerializeField] private Transform posSpawn;
    [SerializeField] private List<GameObject> prefabCatapult = new List<GameObject>();

    public AnimControllerCatapult SetCatapult(int id)
    {
        GameObject inst = Instantiate(prefabCatapult[id - 1], posSpawn.position, posSpawn.rotation, posSpawn);

        return inst.GetComponent<AnimControllerCatapult>(); // catapult
    }
}
