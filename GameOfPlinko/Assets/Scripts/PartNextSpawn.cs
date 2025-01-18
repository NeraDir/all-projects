using UnityEngine;

public class PartNextSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _nextPart;

    private bool _triggered;

    public void OnTriggerUSE() 
    { 
        if (_triggered)
            return;
        Instantiate(_nextPart[Random.Range(0,_nextPart.Length)], new Vector3(-11.7f, -18.12722f, transform.position.z + 100), _nextPart[Random.Range(0,_nextPart.Length)].transform.rotation);
        Destroy(gameObject.transform.parent.gameObject, 10);
        _triggered = true;
    }
}
