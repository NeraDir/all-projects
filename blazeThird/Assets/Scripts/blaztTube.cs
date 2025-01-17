using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blaztTube : MonoBehaviour
{
    [SerializeField]
    private Image _berryPrefab;

    [SerializeField]
    private Transform _berrySpawnPosition;

    private List<Image> _currentBerrysList = new List<Image>();

    private void Awake()
    {
        for (int i = 0; i < 15; i++)
        {
            SpawnNew();
        }
    }

    public void DestroyFirst()
    {
        Destroy(_currentBerrysList[0].gameObject);
        _currentBerrysList.Remove(_currentBerrysList[0]);
    }

    public void OnClickCheckBerry()
    {
        if (_currentBerrysList[0].sprite == blaztGame.targetBerry)
        {
            blaztGame.setNewBerry?.Invoke();
        }
    }

    public Image GetFirstImage()
    {
        return _currentBerrysList[0];
    }

    public void SpawnNew()
    {
        _currentBerrysList.Add(Instantiate(_berryPrefab, _berrySpawnPosition));
    }
}
