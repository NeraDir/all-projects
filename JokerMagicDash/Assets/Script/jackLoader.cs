using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class jackLoader : MonoBehaviour
{
    [SerializeField]
    private Transform[] _spawnPositions;

    [SerializeField]
    private GameObject _diceObject;

    private IEnumerator Start()
    {
        float value = 0;
        while (value < 8) 
        {
            Instantiate(_diceObject, new Vector3(Random.Range(_spawnPositions[0].position.x, _spawnPositions[1].position.x), _spawnPositions[0].position.y, _spawnPositions[0].position.z), Quaternion.identity);
            value += 0.1f;
            yield return new WaitForSeconds(0.1f);
            
        }
        SceneManager.LoadScene("Menu");
    }
}
