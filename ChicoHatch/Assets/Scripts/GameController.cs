using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Material[] _skins;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private RockComponent _rockPrefab;
    [SerializeField] private Transform[] _spawnPositions;

    [SerializeField] private int Count;

    [SerializeField] private GameObject[] _labyrinth;

    private Material[] materials;

    [SerializeField] private Text[] _displayLevelText;

    public static int CurrentLevel = 0;

    private IEnumerator SpawnRocks()
    {
        for (int i = 0; i < Count; i++)
        {
            Instantiate(_rockPrefab, new Vector3(Random.Range(_spawnPositions[0].position.x, _spawnPositions[1].position.x), Random.Range(_spawnPositions[0].position.y, _spawnPositions[1].position.y), Random.Range(_spawnPositions[0].position.z, _spawnPositions[1].position.z)), Quaternion.Euler(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360)));
            
        }
        yield return new WaitForSeconds(0.1f);
    }

    private void Start()
    {

        StartCoroutine(SpawnRocks());
        int index = 0;
        foreach (var item in _displayLevelText)
        {
            item.text = "LEVEL " + (CurrentLevel + 1).ToString();
        }
        if (CurrentLevel >= _labyrinth.Length)
        {
            index = Random.Range(0, _labyrinth.Length);
        }
        else
        {
            index = CurrentLevel;
        }
        _labyrinth[index].SetActive(true);
        materials = _meshRenderer.materials;
        materials[0] = _skins[MenuManager.CurrentSkinIndex]; 
        _meshRenderer.materials = materials; 
    }

    public void OnNext()
    {
        CurrentLevel += 1;
        if (CurrentLevel > MenuManager.MaxreachedLevel)
        {
            MenuManager.MaxreachedLevel = CurrentLevel;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
